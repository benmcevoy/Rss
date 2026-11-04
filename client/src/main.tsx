import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './App.css'
import App from './App.tsx'
import { client } from './api/rss/client.gen.ts'

client.setConfig({ baseUrl: import.meta.env.VITE_API_BASE_URL })

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
