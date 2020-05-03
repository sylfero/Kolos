using SSI_Kolokwium.NeuralNetwork;
using SSI_Kolokwium.NeuralNetwork.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI_Kolokwium
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] seeds = Data.Get("seeds.csv");
            seeds.Shuffle();
            seeds.Normalize();
            double[] test = { 15.26, 14.84, 0.871, 5.763, 3.312, 2.221, 5.22 };
            test.Normalize();
            Console.WriteLine($"Predicted class: {Knn.Classify(test, seeds, 3, 4)}");
            
            double[][][] partition = seeds.Partitioner();

            double[][] trainInput = Data.GetInputs(partition[0]);
            double[][] trainExpected = Data.GetOutputs(partition[0]);

            double[][] testInput = Data.GetInputs(partition[1]);
            double[][] testExpected = Data.GetOutputs(partition[1]);

            Network network = new Network(1, new SigmoidActivationFunction(), 7, 7, 5, 5, 5, 3);
            network.Train(trainInput, trainExpected, 10_000);
            Console.WriteLine(network.Test(testInput, testExpected));
            Console.ReadKey();
        }
    }
}
