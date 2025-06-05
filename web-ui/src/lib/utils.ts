// Utility function used by shadcn/ui components
import { clsx, type ClassValue } from 'clsx'
import { twMerge } from 'tailwind-merge'

/**
 * Concatenate classes conditionally using `clsx` and merge Tailwind classes
 * with `tailwind-merge`.
 */
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}
