import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import ListingPage from './pages/AdvertisementPage';
import './index.css';


export default function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/listings/:id" element={<ListingPage />} />
          <Route
            path="*"
            element={
              <div className="page-not-found">
                <h2>Сторінку не знайдено</h2>
                <Link to="/" className="back-link">На головну</Link>
              </div>
            }
          />
        </Routes>
      </div>
    </BrowserRouter>
  );
}