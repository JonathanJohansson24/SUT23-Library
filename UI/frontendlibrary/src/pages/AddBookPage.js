import React, { useState } from 'react';
import './FormPage.css'

    const AddBookPage = () => {
    const [book, setBook] = useState({  title: '',
        description: '',
        author: '',
        genre: '',
        publicationYear: '',
        isAvailable: true});

    const genres = [
        'Horror',
        'Thriller',
        'Fantasy',
        'Romance',
        'Mystery',
        'ScienceFiction',
        'NonFiction'
    ];

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
          const response = await fetch('https://localhost:7124/api/book', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(book),
          });
    
          const data = await response.json();
          if (data.isSuccess) {
            console.log('Book added successfully');
            // Återställ formuläret efter framgångsrik inlämning
            setBook({
              title: '',
              description: '',
              author: '',
              genre: 'Horror',
              publicationYear: '',
              isAvailable: true,
            });
          } else {
            console.error('Failed to add book', data.errorMessage);
          }
        } catch (error) {
          console.error('There was an error adding the book!', error);
        }
      };
    
      return (
        <div className="container">
          <h2>Add a New Book</h2>
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
    
            <button type="submit">Add Book</button>
          </form>
        </div>
      );
    };
    
    export default AddBookPage;