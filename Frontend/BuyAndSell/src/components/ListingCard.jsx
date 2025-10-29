import React from 'react';
import { Link } from 'react-router-dom';
import './ListingCard.css';


export default function ListingCard({ item }) {
    return (
        <Link to={`/listings/${item.id}`} className="listing-card">
            <img src={item.image} alt={item.title} className="listing-img" />
            <div className="listing-body">
                <h3 className="listing-title">{item.title}</h3>
                <p className="listing-location">{item.location}</p>
                <div className="listing-footer">
                    <span className="listing-price">{item.price}</span>
                    <span className="listing-date">📅 2 дні тому</span>
                </div>
            </div>
        </Link>
    );
}