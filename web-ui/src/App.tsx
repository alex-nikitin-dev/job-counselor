import { Link, Outlet } from 'react-router-dom'

/**
 * Application layout with a simple navigation bar and routed pages.
 */
export default function App() {
  return (
    <div className="min-h-screen flex flex-col">
      {/* Navigation bar shown on all pages */}
      <nav className="bg-gray-800 text-white p-4">
        <ul className="flex space-x-4">
          <li>
            {/* React Router <Link> keeps the app SPA-style */}
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/about">About</Link>
          </li>
          <li>
            <Link to="/dashboard">Dashboard</Link>
          </li>
        </ul>
      </nav>

      {/* Routed page content */}
      <main className="flex-grow p-4">
        <Outlet />
      </main>
    </div>
  )
}
