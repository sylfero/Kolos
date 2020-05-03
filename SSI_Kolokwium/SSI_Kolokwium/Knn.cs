using System;

namespace SSI_Kolokwium
{
    static class Knn
    {
        public static string Classify(double[] unknown, double[][] trainData, int numClasses, int k)
        {
            int n = trainData.Length;

            IndexAndDistance[] info = new IndexAndDistance[n];

            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(unknown, trainData[i]);
                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;
            }

            Array.Sort(info);

            for (int i = 0; i < k; ++i)
            {
                string c = trainData[info[i].idx][trainData[0].Length - 1] == 1 ? "Seed 3" : (trainData[info[i].idx][trainData[0].Length - 2] == 1 ? "Seed 2" : "Seed 1");
                double dist = info[i].dist;

                Console.Write("Nearest: (");

                for (int j = 0; j < trainData[info[i].idx].Length - 4; j++)
                {
                    Console.Write("{0:0.000}, ", trainData[info[i].idx][j]);
                }

                Console.Write("{0:0.000}) Distance: {1:0.000} Class: {2}", trainData[info[i].idx][trainData[info[i].idx].Length - 4], Math.Round(dist, 3), c);
                Console.WriteLine();
            }

            string result = Vote(info, trainData, numClasses, k);

            return result;
        }

        private static double Distance(double[] unknown, double[] data)
        {
            double sum = 0;

            for (int i = 0; i < unknown.Length; i++)
                sum += Math.Abs(unknown[i] - data[i]);

            return sum;
        }

        static string Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
        {
            int[] votes = new int[numClasses];

            for (int i = 0; i < k; i++)
            {
                int idx = info[i].idx;
                int c = trainData[idx][trainData[0].Length - 1] == 1 ? 2 : (trainData[idx][trainData[0].Length - 2] == 1 ? 1 : 0);
                votes[c]++;
            }

            int mostVotes = 0;
            string classWithMostVotes = "";

            for (int i = 0; i < numClasses; i++)
            {
                if (votes[i] > mostVotes)
                {
                    mostVotes = votes[i];
                    classWithMostVotes = i == 0 ? "Seed 1" : (i == 1 ? "Seed 2" : "Seed 3");
                }
            }

            return classWithMostVotes;
        }

        private class IndexAndDistance : IComparable
        {
            public int idx;
            public double dist;

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                IndexAndDistance other = (IndexAndDistance)obj;

                if (other != null)
                    return dist.CompareTo(other.dist);
                else
                    throw new ArgumentException("Object is not a IndexAndDistance");
            }
        }
    }
}
