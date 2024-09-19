using LibraryMinimalApi.Data.Enum;
using LibraryMinimalApi.Models;

namespace LibraryMinimalApi.Data
{
    public static class LibraryBooks
    {
        public static List<Book> bookList = new List<Book>
        {
            new Book
            {
                BookID = 1,
                Title = "The Shining",
                Author = "Stephen King",
                Genre = BookGenre.Horror,
                Description = "A horror novel about a haunted hotel.",
                PublicationYear = 1977,
                IsAvailable = true
            },
            new Book
            {
                BookID = 2,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Genre = BookGenre.Fantasy,
                Description = "A fantasy novel about a hobbit's adventure.",
                PublicationYear = 1937,
                IsAvailable = false
            },
            new Book
            {
                BookID = 3,
                Title = "Gone Girl",
                Author = "Gillian Flynn",
                Genre = BookGenre.Thriller,
                Description = "A thriller about a woman's mysterious disappearance.",
                PublicationYear = 2012,
                IsAvailable = true
            },
            new Book
            {
                BookID = 4,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Genre = BookGenre.Romance,
                Description = "A classic romance about love and social standing.",
                PublicationYear = 1813,
                IsAvailable = true
            },
            new Book
            {
                BookID = 5,
                Title = "The Da Vinci Code",
                Author = "Dan Brown",
                Genre = BookGenre.Mystery,
                Description = "A mystery thriller about a secret society and ancient symbols.",
                PublicationYear = 2003,
                IsAvailable = false
            },
            new Book
            {
                BookID = 6,
                Title = "1984",
                Author = "George Orwell",
                Genre = BookGenre.Fantasy,
                Description = "A dystopian novel about a totalitarian regime.",
                PublicationYear = 1949,
                IsAvailable = true
            },
            new Book
            {
                BookID = 7,
                Title = "Dracula",
                Author = "Bram Stoker",
                Genre = BookGenre.Horror,
                Description = "A horror novel about the legendary vampire Count Dracula.",
                PublicationYear = 1897,
                IsAvailable = false
            },
            new Book
            {
                BookID = 8,
                Title = "The Girl with the Dragon Tattoo",
                Author = "Stieg Larsson",
                Genre = BookGenre.Mystery,
                Description = "A mystery thriller about a journalist and a hacker solving a cold case.",
                PublicationYear = 2005,
                IsAvailable = true
            },
            new Book
            {
                BookID = 9,
                Title = "Twilight",
                Author = "Stephenie Meyer",
                Genre = BookGenre.Romance,
                Description = "A romance fantasy novel about a girl falling in love with a vampire.",
                PublicationYear = 2005,
                IsAvailable = true
            },
            new Book
            {
                BookID = 10,
                Title = "The Hunger Games",
                Author = "Suzanne Collins",
                Genre = BookGenre.Fantasy,
                Description = "A dystopian novel about a fight to the death in a televised arena.",
                PublicationYear = 2008,
                IsAvailable = false
            }
        };
    }
}
