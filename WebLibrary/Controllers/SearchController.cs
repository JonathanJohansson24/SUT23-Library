using LibraryMinimalApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLibrary.Models;
using WebLibrary.Services;

namespace WebLibrary.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILibraryService _libraryService;

        public SearchController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public IActionResult Search()
        {
            return View();
        }

        // POST: Books/Search
        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                ViewBag.ErrorMessage = "Please enter a search term.";
                return View(); // Återgå till sökvy med felmeddelande
            }

            var response = await _libraryService.GetBookByWord<ResponseDTO>(search);
            Console.WriteLine($"Response Success: {response.IsSuccess}, Result: {response.Result}");

            // Logga full response
            Console.WriteLine($"Full Response: {JsonConvert.SerializeObject(response)}");

            if (response.IsSuccess && response.Result != null)
            {
                Console.WriteLine($"Response Result Type: {response.Result?.GetType()}");

                // Försök deserialisera till List<BookDTO>
                var jsonResult = JsonConvert.SerializeObject(response.Result);
                var books = JsonConvert.DeserializeObject<List<BookDTO>>(jsonResult);

                // Kontrollera om böckerna är null
                if (books != null && books.Any())
                {
                    Console.WriteLine($"Number of books to display: {books.Count()}");
                    return View("SearchResults", books);
                }
            }

            ViewBag.ErrorMessage = response.ErrorMessages?.FirstOrDefault() ?? "No books found.";
            return View();
        }
    }
}
