import React, { useState } from 'react';
import BookItem from '../components/BookItem';
import './SearchBookPage.css';

const SearchBooksPage = () => {
  const [query, setQuery] = useState('');
  const [books, setBooks] = useState([]);

  const searchBooks = async () => {
    try {
      const response = await fetch(`https://localhost:7124/api/books/search/${query}`);
      const data = await response.json();
      if (data.isSuccess) {
        setBooks(data.result);
      } else {
        console.error("No books found.");
      }
    } catch (error) {
      console.error("There was an error fetching the books!", error);
    }
  };

  return (
    <div>
      <h2>Search Books</h2>
      <div className="search-container">
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Search by title"
        />
        <button onClick={searchBooks}>Search</button>
      </div>

      <div className="book-list-container">
        {books.length > 0 ? (
          books.map((book) => <BookItem key={book.bookID} book={book} />)
        ) : (
          query && <p>No books found.</p>  // If no books are found, display a message.
        )}
      </div>
    </div>
  );
};

export default SearchBooksPage;