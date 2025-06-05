import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

import './index.css'
import App from './App'
import Home from './pages/Home'
import About from './pages/About'
import Dashboard from './pages/Dashboard'
import ProfileEditor from './pages/ProfileEditor'
import ResumeList from './pages/ResumeList'
import CoverLetterGenerator from './pages/CoverLetterGenerator'
import JobTrackerKanban from './pages/JobTrackerKanban'
import { AuthProvider } from './contexts/AuthContext'

const queryClient = new QueryClient()

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<App />}>
              <Route index element={<Home />} />
              <Route path="about" element={<About />} />
              <Route path="dashboard" element={<Dashboard />} />
              <Route path="profile" element={<ProfileEditor />} />
              <Route path="resumes" element={<ResumeList />} />
              <Route path="cover-letter" element={<CoverLetterGenerator />} />
              <Route path="jobs" element={<JobTrackerKanban />} />
            </Route>
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </QueryClientProvider>
  </StrictMode>
)
