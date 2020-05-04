using SSI_Kolokwium.NeuralNetwork;
using SSI_Kolokwium.NeuralNetwork.ActivationFunctions;
using System;

namespace SSI_Kolokwium
{
    class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Zadanie 1
            /// </summary>
            //Get data from file, shuffle it and normalize
            double[][] seeds = Data.Get("seeds.csv");
            seeds.Shuffle();
            seeds.Normalize();

            /// <summary>
            /// Zadanie 2
            /// </summary>
            //Line form file it belongs to Seed 1
            double[] test = { 15.26, 14.84, 0.871, 5.763, 3.312, 2.221, 5.22 };
            test.Normalize();
            //Knn.Classify(test data, train data, number of outputs, k)
            Console.WriteLine($"Predicted class: {Knn.Classify(test, seeds, 3, 4)}");

            /// <summary>
            /// Zadanie 3
            /// </summary>
            //Partition seeds into two parts (70% and 30%)
            double[][][] partition = seeds.Partitioner();

            //Separate train data into inputs and expected values
            double[][] trainInput = Data.GetInputs(partition[0]);
            double[][] trainExpected = Data.GetOutputs(partition[0]);

            //Separate test data into inputs and expected values
            double[][] testInput = Data.GetInputs(partition[1]);
            double[][] testExpected = Data.GetOutputs(partition[1]);

            //Create Neural network
            Network network = new Network(0.1, new SigmoidActivationFunction(), 7, 5, 5, 5, 5, 3);
            //Train network
            network.Train(trainInput, trainExpected, 10_000);
            //Test network (due to data being ranorm accuracy can vary but it should be around 90% most of the time)
            Console.WriteLine("Accuracy " + (network.Test(testInput, testExpected) * 100) + "%");
            //Write weights to file
            Serializer.Serialize("weights.txt", network);

            //Create another netowrk
            Network network2 = new Network(0.1, new SigmoidActivationFunction(), 7, 5, 5, 5, 5, 3);
            //Read weights from file into network
            Serializer.Deserialize("weights.txt", network2);
            //It should be the same as for first network
            Console.WriteLine("Accuracy " + (network2.Test(testInput, testExpected) * 100) + "%");
            Console.ReadKey();
        }
    }
}
