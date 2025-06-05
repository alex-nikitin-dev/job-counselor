import { useState } from 'react'
import { DndContext, useDroppable, closestCorners } from '@dnd-kit/core'
import type { DragEndEvent } from '@dnd-kit/core'
import { SortableContext, useSortable, verticalListSortingStrategy } from '@dnd-kit/sortable'
import { CSS } from '@dnd-kit/utilities'
import { Card, CardHeader, CardContent } from '../components/ui/card'

interface JobItem {
  id: string
  title: string
}

/**
 * Simple kanban board demo for tracking job applications. Uses dnd-kit
 * to allow drag-and-drop between columns.
 */
export default function JobTrackerKanban() {
  const [columns, setColumns] = useState<Record<string, JobItem[]>>({
    Applied: [
      { id: '1', title: 'Frontend Dev' },
      { id: '2', title: 'Backend Dev' },
    ],
    Interview: [],
    Offer: [],
  })

  function handleDragEnd(event: DragEndEvent) {
    const { active, over } = event
    if (!over || active.id === over.id) return

    const fromCol = active.data.current?.column
    const toCol = over.data.current?.column
    if (!fromCol || !toCol) return

    setColumns((cols) => {
      const fromItems = cols[fromCol]
      const toItems = cols[toCol]
      const item = fromItems.find((i) => i.id === active.id)!
      return {
        ...cols,
        [fromCol]: fromItems.filter((i) => i.id !== active.id),
        [toCol]: [...toItems, item],
      }
    })
  }

  return (
    <DndContext collisionDetection={closestCorners} onDragEnd={handleDragEnd}>
      <div className="flex space-x-4">
        {Object.entries(columns).map(([name, items]) => (
          <Column key={name} id={name} items={items} />
        ))}
      </div>
    </DndContext>
  )
}

function Column({ id, items }: { id: string; items: JobItem[] }) {
  const { setNodeRef } = useDroppable({ id, data: { column: id } })
  return (
    <Card ref={setNodeRef} className="w-60">
      <CardHeader className="font-bold text-center">{id}</CardHeader>
      <CardContent className="space-y-2">
        <SortableContext items={items} strategy={verticalListSortingStrategy}>
          {items.map((item) => (
            <JobCard key={item.id} item={item} column={id} />
          ))}
        </SortableContext>
      </CardContent>
    </Card>
  )
}

function JobCard({ item, column }: { item: JobItem; column: string }) {
  const { attributes, listeners, setNodeRef, transform, transition } = useSortable({
    id: item.id,
    data: { column },
  })
  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  }
  return (
    <div
      ref={setNodeRef}
      style={style}
      {...attributes}
      {...listeners}
      className="p-2 bg-muted rounded-md border cursor-grab"
    >
      {item.title}
    </div>
  )
}
