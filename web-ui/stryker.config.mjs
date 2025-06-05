/**
 * Stryker configuration for mutation testing with Vitest.
 */
export default {
  packageManager: 'npm',
  mutate: ['src/**/*.ts', 'src/**/*.tsx', '!src/**/*.test.ts*'],
  testRunner: 'vitest',
  vitest: {
    project: 'vitest.config.ts',
  },
  reporters: ['html', 'clear-text', 'progress'],
  coverageAnalysis: 'perTest',
}
