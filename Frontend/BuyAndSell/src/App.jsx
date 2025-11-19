import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import AdvertisementPage from './pages/AdvertisementPage';
import AdsCreate from './pages/adsCreate';
import './index.css';

export default function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/listings/:id" element={<AdvertisementPage />} />
          <Route path="/ads/:id" element={<AdvertisementPage />} />
          <Route path="/ads/create" element={<AdsCreate />} />
          <Route path="/ads/:id/edit" element={<AdsCreate />} />
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