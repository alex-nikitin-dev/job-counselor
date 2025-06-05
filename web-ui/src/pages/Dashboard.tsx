import { useQuery } from '@tanstack/react-query'
import { AnalyticsApi } from '../api'
import { Line } from 'react-chartjs-2'
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js'

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)

/**
 * Displays analytics information using charts powered by Chart.js.
 */
export default function Dashboard() {
  const { data } = useQuery({
    queryKey: ['analytics'],
    queryFn: () => new AnalyticsApi().apiV1AnalyticsGet(),
  })

  const chartData = {
    labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri'],
    datasets: [
      {
        label: 'Jobs applied',
        data: [1, 2, 1, 3, 2],
        borderColor: 'rgb(53, 162, 235)',
      },
    ],
  }

  return (
    <div className="space-y-4">
      <h1 className="text-3xl font-bold">Dashboard</h1>
      <Line data={chartData} />
      {data && <p className="text-muted-foreground">{data}</p>}
    </div>
  )
}
