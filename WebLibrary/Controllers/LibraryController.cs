using LibraryMinimalApi.Models;
using LibraryMinimalApi.Models.DTOs;
using LibraryMinimalApi.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLibrary.Models;
using WebLibrary.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebLibrary.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        public async Task<IActionResult> Index()
        {
            List<BookDTO> list = new List<BookDTO>();

            var response = await _libraryService.GetAllBooks<ResponseDTO>();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject
                    <List<BookDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            BookDTO bookDto = new BookDTO();

            var response = await _libraryService.GetBookById<ResponseDTO>(id);

            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return View();
        }

        public async Task<IActionResult> CreateBook()
        {
            ViewBag.Genres = Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>()
                          .Select(g => new SelectListItem
                          {
                              Value = g.ToString(),
                              Text = g.ToString()
                          });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                // Validera att genren är giltig
                if (!Enum.TryParse(typeof(BookGenre), model.Genre, true, out var genre))
                {
                    ModelState.AddModelError("Genre", "Invalid genre.");
                    return View(model);
                }

                var bookDto = new BookDTO
                {
                    Title = model.Title,
                    Author = model.Author,
                    PublicationYear = model.PublicationYear,
                    Genre = model.Genre, // Håll som string
                    IsAvailable = model.IsAvailable
                };

                // Anropa tjänsten för att skapa boken
                var response = await _libraryService.CreateBookAsync<ResponseDTO>(bookDto);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateBook(int bookId)
        {
            var response = await _libraryService.GetBookById<ResponseDTO>(bookId);

            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _libraryService.UpdateBookAsync<ResponseDTO>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var response = await _libraryService.GetBookById<ResponseDTO>(bookId);

            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            //return View();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(BookDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _libraryService.DeleteBookAsync<ResponseDTO>(model.BookID);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }



    }
}
