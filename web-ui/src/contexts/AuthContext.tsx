import React, { createContext, useContext, useState, useEffect } from 'react'
import { ProfileApi } from '../api/apis/ProfileApi'

interface AuthState {
  user?: string
  loading: boolean
  login: (email: string, password: string) => Promise<void>
  logout: () => Promise<void>
}

const AuthContext = createContext<AuthState | null>(null)

/**
 * Provides authentication state and helpers to descendant components.
 * Handles login, logout and silent refresh via backend endpoints that
 * use HttpOnly cookies to store the JWT.
 */
export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<string>()
  const [loading, setLoading] = useState(true)

  // Called to refresh the session silently on mount
  useEffect(() => {
    refresh().finally(() => setLoading(false))
  }, [])

  const profileApi = new ProfileApi()

  async function refresh() {
    try {
      // Backend should refresh the JWT using the cookie
      await fetch('/api/v1/auth/refresh', { credentials: 'include' })
      // Optionally load user profile
      const me = await profileApi.apiV1ProfilesGet()
      setUser(me)
    } catch {
      setUser(undefined)
    }
  }

  async function login(email: string, password: string) {
    setLoading(true)
    await fetch('/api/v1/auth/login', {
      method: 'POST',
      credentials: 'include',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password }),
    })
    await refresh()
    setLoading(false)
  }

  async function logout() {
    setLoading(true)
    await fetch('/api/v1/auth/logout', {
      method: 'POST',
      credentials: 'include',
    })
    setUser(undefined)
    setLoading(false)
  }

  return (
    <AuthContext.Provider value={{ user, loading, login, logout }}>
      {children}
    </AuthContext.Provider>
  )
}

/**
 * Hook to access authentication state.
 */
export function useAuth() {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('AuthProvider missing')
  return ctx
}
