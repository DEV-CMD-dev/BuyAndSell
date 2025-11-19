import React from 'react';
import SearchAndFilters from '../components/SearchAndFilters';
import ListingCard from '../components/ListingCard';
import './HomePage.css';


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


export default function HomePage() {
    const [listings, setListings] = React.useState(MOCK_LISTINGS);
    const [filtered, setFiltered] = React.useState(MOCK_LISTINGS);


    const handleSearch = ({ q, city }) => {
        const qLow = q.toLowerCase();
        const cityLow = city.toLowerCase();
        const results = listings.filter(l =>
            (!qLow || l.title.toLowerCase().includes(qLow)) &&
            (!cityLow || l.location.toLowerCase().includes(cityLow))
        );
        setFiltered(results);
    };


    return (
        <div className="home-container">
            <SearchAndFilters onSearch={handleSearch} />
            <div className="listing-grid">
                {filtered.map(item => (<ListingCard key={item.id} item={item} />))}
            </div>
            {filtered.length === 0 && <div className="empty">Нічого не знайдено</div>}
        </div>
    );
}