import React, { useEffect, useState } from 'react';
import SearchAndFilters from '../components/SearchAndFilters';
import ListingCard from '../components/ListingCard';
import './HomePage.css';
import ListingNotFound from './ListingNotFound';
import Loading from './Loading';

export default function HomePage() {
    const [listings, setListings] = useState([]);
    const [filtered, setFiltered] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchListings();
    }, []);

    const fetchListings = async (filters = {}) => {
        try {
            setLoading(true);

            const queryParams = new URLSearchParams();

            if (filters.searchByTitle) queryParams.append('searchByTitle', filters.searchByTitle);
            if (filters.searchByCity) queryParams.append('searchByCity', filters.searchByCity);
            if (filters.categoryIdFilter) queryParams.append('categoryIdFilter', filters.categoryIdFilter);
            if (filters.minPrice) queryParams.append('minPrice', filters.minPrice);
            if (filters.maxPrice) queryParams.append('maxPrice', filters.maxPrice);

            const res = await fetch(`https://localhost:7173/api/Advertisements?${queryParams.toString()}`);
            if (!res.ok) throw new Error('Server error');
            const data = await res.json();

            setListings(data);
            setFiltered(data);
        } catch (err) {
            console.error('API error:', err);
            setListings([]);
            setFiltered([]);
        } finally {
            setLoading(false);
        }
    };

    const handleSearch = async ({ title = '', city = '' }) => {
        await fetchListings({ searchByTitle: title, searchByCity: city });
    };

    return (
        <div className="home-container">
            <SearchAndFilters onSearch={handleSearch} />

            {loading ? (
                <Loading />
            ) : (
                <>
                    <div className="listing-grid">
                        {filtered.map(item => (<ListingCard key={item.id} item={item} />))}
                    </div>
                    {filtered.length === 0 && <ListingNotFound />}
                </>
            )}
        </div>
    );
}
