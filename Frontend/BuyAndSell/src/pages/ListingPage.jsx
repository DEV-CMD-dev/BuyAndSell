import React from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './ListingPage.css';


const MOCK_LISTINGS = [
    { id: '1', title: 'Ноутбук Lenovo IdeaPad', price: '12 500₴', location: 'Київ', description: 'Стан ідеальний', image: 'https://picsum.photos/seed/1/600/400' },
    { id: '2', title: 'Велосипед CITY 28"', price: '3 200₴', location: 'Львів', description: 'Міський велосипед', image: 'https://picsum.photos/seed/2/600/400' }
];


export default function ListingPage() {
    const { id } = useParams();
    const navigate = useNavigate();
    const listing = MOCK_LISTINGS.find(l => l.id === id);


    if (!listing) {
        return (
            <div className="listing-not-found">
                <h2>Оголошення не знайдено</h2>
                <button onClick={() => navigate('/')}>Повернутись</button>
            </div>
        );
    }


    return (
        <div className="listing-page">
            <img src={listing.image} alt={listing.title} className="listing-image" />
            <div className="listing-details">
                <h1>{listing.title}</h1>
                <p>{listing.location}</p>
                <p className="price">{listing.price}</p>
                <p>{listing.description}</p>
            </div>
        </div>
    );
}