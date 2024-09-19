using LibraryMinimalApi.Models.DTOs;
using System.Linq;

namespace WebLibrary.Services
{
    public interface ILibraryService
    {
        Task<T> GetAllBooks<T>();
        Task<T> GetBookById<T>(int bookId);
        Task<T> GetBookByWord<T>(string word);
        Task<T> GetAvailableBooks<T>();
        Task<T> CreateBookAsync<T>(BookDTO bookDto);
        Task<T> UpdateBookAsync<T>(BookDTO bookDto);
        Task<T> DeleteBookAsync<T>(int bookId);
    }

    public class LibraryService : BaseService, ILibraryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public LibraryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        
        public async Task<T> CreateBookAsync<T>(BookDTO bookDto)
        {
            return await SendAsync<T>(new Models.ApiRequest
            {
                Type = StaticDetails.ApiType.POST,
                Data = bookDto,
                Url = StaticDetails.LibraryApiBase + "/api/book"
            });
        }

        public async Task<T> DeleteBookAsync<T>(int bookId)
        {
            return await SendAsync<T>(new Models.ApiRequest
            {
                Type = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.LibraryApiBase + "/api/book/" + bookId,
                AccessToken = ""
            });
        }

      

        public Task<T> GetAllBooks<T>()
        {
            return this.SendAsync<T>(new Models.ApiRequest()
            {
                Type = StaticDetails.ApiType.GET,
                Url = StaticDetails.LibraryApiBase + "/api/books",
                AccessToken = ""
            });
        }
        public Task<T> GetAvailableBooks<T>()
        {
            return this.SendAsync<T>(new Models.ApiRequest()
            {
                Type = StaticDetails.ApiType.GET,
                Url = StaticDetails.LibraryApiBase + "/api/books/available",  // Anropa den nya endpointen
                AccessToken = ""
            });
        }

        public async Task<T> GetBookById<T>(int bookId)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                Type = StaticDetails.ApiType.GET,
                Url = StaticDetails.LibraryApiBase + "/api/books/" + bookId,
                AccessToken = ""
            });
        }

        public async Task<T> GetBookByWord<T>(string word)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                Type = StaticDetails.ApiType.GET,
                Url = StaticDetails.LibraryApiBase + "/api/books/search/" + word,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBookAsync<T>(BookDTO bookDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest
            {
                Type = StaticDetails.ApiType.PUT,
                Data = bookDTO,
                Url = StaticDetails.LibraryApiBase + "/api/book",
                AccessToken = ""
            });
        }
    }
}
