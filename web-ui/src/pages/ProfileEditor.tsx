import { useEffect, useState } from 'react'
import { ProfileApi } from '../api'
import type { ApiV1ProfilesPostRequest } from '../api'
import { Input } from '../components/ui/input'
import { Textarea } from '../components/ui/textarea'
import { Button } from '../components/ui/button'
import { Card, CardHeader, CardContent } from '../components/ui/card'

/**
 * Page with a simple form allowing the user to edit their profile
 * and save it via the backend API.
 */
export default function ProfileEditor() {
  const [form, setForm] = useState<ApiV1ProfilesPostRequest>({
    fullName: '',
    email: '',
    phone: '',
    summary: '',
  })
  const [status, setStatus] = useState<string>()

  useEffect(() => {
    // Load existing profile on mount
    new ProfileApi()
      .apiV1ProfilesGet()
      .then(() => {
        // placeholder since real API just returns a string
      })
      .catch(console.error)
  }, [])

  function handleChange(e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    const { name, value } = e.target
    setForm((f) => ({ ...f, [name]: value }))
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    try {
      await new ProfileApi().apiV1ProfilesPost({ apiV1ProfilesPostRequest: form })
      setStatus('Saved!')
    } catch {
      setStatus('Failed to save')
    }
  }

  return (
    <Card className="max-w-xl mx-auto">
      <CardHeader>
        <h1 className="text-xl font-bold">Edit Profile</h1>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="space-y-4">
          <Input name="fullName" placeholder="Full name" value={form.fullName} onChange={handleChange} />
          <Input name="email" placeholder="Email" value={form.email} onChange={handleChange} />
          <Input name="phone" placeholder="Phone" value={form.phone} onChange={handleChange} />
          <Textarea name="summary" placeholder="Professional summary" value={form.summary} onChange={handleChange} />
          <Button type="submit">Save</Button>
          {status && <p>{status}</p>}
        </form>
      </CardContent>
    </Card>
  )
}
