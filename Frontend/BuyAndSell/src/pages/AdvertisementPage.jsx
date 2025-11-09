import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './ListingPage.css';
import './AdvertisementPage.css';
import { getAdById } from '../services/adsService';

export default function AdvertisementPage() {
    const navigate = useNavigate();
    const { id } = useParams();
    const [ad, setAd] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [currentImage, setCurrentImage] = useState(0);

    const images = Array.isArray(ad?.images) && ad.images.length ? ad.images : ad?.image ? [ad.image] : [];
    const prevImage = () => {
        if (images.length === 0) return;
        setCurrentImage((i) => (i - 1 + images.length) % images.length);
    };
    const nextImage = () => {
        if (images.length === 0) return;
        setCurrentImage((i) => (i + 1) % images.length);
    };

    useEffect(() => {
        let isMounted = true;
        const load = async () => {
            try {
                const data = await getAdById(id);
                if (isMounted) setAd(data);
            } catch (e) {
                setError('Помилка завантаження');
                if (isMounted) setAd(null);
            } finally {
                if (isMounted) setLoading(false);
            }
        };
        load();
        return () => {
            isMounted = false;
        };
    }, [id]);

    if (loading) return <div className="listing-page">Завантаження...</div>;
    if (!ad) return <div className="listing-page">Оголошення не знайдено</div>;

    return (
        <div className="listing-page">
            {images.length > 0 && (
                <div className="image-viewer">
                    <button className="nav-button left" onClick={prevImage}>‹</button>
                    <img
                        src={images[currentImage]}
                        alt={`${ad.title} - фото ${currentImage + 1}`}
                        className="listing-image"
                    />
                    <button className="nav-button right" onClick={nextImage}>›</button>
                    <p className="image-counter">{currentImage + 1} / {images.length}</p>
                </div>
            )}
            <div className="listing-details">
                <h1>{ad.title}</h1>
                <p>{ad.location}</p>
                <p className="price">{ad.price}</p>
                <p>{ad.description}</p>
                <h3>Продавець</h3>
                <p>
                    {ad.seller?.name} | Рейтинг: {ad.seller?.rating} ⭐
                </p>
                <p>{ad.seller?.phone}</p>
                <p className="posted-date">
                    Опубліковано: {new Date(ad.createdAt).toLocaleDateString()}
                </p>
                <button onClick={() => navigate('/')}>Повернутись</button>
                {error && <p style={{ color: 'crimson' }}>{error}</p>}
            </div>
        </div>
    );
}