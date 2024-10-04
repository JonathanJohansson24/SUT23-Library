import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './DetailsPage.css'
const BookDetailPage = () => {
  const { id } = useParams(); // Hämta bok-ID från URL:en
  const [book, setBook] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    async function fetchBook() {
      try {
        const response = await fetch(`https://localhost:7124/api/books/${id}`);

        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        if (data.isSuccess) {
          setBook(data.result);
        } else {
          console.error("Failed to fetch book:", data.errorMessage);
        }
      } catch (error) {
        console.error("There was an error fetching the book!", error);
      }
    }

    fetchBook();
  }, [id]);

  const handleDelete = async () => {
    const confirmDelete = window.confirm("Are you sure you want to delete this book?");
    if (confirmDelete) {
      try {
        const response = await fetch(`https://localhost:7124/api/book/${id}`, {
          method: 'DELETE',
        });

        if (response.status === 204) {
          console.log("Book deleted successfully");
          navigate('/'); // Navigera tillbaka till boklistan
        } else {
          const data = await response.json();
          console.error('Failed to delete book:', data.errorMessage);
        }
      } catch (error) {
        console.error("There was an error deleting the book!", error);
      }
    }
  };

  const handleEdit = () => {
    navigate(`/update/${id}`); // Navigera till update-sidan för denna bok
  };

  return (
    <div className="details-container">
      {book ? (
        <div className="book-details">
          <h2>{book.title}</h2>
          <p><strong>Author:</strong> {book.author}</p>
          <p><strong>Description:</strong> {book.description || "No description provided."}</p>
          <p><strong>Genre:</strong> {book.genre}</p>
          <p><strong>Publication Year:</strong> {book.publicationYear}</p>
          <p><strong>Is Available:</strong> {book.isAvailable ? 'Yes' : 'No'}</p>

          {/* Knappar för edit och delete */}
          <div className="button-group">
            <button className="btn-edit" onClick={handleEdit}>Edit</button>
            <button className="btn-delete" onClick={handleDelete}>Delete</button>
          </div>
        </div>
      ) : (
        <p>Loading book details...</p>
      )}
    </div>
  );
};

export default BookDetailPage;