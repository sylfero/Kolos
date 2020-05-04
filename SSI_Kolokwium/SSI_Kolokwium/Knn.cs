using System;

namespace SSI_Kolokwium
{
    static class Knn
    {
        public static string Classify(double[] input, double[][] trainData, int numClasses, int k)
        {
            IndexAndDistance[] info = new IndexAndDistance[trainData.Length];

            //Calculate distance for each element in trainData array and input
            for (int i = 0; i < trainData.Length; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(input, trainData[i]);
                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;
            }

            //Sort distances
            Array.Sort(info);

            //Display k nearest elements to input
            for (int i = 0; i < k; i++)
            {
                string c = trainData[info[i].idx][trainData[0].Length - 1] == 1 ? "Seed 3" : (trainData[info[i].idx][trainData[0].Length - 2] == 1 ? "Seed 2" : "Seed 1");
                double dist = info[i].dist;

                Console.Write("Nearest: (");

                for (int j = 0; j < trainData[info[i].idx].Length - 4; j++)
                {
                    Console.Write("{0:0.000}, ", trainData[info[i].idx][j]);
                }

                Console.Write("{0:0.000}) Distance: {1:0.000} Class: {2}", trainData[info[i].idx][trainData[info[i].idx].Length - 4], dist, c);
                Console.WriteLine();
            }

            //Get most possible class of input data
            string result = Vote(info, trainData, numClasses, k);

            return result;
        }

        private static double Distance(double[] input, double[] data)
        {
            double sum = 0;

            for (int i = 0; i < input.Length; i++)
                sum += Math.Abs(input[i] - data[i]);

            return sum;
        }

        static string Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
        {
            int[] votes = new int[numClasses];

            //Count how many times each class occurs in k nearest elements
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
        

        //We need distance and index of each element cuz we will be sorting elements by distance but still need to locate elements in input array
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
