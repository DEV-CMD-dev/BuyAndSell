import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';


export default function Navbar() {
    return (
        <nav className="navbar">
            <div className="navbar-container">
                <Link to="/" className="logo">Buy&Sell</Link>
                <div className="nav-links">
                    <Link to="/newListing" className="btn">Додати оголошення</Link>
                    <Link to="/favorites" className="btn">Улюблені</Link>
                </div>
            </div>
        </nav>
    );
}