export default [
  { ignores: ['node_modules', 'dist'] },
  (await import('@eslint/js')).default.configs.recommended,
  {
    files: ['**/*.{js,mjs,cjs,ts,tsx,mts,cts}'],
    languageOptions: {
      globals: (await import('globals')).browser,
      ecmaVersion: 2020,
      sourceType: 'module'
    },
    rules: {
      'no-console': 'off',
      'no-debugger': 'off',
      'padded-blocks': 'off',
      'quotes': ['error', 'single', { avoidEscape: true, allowTemplateLiterals: true }],
      'semi': ['error', 'always'],
      'space-before-function-paren': ['error', {
        'anonymous': 'always',
        'named': 'never',
        'asyncArrow': 'always'
      }]
    }
  },
  ...((await import('typescript-eslint')).default.configs.recommended),
  {
    files: ['**/*.{js,mjs,cjs,ts,tsx,jsx}'],
    plugins: {
      react: (await import('eslint-plugin-react')).default,
      'react-hooks': (await import('eslint-plugin-react-hooks')).default
    },
    settings: {
      react: { version: 'detect' }
    },
    rules: {
      // New JSX transform doesn't need React in scope
      'react/react-in-jsx-scope': 'off',
      'react/jsx-uses-react': 'off',
      // Hooks rules
      'react-hooks/rules-of-hooks': 'error',
      'react-hooks/exhaustive-deps': 'warn'
    }
  }
];
