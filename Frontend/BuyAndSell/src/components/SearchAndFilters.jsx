import React from 'react';
import './SearchAndFilters.css';


export default function SearchAndFilters({ onSearch }) {
    const [title, setTitle] = React.useState('');
    const [city, setCity] = React.useState('');


    const handleSubmit = (e) => {
        e.preventDefault();
        onSearch({ title, city });
    };


    return (
        <form className="search-form" onSubmit={handleSubmit}>
            <input value={title} onChange={(e) => setTitle(e.target.value)} placeholder="Що шукаєш?" className="input search" />
            <input value={city} onChange={(e) => setCity(e.target.value)} placeholder="Місто" className="input city" />
            <button className="search-btn">Пошук</button>
        </form>
    );
}