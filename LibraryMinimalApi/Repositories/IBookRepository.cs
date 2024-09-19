using LibraryMinimalApi.Data;
using LibraryMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMinimalApi.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetBookAsync(int id);
        Task<List<Book>> GetBookAsync(string word);

        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task RemoveBookAsync(Book book);
        Task SaveAsync();
    }

    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books.Where(b => b.IsAvailable).ToListAsync();  // Filtrera där IsAvailable = true
        }

        public async Task CreateBookAsync(Book book)
        {
           _context.Books.AddAsync(book);
            await SaveAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.BookID == id);
        }

        public async Task<List<Book>> GetBookAsync(string word)
        {
            return await _context.Books
                       .Where(b => b.Title.Contains(word) || b.Author.Contains(word))
        .ToListAsync();
        }

        public async Task RemoveBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            
        }
    }
}
