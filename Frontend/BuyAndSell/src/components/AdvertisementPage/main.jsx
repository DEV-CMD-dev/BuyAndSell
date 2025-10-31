import React, { useState, useEffect } from 'react';
import './index.css';

function AdvertisementPage() {
  const [ad, setAd] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    //---------------------------test--------------------
    setTimeout(() => {
      setAd({
        id: 1,
        title: 'title',
        price: 1000000000000,
        description: 'description',
        images: [
          '/no-image.png',
          '/no-image.png',
          '/no-image.png'
        ],
        user: {
          name: 'name',
          city: 'city',
          phone: 'phone',
          email: 'email'
        },
        createdAt: new Date()
      });
      setLoading(false);
    }, 500);
    //----------------------------------------------------------
  }, []);

  if (loading) return <div>Loading...</div>;
  if (!ad) return <div>Оголошення не знайдено</div>;

  return (
    <div className="page">
      <h1 className="title">{ad.title}</h1>
      <div className="date">Опубліковано: {new Date(ad.createdAt).toLocaleDateString('uk-UA')}</div>
      
      <div className="gallery">
        {ad.images.map((img, idx) => (
          <img key={idx} src={img} alt={`${ad.title} ${idx+1}`} className="gallery-image"/>
        ))}
      </div>

      <div className="description">
        <h2>Опис товару</h2>
        <p>{ad.description}</p>
        <div className="price">{ad.price} ₴</div>
      </div>

      <div className="contacts">
        <h2>Контакти продавця</h2>
        <p>Ім'я: {ad.user.name}</p>
        <p>Місто: {ad.user.city}</p>
        <p>Телефон: {ad.user.phone}</p>
        <p>Email: {ad.user.email}</p>
      </div>
    </div>
  );
}

export default AdvertisementPage;
