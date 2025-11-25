import { Link } from 'react-router-dom';
import './ListingCard.css';


export default function ListingCard({ item }) {
    if (!item) return null;

    return (
        <Link to={`/listings/${item.id}`} className="listing-card">
            <img src={item.image} alt={item.title} className="listing-img" />
            <div className="listing-body">
                <h3 className="listing-title">{item.title}</h3>
                <p className="listing-location">{item.location}</p>
                <div className="listing-footer">
                    <span className="listing-price">{item.price}</span>
                    <span className="listing-date">📅 {new Date(item.createdAt).toLocaleTimeString()}</span>
                </div>
            </div>
        </Link>
    );
}