const API_URL = "http://localhost:7124/";

// Hämta alla böcker
async function getBooks() {
  try {
    const response = await fetch(API_URL + "api/books");
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    if (data.isSuccess) {
      return data.result;
    } else {
      console.error("Failed to fetch books:", data.errorMessage);
      return [];
    }
  } catch (error) {
    console.error("There was an error fetching the books!", error);
    return [];
  }
}

// Hämta en bok baserat på ID
async function getBookById(id) {
  try {
    const response = await fetch(`${API_URL}api/books/${id}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    if (data.isSuccess) {
      return data.result;
    } else {
      console.error(`Failed to fetch book with ID ${id}:`, data.errorMessage);
      return null;
    }
  } catch (error) {
    console.error("There was an error fetching the book by ID!", error);
    return null;
  }
}

// Hämta böcker som matchar ett sökord i titeln
async function getBooksByWord(word) {
  try {
    const response = await fetch(`${API_URL}api/books/search/${word}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    if (data.isSuccess) {
      return data.result;
    } else {
      console.error(`No books found with the word '${word}':`, data.errorMessage);
      return [];
    }
  } catch (error) {
    console.error("There was an error fetching books by word!", error);
    return [];
  }
}

// Hämta böcker som är tillgängliga
async function getAvailableBooks() {
  try {
    const response = await fetch(`${API_URL}api/books/available`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const data = await response.json();
    if (data.isSuccess) {
      return data.result;
    } else {
      console.error("Failed to fetch available books:", data.errorMessage);
      return [];
    }
  } catch (error) {
    console.error("There was an error fetching available books!", error);
    return [];
  }
}

// Exportera alla funktioner för användning i andra komponenter
export {
  getBooks,
  getBookById,
  getBooksByWord,
  getAvailableBooks
};