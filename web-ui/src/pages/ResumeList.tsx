import { useQuery } from '@tanstack/react-query'
import { ResumeApi } from '../api'
import { Button } from '../components/ui/button'
import { Card, CardHeader, CardContent } from '../components/ui/card'
import { Viewer } from '@react-pdf-viewer/core'
import '@react-pdf-viewer/core/lib/styles/index.css'
import { useState } from 'react'

/**
 * Displays resumes fetched from the backend. Selecting a resume shows
 * a PDF preview using @react-pdf-viewer.
 */
export default function ResumeList() {
  const { data } = useQuery({
    queryKey: ['resumes'],
    queryFn: () => new ResumeApi().apiV1ResumesGet(),
  })
  const [selected, setSelected] = useState<string | null>(null)
  return (
    <div className="space-y-4">
      <h1 className="text-2xl font-bold">Resumes</h1>
      <div className="flex space-x-2">
        {data?.split(',').map((name) => (
          <Button key={name} onClick={() => setSelected(`/resumes/${name}.pdf`)}>
            {name}
          </Button>
        ))}
      </div>
      {selected && (
        <Card>
          <CardHeader>Preview</CardHeader>
          <CardContent>
            <div className="h-[600px]">
              <Viewer fileUrl={selected} />
            </div>
          </CardContent>
        </Card>
      )}
    </div>
  )
}
