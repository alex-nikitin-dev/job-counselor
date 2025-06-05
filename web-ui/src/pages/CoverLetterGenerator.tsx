import { useState } from 'react'
import { CoverLetterApi } from '../api'
import { Textarea } from '../components/ui/textarea'
import { Button } from '../components/ui/button'
import { Card, CardHeader, CardContent } from '../components/ui/card'

/**
 * Allows the user to generate a cover letter using free form text
 * that is submitted to the backend API.
 */
export default function CoverLetterGenerator() {
  const [prompt, setPrompt] = useState('')
  const [result, setResult] = useState('')

  async function generate() {
    const text = await new CoverLetterApi().apiV1CoverlettersGet()
    setResult(text)
  }

  return (
    <Card className="max-w-xl space-y-4">
      <CardHeader>
        <h1 className="text-xl font-bold">Cover Letter Generator</h1>
      </CardHeader>
      <CardContent className="space-y-4">
        <Textarea value={prompt} onChange={(e) => setPrompt(e.target.value)} placeholder="Describe the position" />
        <Button onClick={generate}>Generate</Button>
        {result && <pre className="whitespace-pre-wrap">{result}</pre>}
      </CardContent>
    </Card>
  )
}
