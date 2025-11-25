import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './ListingPage.css';
import Loading from './Loading';



export default function ListingPage() {
    const { id } = useParams();
    const [listing, setListing] = useState(null);

    useEffect(() => {
        fetch(`https://localhost:7173/api/Advertisements/${id}`)
            .then(res => res.json())
            .then(data => setListing(data))
            .catch(err => console.error(err));
    }, [id]);

    if (!listing) {
        return (
            <Loading />
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
                <p>Стан: {listing.isNew ? "Новий" : "Б/В"}</p>
            </div>
        </div>
    );
}