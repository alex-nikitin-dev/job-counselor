import { Link, Outlet } from 'react-router-dom'

/**
 * Application layout with navigation bar and routed content.
 */
export default function App() {
  return (
    <div className="min-h-screen flex flex-col">
      <nav className="bg-gray-800 text-white p-4">
        <ul className="flex space-x-4">
          <li><Link to="/">Home</Link></li>
          <li><Link to="/about">About</Link></li>
          <li><Link to="/dashboard">Dashboard</Link></li>
          <li><Link to="/profile">Profile</Link></li>
          <li><Link to="/resumes">Resumes</Link></li>
          <li><Link to="/cover-letter">Cover Letter</Link></li>
          <li><Link to="/jobs">Jobs</Link></li>
        </ul>
      </nav>
      <main className="flex-grow p-4">
        <Outlet />
      </main>
    </div>
  )
}
