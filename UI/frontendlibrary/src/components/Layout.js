import { Link } from 'react-router-dom';
import './Navbar.css'
const Layout = ({ children }) => {
  return (
    <div>
      <nav className="navbar">
        <ul className="navbar-list">
          <li className="navbar-item">
            <Link to="/" className="navbar-link">Library</Link>
          </li>
          <li className="navbar-item">
            <Link to="/search" className="navbar-link">Search Books</Link>
          </li>
          <li className="navbar-item">
            <Link to="/add" className="navbar-link">Add a Book</Link>
          </li>
        </ul>
      </nav>
      <hr />
      {children}
    </div>
  );
};

export default Layout;