import React from 'react';
import './SearchAndFilters.css';


export default function SearchAndFilters({ onSearch }) {
    const [q, setQ] = React.useState('');
    const [city, setCity] = React.useState('');


    const handleSubmit = (e) => {
        e.preventDefault();
        onSearch({ q, city });
    };


    return (
        <form className="search-form" onSubmit={handleSubmit}>
            <input value={q} onChange={(e) => setQ(e.target.value)} placeholder="Що шукаєш?" className="input search" />
            <input value={city} onChange={(e) => setCity(e.target.value)} placeholder="Місто" className="input city" />
            <button className="search-btn">Пошук</button>
        </form>
    );
}