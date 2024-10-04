import React, { Component } from 'react';
import BookItem from '../components/BookItem';
import './BookListPage.css';

class BookListPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      books: [],
      currentPage: 1,
      booksPerPage: 4,
      sortCriteria: 'alphabetical' // Default sorting criteria
    };
  }

  API_URL = "https://localhost:7124/";

  componentDidMount() {
    this.getBooks();
  }

  async getBooks() {
    try {
      const response = await fetch(this.API_URL + "api/books");

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();

      if (data.isSuccess) {
        this.setState({ books: data.result });
      } else {
        console.error("Failed to fetch books:", data.errorMessage);
      }

    } catch (error) {
      console.error("There was an error fetching the books!", error);
    }
  }

  handlePageChange = (pageNumber) => {
    this.setState({ currentPage: pageNumber });
  }

  handleSortChange = (event) => {
    this.setState({ sortCriteria: event.target.value });
  }

  sortBooks = (books) => {
    const { sortCriteria } = this.state;
    if (sortCriteria === 'alphabetical') {
      return books.sort((a, b) => a.title.localeCompare(b.title));
    } else if (sortCriteria === 'availability') {
      return books.sort((a, b) => b.isAvailable - a.isAvailable);
    }
    return books;
  }

  render() {
    const { books, currentPage, booksPerPage, sortCriteria } = this.state;

    // Sort books based on the selected criteria
    const sortedBooks = this.sortBooks([...books]);

    // Calculate the books to display on the current page
    const indexOfLastBook = currentPage * booksPerPage;
    const indexOfFirstBook = indexOfLastBook - booksPerPage;
    const currentBooks = sortedBooks.slice(indexOfFirstBook, indexOfLastBook);

    // Calculate page numbers
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(books.length / booksPerPage); i++) {
      pageNumbers.push(i);
    }

    return (
      <div>
        <h2>Library</h2>
        <div className="sort-container">
          <label htmlFor="sort">Sort by:</label>
          <select id="sort" value={sortCriteria} onChange={this.handleSortChange}>
            <option value="alphabetical">Alphabetical</option>
            <option value="availability">Availability</option>
          </select>
        </div>
        <div className="book-list-container">
          {currentBooks.length > 0 ? (
            currentBooks.map((book) => <BookItem key={book.bookID} book={book} />)
          ) : (
            <p>No books available.</p>
          )}
        </div>
        <div className="pagination">
          {pageNumbers.map(number => (
            <button
              key={number}
              onClick={() => this.handlePageChange(number)}
              className={currentPage === number ? 'active' : ''}
            >
              {number}
            </button>
          ))}
        </div>
      </div>
    );
  }
}

export default BookListPage;