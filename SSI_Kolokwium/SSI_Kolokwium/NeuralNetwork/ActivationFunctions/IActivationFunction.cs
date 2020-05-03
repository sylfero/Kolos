namespace SSI_Kolokwium.NeuralNetwork.ActivationFunctions
{
    public interface IActivationFunction
    {
        double Calculate(double input);

        double Derivative(double input);
    }
}
