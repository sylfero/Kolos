using System;
using System.IO;

namespace SSI_Kolokwium
{
    static class Data
    {
        //Get data from file
        public static double[][] Get(string path)
        {
            //Read all data form file to array
            string[] lines = File.ReadAllLines(path);
            double[][] data = new double[lines.Length - 1][];
            
            //First line has labels so we can skip it
            for (int i = 1; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                //Last digit represtents seed type (1, 2 or 3) so we must make the array longer
                data[i - 1] = new double[tmp.Length + 2];

                //Gather data
                for (int j = 0; j < tmp.Length - 1; j++)
                {
                    data[i - 1][j] = double.Parse(tmp[j]);
                }
                

                //Fill last 3 elements with 0
                for (int j = data[i - 1].Length - 3; j < data[i - 1].Length; j++)
                {
                    data[i - 1][j] = 0;
                }
                //Based on type of seed place 1 in right position
                data[i - 1][(tmp.Length  - 1) + int.Parse(tmp[tmp.Length - 1]) - 1] = 1;
            }

            return data;
        }

        //Shuffle data in array
        public static void Shuffle(this double[][] data)
        {
            Random rnd = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                double[] tmp = data[i];
                int r = rnd.Next(i, data.Length);
                data[i] = data[r];
                data[r] = tmp;
            }
        }

        //Normalize data in array
        public static void Normalize(this double[][] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                double max = data[i][0];
                double min = data[i][0];

                //Find min and max value
                for (int j = 1; j < data[i].Length - 3; j++)
                {
                    if (data[i][j] > max)
                    {
                        max = data[i][j];
                    }
                    else if (data[i][j] < min)
                    {
                        min = data[i][j];
                    }
                }

                //Normalize values
                for (int j = 0; j < data[i].Length - 3; j++)
                {
                    data[i][j] = (data[i][j] - min) / (max - min);
                }
            }
        }

        //Normalize but for single line (only input values without result) 
        public static void Normalize(this double[] data)
        {
            double max = data[0];
            double min = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
                else if (data[i] < min)
                {
                    min = data[i];
                }
            }

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (data[i] - min) / (max - min);
            }
        }

        //From array get only values describing seeds
        public static double[][] GetInputs(this double[][] data)
        {
            double[][] outArray = (double[][])data.Clone();
            for (int i = 0; i < data.Length; i++)
            {
                Array.Resize(ref outArray[i], outArray[i].Length - 3);
            }

            return outArray;
        }

        //From array get only types of seeds
        public static double[][] GetOutputs(this double[][] data)
        {
            double[][] outArray = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                outArray[i] = new double[3];
                for (int j = data[i].Length - 3; j < data[i].Length; j++)
                {
                    outArray[i][j - data[i].Length + 3] = data[i][j];
                }
            }

            return outArray;
        }

        //Separate array into two parts (70% and 30%)
        public static double[][][] Partitioner(this double[][] data)
        {
            double[][][] outArray = new double[2][][];
            outArray[0] = new double[(int)(data.Length * 0.7)][];
            outArray[1] = new double[data.Length - (int)(data.Length * 0.7)][];

            //70% of data
            for (int i = 0; i < (int)(data.Length * 0.7); i++)
            {
                outArray[0][i] = new double[data[i].Length];
                for (int j = 0; j < data[i].Length; j++)
                {
                    outArray[0][i][j] = data[i][j];
                }
            }

            //30% of data
            for (int i = (int)(data.Length * 0.7); i < data.Length; i++)
            {
                outArray[1][i - (int)(data.Length * 0.7)] = new double[data[i].Length];
                for (int j = 0; j < data[i].Length; j++)
                {
                    outArray[1][i- (int)(data.Length * 0.7)][j] = data[i][j];
                }
            }

            return outArray;
        }
    }
}