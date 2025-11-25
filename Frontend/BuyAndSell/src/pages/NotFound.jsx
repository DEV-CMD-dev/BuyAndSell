import "./NotFound.css"
import { Link } from 'react-router-dom';
export default function NotFound(){
    return (
        <div className="page-not-found">
            <h2>Сторінку не знайдено</h2>
            <Link to="/" className="back-link">На головну</Link>
        </div>
    )
}