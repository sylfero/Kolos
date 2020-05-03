using System.Collections.Generic;

namespace SSI_Kolokwium.NeuralNetwork.InputFunctions
{
    public interface IInputFunction
    {
        double Calculate(List<Synapse> inputs);
    }
}
