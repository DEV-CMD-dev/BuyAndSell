import React from 'react';
import SearchAndFilters from '../components/SearchAndFilters';
import ListingCard from '../components/ListingCard';
import { getAdsList } from '../services/adsService';
import './HomePage.css';

export default function HomePage() {
  const [listings, setListings] = React.useState([]);
  const [filtered, setFiltered] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState('');

  React.useEffect(() => {
    let mounted = true;
    (async () => {
      try {
        setLoading(true);
        const data = await getAdsList();
        if (!mounted) return;
        const items = (data || []).map(d => ({
          id: String(d.id),
          title: d.title || '',
          description: d.description || '',
          price: typeof d.price === 'number' ? `${d.price}₴` : (d.price ?? ''),
          location: d.location ?? '',
          image: d.image ?? '',
          createdAt: d.createdAt ? new Date(d.createdAt) : new Date()
        }));
        setListings(items);
        setFiltered(items);
      } catch (e) {
        setError(e?.message || 'Помилка завантаження');
      } finally {
        setLoading(false);
      }
    })();
    return () => { mounted = false; };
  }, []);

  React.useEffect(() => { setFiltered(listings); }, [listings]);

  const handleSearch = ({ q = '', city = '' } = {}) => {
    const qLow = String(q).toLowerCase();
    const cityLow = String(city).toLowerCase();
    const results = listings.filter(l => {
      const title = (l.title || '').toLowerCase();
      const desc = (l.description || '').toLowerCase();
      const loc = (l.location || '').toLowerCase();
      const matchesQ = !qLow || title.includes(qLow) || desc.includes(qLow);
      const matchesCity = !cityLow || loc.includes(cityLow);
      return matchesQ && matchesCity;
    });
    setFiltered(results);
  };

  return (
    <div className="home-container">
      <SearchAndFilters onSearch={handleSearch} />
      {loading && <div className="loading">Завантаження...</div>}
      {error && <div className="error">{error}</div>}
      <div className="listing-grid">
        {filtered.map(item => (<ListingCard key={item.id} item={item} />))}
      </div>
      {!loading && filtered.length === 0 && <div className="empty">Нічого не знайдено</div>}
    </div>
  );
}