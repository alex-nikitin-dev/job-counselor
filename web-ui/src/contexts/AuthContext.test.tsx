import { render, screen, waitFor } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { AuthProvider, useAuth } from './AuthContext'

/**
 * Simple component that exposes auth state for testing.
 */
function TestComponent() {
  const { user, loading, login } = useAuth()
  return (
    <div>
      <span>{loading ? 'loading' : user ?? 'none'}</span>
      <button onClick={() => login('a@test.com', 'pw')}>login</button>
    </div>
  )
}

describe('AuthProvider', () => {
  const fetchMock = vi.fn()

  beforeEach(() => {
    vi.stubGlobal('fetch', fetchMock)
  })

  afterEach(() => {
    vi.restoreAllMocks()
  })

  it('logs in and sets user', async () => {
    fetchMock.mockImplementation((url: string) => {
      if (url.includes('profiles')) {
        return Promise.resolve(
          new Response(JSON.stringify('john'), {
            status: 200,
            headers: { 'Content-Type': 'application/json' },
          }),
        )
      }
      return Promise.resolve(new Response(null, { status: 200 }))
    })

    render(
      <AuthProvider>
        <TestComponent />
      </AuthProvider>,
    )

    // Trigger login
    await userEvent.click(screen.getByRole('button', { name: 'login' }))

    await waitFor(() => expect(screen.getByText('john')).toBeInTheDocument())
  })
})
