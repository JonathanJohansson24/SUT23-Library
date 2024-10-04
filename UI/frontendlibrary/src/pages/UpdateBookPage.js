import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './FormPage.css'

const UpdateBookPage = () => {
  const { id } = useParams();
  const [book, setBook] = useState({
    title: '',
    author: '',
    description: '',
    genre: '',
    publicationYear: '',
    isAvailable: true,
  });
  const genres = [
    'Horror',
    'Thriller',
    'Fantasy',
    'Romance',
    'Mystery',
    'ScienceFiction',
    'NonFiction'
];
  const navigate = useNavigate();

  useEffect(() => {
    async function fetchBook() {
      try {
        const response = await fetch(`https://localhost:7124/api/books/${id}`);
        const data = await response.json();
        if (data.isSuccess) {
          setBook(data.result);
        }
      } catch (error) {
        console.error("Failed to fetch book details");
      }
    }
    fetchBook();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(`https://localhost:7124/api/book`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(book),
      });

      const data = await response.json();
      if (data.isSuccess) {
        console.log('Book updated successfully');
        navigate(`/book/${id}`); // Tillbaka till bokens detaljsida efter uppdatering
      } else {
        console.error('Failed to update book:', data.errorMessage);
      }
    } catch (error) {
      console.error('There was an error updating the book!', error);
    }
  };

  return (
    <div className="container">
      <h2>Update Book</h2>
      <form onSubmit={handleSubmit}>
        {/* Title */}
        <label htmlFor="title">Title</label>
        <input
          type="text"
          id="title"
          placeholder="Title"
          value={book.title}
          onChange={(e) => setBook({ ...book, title: e.target.value })}
          required
        />

        {/* Author */}
        <label htmlFor="author">Author</label>
        <input
          type="text"
          id="author"
          placeholder="Author"
          value={book.author}
          onChange={(e) => setBook({ ...book, author: e.target.value })}
          required
        />

        {/* Description */}
        <label htmlFor="description">Description</label>
        <textarea
          id="description"
          placeholder="Description"
          value={book.description}
          onChange={(e) => setBook({ ...book, description: e.target.value })}
        />

        {/* Genre Dropdown */}
        <label htmlFor="genre">Genre</label>
        <select
          id="genre"
          value={book.genre}
          onChange={(e) => setBook({ ...book, genre: e.target.value })}
          required
        >
          {genres.map((genre) => (
            <option key={genre} value={genre}>
              {genre}
            </option>
          ))}
        </select>

        {/* Publication Year */}
        <label htmlFor="publicationYear">Publication Year</label>
        <input
          type="number"
          id="publicationYear"
          placeholder="Publication Year"
          value={book.publicationYear}
          onChange={(e) => setBook({ ...book, publicationYear: parseInt(e.target.value) || '' })}
          min="1800"
          max="2200"
          required
        />

        {/* IsAvailable Checkbox */}
        <label className="checkbox-label">
          <input
            type="checkbox"
            checked={book.isAvailable}
            onChange={(e) => setBook({ ...book, isAvailable: e.target.checked })}
          />
          Available
        </label>

        <button type="submit">Update Book</button>
      </form>
    </div>
  );
};

export default UpdateBookPage;