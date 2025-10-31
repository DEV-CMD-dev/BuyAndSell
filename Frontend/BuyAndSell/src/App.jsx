import { useState } from 'react'
import './App.css'
import AdvertisementPage from "./components/AdvertisementPage/main.jsx";



function App() {
  const [count, setCount] = useState(0)

  return (
    <div>
      <AdvertisementPage />
    </div>
  )
}

export default App
