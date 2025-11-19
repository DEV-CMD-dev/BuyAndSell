import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './adsCreate.css';

const CATEGORIES = ['Електроніка', 'Транспорт', 'Нерухомість', 'Дитячі товари', 'Послуги', 'Інше'];

export default function AdsCreate() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [files, setFiles] = useState([]);
  
  const [form, setForm] = useState({
    title: '',
    category: '',
    price: '',
    location: '',
    description: '',
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleFileChange = (e) => {
    if (e.target.files) {
      setFiles([...files, ...Array.from(e.target.files)]);
    }
  };

  const removeFile = (index) => {
    setFiles(files.filter((_, i) => i !== index));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      const fd = new FormData();
      fd.append('title', form.title);
      fd.append('category', form.category);
      fd.append('price', form.price.toString().replace(',', '.'));
      fd.append('location', form.location);
      fd.append('description', form.description);
      
      files.forEach(file => fd.append('images', file));

      const res = await fetch('/api/ads', { 
        method: 'POST', 
        body: fd 
      });
      
      if (!res.ok) {
        const textData = await res.text();
        let errorMsg = 'Помилка при створенні';
        
        try {
            const json = JSON.parse(textData);
            errorMsg = json.message || JSON.stringify(json);
        } catch {
            errorMsg = textData;
        }
        
        throw new Error(errorMsg || `Статус: ${res.status}`);
      }
      
      const data = await res.json();
      navigate(data.id ? `/ads/${data.id}` : '/');
    } catch (err) {
      console.error(err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="ad-form-container">
      <h1>Нове оголошення</h1>
      {error && <div className="error-msg" style={{color: 'red', marginBottom: '10px'}}>{error}</div>}

      <form className="ad-form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Назва</label>
          <input
            name="title"
            value={form.title}
            onChange={handleChange}
            required
            disabled={loading}
          />
        </div>

        <div className="form-row">
          <div className="form-group">
            <label>Категорія</label>
            <select
              name="category"
              value={form.category}
              onChange={handleChange}
              required
              disabled={loading}
            >
              <option value="">Оберіть...</option>
              {CATEGORIES.map(c => <option key={c} value={c}>{c}</option>)}
            </select>
          </div>
          <div className="form-group">
            <label>Ціна (₴)</label>
            <input
              type="number"
              name="price"
              value={form.price}
              onChange={handleChange}
              required
              disabled={loading}
            />
          </div>
        </div>

        <div className="form-group">
          <label>Місто</label>
          <input
            name="location"
            value={form.location}
            onChange={handleChange}
            disabled={loading}
          />
        </div>

        <div className="form-group">
          <label>Опис</label>
          <textarea
            name="description"
            rows={5}
            value={form.description}
            onChange={handleChange}
            disabled={loading}
          />
        </div>

        <div className="form-group">
          <label>Фотографії</label>
          <div className="file-list">
            {files.map((f, i) => (
              <div key={i} className="file-item">
                <span>{f.name}</span>
                <button type="button" onClick={() => removeFile(i)}>×</button>
              </div>
            ))}
          </div>
          <input
            type="file"
            multiple
            accept="image/*"
            onChange={handleFileChange}
            disabled={loading}
          />
        </div>

        <div className="form-actions">
          <button type="button" onClick={() => navigate(-1)} disabled={loading}>Скасувати</button>
          <button type="submit" className="primary" disabled={loading}>
            {loading ? 'Створення...' : 'Створити'}
          </button>
        </div>
      </form>
    </div>
  );
}