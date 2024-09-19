using AutoMapper;
using LibraryMinimalApi.Data;
using LibraryMinimalApi.Data.Enum;
using LibraryMinimalApi.Models;
using LibraryMinimalApi.Models.DTOs;
using LibraryMinimalApi.Repositories;
using System.Net;
using static Azure.Core.HttpHeader;

namespace LibraryMinimalApi.EndPoints
{
    public static class BookEndpoints
    {


        public static void ConfigurationBookEndPoints(this WebApplication app)
        {
            app.MapGet("/api/books", GetAllBooks).WithName("GetBooks").Produces<APIResponse>();
            app.MapGet("/api/books/{id:int}", GetBookById).WithName("GetBookById").Produces<APIResponse>(404);
            app.MapGet("/api/books/search/{word}", GetBookByWord).WithName("GetBookByWord").Produces<APIResponse>(200);
            app.MapGet("/api/books/available", GetAvailableBooks).WithName("GetAvailableBooks").Produces<APIResponse>();
            app.MapPost("/api/book", CreateBook).WithName("CreateBook").Accepts<CreateBookDTO>("application/json").Produces<APIResponse>(201).Produces(400);
            app.MapPut("/api/book", UpdateBook).WithName("UpdateBook").Accepts<UpdateBookDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("/api/book/{id:int}", DeleteBook).WithName("DeleteBook").Produces<APIResponse>(204).Produces(404);
        }

        private async static Task<IResult> GetAllBooks(IBookRepository _bookRepository, IMapper _mapper)
        {
            APIResponse response = new APIResponse();

            var books = await _bookRepository.GetAllBooksAsync();

            // Mappa böckerna till DTOs
            var bookDTOS = _mapper.Map<List<BookDTO>>(books);
            response.Result = bookDTOS;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> GetBookById(IBookRepository _bookRepository, IMapper _mapper, int id)
        {
            APIResponse response = new APIResponse();
            // Hämta boken baserat på ID från databasen via repository
            var book = await _bookRepository.GetBookAsync(id);

            if (book != null)
            {
                // Mappa boken till BookDTO med AutoMapper
                var bookDTO = _mapper.Map<BookDTO>(book);

                // Bygg framgångsrikt svar
                response.Result = bookDTO;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }
            else
            {
                // Om boken inte hittas
                response.IsSuccess = false;
                response.ErrorMessage.Add($"Book with ID {id} not found");
                response.StatusCode = HttpStatusCode.NotFound;

                return Results.NotFound(response);
            }
        }
        private async static Task<IResult> GetBookByWord(IBookRepository _bookRepository, IMapper _mapper, string word)
        {
            APIResponse response = new APIResponse();

            var books = await _bookRepository.GetBookAsync(word);

            if (books.Any())
            {
                // Mappa böckerna till BookDTO med AutoMapper
                var bookDtos = _mapper.Map<List<BookDTO>>(books);

                // Bygg ett framgångsrikt svar
                response.Result = bookDtos;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }
            else
            {
                // Om inga böcker hittas
                response.IsSuccess = false;
                response.ErrorMessage.Add($"No books found with word '{word}' in the title");
                response.StatusCode = HttpStatusCode.NotFound;

                return Results.NotFound(response);
            }

        }
        private async static Task<IResult> GetAvailableBooks(IBookRepository _bookRepository, IMapper _mapper)
        {
            APIResponse response = new APIResponse();

            var books = await _bookRepository.GetAvailableBooksAsync();  // Använd den nya repository-metoden

            var bookDTOS = _mapper.Map<List<BookDTO>>(books);
            response.Result = bookDTOS;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> CreateBook(IBookRepository _bookRepository, IMapper _mapper, CreateBookDTO book_c_dto)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            // Validering: Kontrollera att obligatoriska fält finns
            if (string.IsNullOrEmpty(book_c_dto.Title) || string.IsNullOrEmpty(book_c_dto.Author))
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add("Title and Author are required fields.");
                response.StatusCode = HttpStatusCode.BadRequest;
                return Results.BadRequest(response);
            }

            // Kontrollera om genren är giltig
            if (!Enum.TryParse(typeof(BookGenre), book_c_dto.Genre, true, out var genre))
            {
                response.IsSuccess = false;
                response.ErrorMessage.Add("Invalid genre.");
                response.StatusCode = HttpStatusCode.BadRequest;
                return Results.BadRequest(response);
            }

            // Mappa DTO till Book-modellen
            var newBook = _mapper.Map<Book>(book_c_dto);
            newBook.Genre = (BookGenre)genre; // Omvandla till enum

            // Lägg till boken i databasen via repository
            await _bookRepository.CreateBookAsync(newBook);

            // Skapa API-responsen
            response.Result = newBook;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;

            return Results.Created($"/api/books/{newBook.BookID}", response);
        }

        private async static Task<IResult> UpdateBook(IBookRepository _bookRepository, IMapper _mapper, UpdateBookDTO book_u_dto)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            await _bookRepository.UpdateBookAsync(_mapper.Map<Book>(book_u_dto));
            await _bookRepository.SaveAsync();

            response.Result = _mapper.Map<UpdateBookDTO>(await _bookRepository.GetBookAsync(book_u_dto.BookID));
            response.IsSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return Results.Ok(response);

        }

        private async static Task<IResult> DeleteBook(IBookRepository _bookRepository, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            // Hämta boken baserat på ID från databasen via repository
            var bookFromDb = await _bookRepository.GetBookAsync(id);

            if (bookFromDb != null)
            {
                // Ta bort boken från databasen
                await _bookRepository.RemoveBookAsync(bookFromDb);

                // Bygg framgångsrikt svar
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent; // 204 No Content efter en lyckad borttagning
                return Results.Ok(response);
            }
            else
            {
                // Om boken inte hittas, returnera fel
                response.ErrorMessage.Add("Invalid ID");
                response.StatusCode = HttpStatusCode.NotFound; // Returnera 404 om boken inte hittas
                return Results.NotFound(response);
            }
        }
    }
}
