using LibraryMinimalApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLibrary.Models;
using WebLibrary.Services;

namespace WebLibrary.Controllers
{
    public class AvailableController : Controller
    {
        private readonly ILibraryService _libraryService;

        public AvailableController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        public async Task<IActionResult> AvailableBooks()
        {
            var response = await _libraryService.GetAvailableBooks<ResponseDTO>();

            if (response != null && response.IsSuccess)
            {
                List<BookDTO> availableBooks = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
                return View(availableBooks);
            }

            return View();
        }
    }
}
