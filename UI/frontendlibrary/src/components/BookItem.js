import { Link } from 'react-router-dom';
import './BookItem.css';

const BookItem = ({ book }) => {
  return (
    <div className="book-card">
      <div className={`availability-indicator ${book.isAvailable ? 'available' : 'not-available'}`}>
        {book.isAvailable ? 'Available' : 'Unavailable'}
      </div>
      <h3>{book.title}</h3>
      <p><strong>Author:</strong> {book.author}</p>
      <p><strong>Description:</strong> {book.description}</p>
      <Link to={`/book/${book.bookID}`} className="details-button">
        View Details
      </Link>
    </div>
  );
};

export default BookItem;