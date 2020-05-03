using System.Collections.Generic;
using System.Linq;

namespace SSI_Kolokwium.NeuralNetwork.InputFunctions
{
    public class WeightedSumFunction : IInputFunction
    {
        public double Calculate(List<Synapse> inputs) => inputs.Select(x => x.Weight * x.Output).Sum();
    }
}
