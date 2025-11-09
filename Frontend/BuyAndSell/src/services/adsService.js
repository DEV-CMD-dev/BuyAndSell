const USE_MOCK = true;


const MOCK_ADS = [
  {
    id: '1',
    title: 'Ноутбук Lenovo IdeaPad',
    price: '12 500₴',
    location: 'Київ',
    description:
      'Продам ноутбук у відмінному стані. Intel Core i5, 8GB RAM, 512GB SSD.',
    image: 'https://content2.rozetka.com.ua/goods/images/big/465898060.jpg',
    images: [
      'https://content2.rozetka.com.ua/goods/images/big/465898060.jpg',
      'https://content.rozetka.com.ua/goods/images/big/465898061.jpg',
    ],
    createdAt: new Date(Date.now() - 2 * 24 * 60 * 60 * 1000).toISOString(),
    seller: { name: 'Олександр', phone: '+380991234567', rating: 4.8 },
  },
  {
    id: '2',
    title: 'Велосипед CITY 28"',
    price: '3 200₴',
    location: 'Львів',
    description: 'Міський велосипед 28", гарний стан.',
    image: 'https://content1.rozetka.com.ua/goods/images/big/430562622.jpg',
    images: [
      'https://content1.rozetka.com.ua/goods/images/big/430562622.jpg',
      'https://content1.rozetka.com.ua/goods/images/big/430562623.jpg',
    ],
    createdAt: new Date(Date.now() - 5 * 24 * 60 * 60 * 1000).toISOString(),
    seller: { name: 'Іван', phone: '+380671112233', rating: 4.5 },
  },
  {
    id: '3',
    title: 'Смартфон Galaxy A52',
    price: '6 800₴',
    location: 'Одеса',
    description: 'Стан відмінний, повний комплект.',
    image: 'https://content.rozetka.com.ua/goods/images/big/523604275.jpg',
    images: [
      'https://content.rozetka.com.ua/goods/images/big/523604275.jpg',
      'https://content.rozetka.com.ua/goods/images/big/523604276.jpg',
    ],
    createdAt: new Date(Date.now() - 10 * 24 * 60 * 60 * 1000).toISOString(),
    seller: { name: 'Марія', phone: '+380931234567', rating: 4.9 },
  },
];

const API_BASE = '/api';

const delay = (ms) => new Promise((res) => setTimeout(res, ms));

export async function getAdById(id) {
  if (USE_MOCK) {
    await delay(150);
    if (!id) return MOCK_ADS[0];
    return MOCK_ADS.find((a) => a.id === String(id)) || null;
  }
  const resp = await fetch(`${API_BASE}/ads/${id}`);
  if (!resp.ok) throw new Error('Failed to fetch ad');
  return resp.json();
}

export async function getAdsList() {
  if (USE_MOCK) {
    await delay(150);
    return MOCK_ADS;
  }
  const resp = await fetch(`${API_BASE}/ads`);
  if (!resp.ok) throw new Error('Failed to fetch ads');
  return resp.json();
}
