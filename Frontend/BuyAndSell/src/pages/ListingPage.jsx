import { useParams, useNavigate } from 'react-router-dom';
import './ListingPage.css';
import NotFound from './NotFound';


const MOCK_LISTINGS = [
    {
        id: '1',
        isNew: true,
        title: 'Ноутбук Lenovo IdeaPad',
        description: "Cool laptop..",
        price: '12 500₴',
        location: 'Київ',
        image: 'https://content2.rozetka.com.ua/goods/images/big/465898060.jpg',
        createdAt: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000)
    },
    {
        id: '2',
        isNew: true,
        title: 'Велосипед CITY 28"',
        description: "Nice bike..",
        price: '3 200₴',
        location: 'Львів',
        image: 'https://content1.rozetka.com.ua/goods/images/big/430562622.jpg',
        createdAt: new Date(Date.now() - 5 * 24 * 60 * 60 * 1000)
    },
    {
        id: '3',
        isNew: true,
        title: 'Смартфон Galaxy A52',
        description: "",
        price: '6 800₴',
        location: 'Одеса',
        image: 'https://content.rozetka.com.ua/goods/images/big/523604275.jpg',
        createdAt: new Date(Date.now() - 10 * 24 * 60 * 60 * 1000)
    }
];


export default function ListingPage() {
    const { id } = useParams();
    const navigate = useNavigate();
    const listing = MOCK_LISTINGS.find(l => l.id === id);


    if (!listing) {
        return (
            <NotFound />
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