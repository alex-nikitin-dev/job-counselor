import { defineConfig } from 'vitest/config'
import react from '@vitejs/plugin-react'

// Vitest configuration enabling jsdom environment
export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    setupFiles: './vitest.setup.ts',
  },
})
