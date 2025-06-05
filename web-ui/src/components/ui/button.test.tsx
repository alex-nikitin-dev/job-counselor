import { render, screen } from '@testing-library/react'
import { describe, it, expect } from 'vitest'
import { Button } from './button'

// Unit tests for Button component

describe('Button', () => {
  it('renders children text', () => {
    render(<Button>Click</Button>)
    expect(screen.getByRole('button', { name: 'Click' })).toBeInTheDocument()
  })

  it('applies size variant', () => {
    render(<Button size="lg">Big</Button>)
    const btn = screen.getByRole('button', { name: 'Big' })
    expect(btn.className).toContain('h-11')
  })
})
