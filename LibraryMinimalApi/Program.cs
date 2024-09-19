
using AutoMapper;
using LibraryMinimalApi.Data;
using LibraryMinimalApi.Data.Enum;
using LibraryMinimalApi.EndPoints;
using LibraryMinimalApi.Models;
using LibraryMinimalApi.Models.DTOs;
using LibraryMinimalApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryMinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            builder.Services.AddScoped<IBookRepository, BookRepository>(); 
            builder.Services.AddAutoMapper(typeof(MappingConfig));


            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            var app = builder.Build();
            


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

        


            //app.MapGet("/api/books", (IMapper mapper) =>
            //{
            //    var bookDtos = mapper.Map<List<BookDTO>>(LibraryBooks.bookList);
            //    return Results.Ok(bookDtos);
            //}).WithName("GetBooks");

            //app.MapGet("/api/book/id/{id}", (IMapper mapper, int id) =>
            //{
            //    APIResponse response = new APIResponse();

            //    // Hämta boken baserat på ID
            //    var book = LibraryBooks.bookList.FirstOrDefault(b => b.BookID == id);

            //    if (book != null)
            //    {
            //        // Mappa boken till BookDTO
            //        var bookDto = mapper.Map<BookDTO>(book);

            //        // Bygg framgångsrikt svar
            //        response.Result = bookDto;
            //        response.IsSuccess = true;
            //        response.StatusCode = HttpStatusCode.OK;

            //        return Results.Ok(response);
            //    }
            //    else
            //    {
            //        // Om boken inte hittas
            //        response.IsSuccess = false;
            //        response.ErrorMessage.Add($"Book with ID {id} not found");
            //        response.StatusCode = HttpStatusCode.NotFound;

            //        return Results.NotFound(response);
            //    }
            //});
            //app.MapGet("/api/book/word/{word}", (IMapper mapper, string word) =>
            //{
            //    APIResponse response = new APIResponse();

            //    // Hämta alla böcker som innehåller ordet i titeln
            //    var books = LibraryBooks.bookList
            //        .Where(b => b.Title.Contains(word, StringComparison.OrdinalIgnoreCase))
            //        .ToList();

            //    if (books.Any())
            //    {
            //        // Mappa böckerna till BookDTO
            //        var bookDtos = mapper.Map<List<BookDTO>>(books);

            //        // Bygg framgångsrikt svar
            //        response.Result = bookDtos;
            //        response.IsSuccess = true;
            //        response.StatusCode = HttpStatusCode.OK;

            //        return Results.Ok(response);
            //    }
            //    else
            //    {
            //        // Om inga böcker hittas
            //        response.IsSuccess = false;
            //        response.ErrorMessage.Add($"No books found with word '{word}' in the title");
            //        response.StatusCode = HttpStatusCode.NotFound;

            //        return Results.NotFound(response);
            //    }
            //});
            //app.MapPost("/api/book", (IMapper mapper, CreateBookDTO create_book_dto) =>
            //{
            //    APIResponse response = new()
            //    {
            //        IsSuccess = false,
            //        StatusCode = HttpStatusCode.BadRequest
            //    };
            //    // Validering: Du kan lägga till mer validering om det behövs
            //    if (string.IsNullOrEmpty(create_book_dto.Title) || string.IsNullOrEmpty(create_book_dto.Author))
            //    {
            //        response.IsSuccess = false;
            //        response.ErrorMessage.Add("Title and Author are required fields.");
            //        response.StatusCode = HttpStatusCode.BadRequest;
            //        return Results.BadRequest(response);
            //    }

            //    // Kontrollera om genren är giltig
            //    if (!Enum.TryParse(typeof(BookGenre), create_book_dto.Genre, true, out var genre))
            //    {
            //        response.IsSuccess = false;
            //        response.ErrorMessage.Add("Invalid genre.");
            //        response.StatusCode = HttpStatusCode.BadRequest;
            //        return Results.BadRequest(response);
            //    }

            //    // Skapa en ny bok baserat på DTO
            //    var newBook = new Book
            //    {
            //        BookID = LibraryBooks.bookList.Max(b => b.BookID) + 1, // Skapa ett nytt ID (increment based on max ID)
            //        Title = create_book_dto.Title,
            //        Description = create_book_dto.Description,
            //        Author = create_book_dto.Author,
            //        Genre = (BookGenre)genre, // Omvandla till enum
            //        PublicationYear = create_book_dto.PublicationYear,
            //        IsAvailable = create_book_dto.IsAvailable
            //    };

            //    // Lägg till den nya boken i listan
            //    LibraryBooks.bookList.Add(newBook);

            //    // Skapa API-responsen
            //    response.Result = newBook;
            //    response.IsSuccess = true;
            //    response.StatusCode = HttpStatusCode.Created;

            //    return Results.Created($"/api/book/id/{newBook.BookID}", response);
            //});

            //app.MapPut("/api/book/{id}", (IMapper _mapper,[FromBody] UpdateBookDTO book_u_dto) =>
            //{
            //    APIResponse response = new()
            //    {
            //        IsSuccess = false,
            //        StatusCode = HttpStatusCode.BadRequest
            //    };

            //    Book bookFromLib = LibraryBooks.bookList.FirstOrDefault(b => b.BookID == book_u_dto.BookID);

            //    bookFromLib.Description = book_u_dto.Description;
            //    bookFromLib.PublicationYear = book_u_dto.PublicationYear;
            //    bookFromLib.IsAvailable = book_u_dto.IsAvailable;
            //    response.Result = _mapper.Map<UpdateBookDTO>(bookFromLib);
            //    response.IsSuccess = true;
            //    response.StatusCode = HttpStatusCode.OK;
            //    return Results.Ok(response);
            //});

            //app.MapDelete("/api/book/{id:int}", (int id) =>
            //{
            //    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            //    Book bookFromLib = LibraryBooks.bookList.FirstOrDefault(c => c.BookID == id);

            //    if (bookFromLib != null)
            //    {
            //        LibraryBooks.bookList.Remove(bookFromLib);
            //        response.IsSuccess = true;
            //        response.StatusCode = HttpStatusCode.NoContent;
            //        return Results.Ok(response);
            //    }
            //    else
            //    {
            //        response.ErrorMessage.Add("Invalid ID");
            //        return Results.BadRequest(response);
            //    }

            //}).WithName("DeleteCoupon");

            app.ConfigurationBookEndPoints();
            app.Run();
        }
    }
}
