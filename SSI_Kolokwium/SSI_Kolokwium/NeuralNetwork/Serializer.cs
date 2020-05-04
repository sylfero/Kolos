using System.Text;
using System.IO;
using System.Linq;

namespace SSI_Kolokwium.NeuralNetwork
{
    public static class Serializer
    {
        //Save wieghts
        public static void Serialize(string path, Network network)
        {
            //Input layer inputs don't need weights
            string[] output = new string[network.Layers.Count - 1];

            for (int i = network.Layers.Count - 1; i > 0; i--)
            {
                StringBuilder builder = new StringBuilder();

                for (int j = 0; j < network.Layers[i].Neurons.Count; j++)
                {
                    for (int k = 0; k < network.Layers[i].Neurons[j].Inputs.Count; k++)
                    {
                        builder.Append(network.Layers[i].Neurons[j].Inputs[k].Weight + ",");
                    }

                    builder.Length--;
                    builder.Append("|");
                }

                builder.Length--;
                output[i - 1] = builder.ToString();
            }

            File.WriteAllLines(path, output.ToList());
        }

        //Read weights
        public static void Deserialize(string path, Network network)
        {
            string[] input = File.ReadAllLines(path);

            for (int i = network.Layers.Count - 1; i > 0; i--)
            {
                string[] neurons = input[i - 1].Split('|');

                for (int j = 0; j < network.Layers[i].Neurons.Count; j++)
                {
                    string[] synapses = neurons[j].Split(',');
                    for (int k = 0; k < network.Layers[i].Neurons[j].Inputs.Count; k++)
                    {
                        network.Layers[i].Neurons[j].Inputs[k].Weight = double.Parse(synapses[k]);
                    }
                }
            }
        }
    }
}
