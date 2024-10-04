import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import BookListPage from './pages/BookListPage';
import BookDetailPage from './pages/BookDetailPage';
import SearchBooksPage from './pages/SearchBooksPage';
import AddBookPage from './pages/AddBookPage';
import UpdateBookPage from './pages/UpdateBookPage';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<BookListPage />} />
          <Route path="/book/:id" element={<BookDetailPage />} />
          <Route path="/search" element={<SearchBooksPage />} />
          <Route path="/add" element={<AddBookPage />} />
          <Route path="/update/:id" element={<UpdateBookPage />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;


