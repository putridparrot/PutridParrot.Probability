using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability.Distributions;

/// <summary>
/// Poisson distribution: P(X = k) = (λ^k × e^(-λ)) / k!
/// Models the number of events occurring in a fixed interval
/// </summary>
public static class Poisson
{
    /// <summary>
    /// Calculate probability of exactly k events occurring
    /// </summary>
    /// <param name="k">Number of events</param>
    /// <param name="lambda">Average rate (λ) - expected number of events</param>
    /// <returns>Probability of exactly k events</returns>
    /// <exception cref="ArgumentException">Thrown when parameters are invalid</exception>
    public static P Probability(int k, float lambda)
    {
        Guard.ThrowIfNegative(k, nameof(k), "Number of events must be non-negative");
        Guard.ThrowIfNotPositive(lambda, nameof(lambda), "Lambda must be positive");

        var numerator = MathF.Pow(lambda, k) * MathF.Exp(-lambda);
        var denominator = Combinatorics.Factorial(k);

        return new P(numerator / denominator);
    }

    /// <summary>
    /// Calculate cumulative probability of k or fewer events
    /// </summary>
    public static P CumulativeProbability(int k, float lambda)
    {
        Guard.ThrowIfNegative(k, nameof(k), "Number of events must be non-negative");
        Guard.ThrowIfNotPositive(lambda, nameof(lambda), "Lambda must be positive");

        float sum = 0;
        for (int i = 0; i <= k; i++)
        {
            sum += Probability(i, lambda).Value;
        }
        return new P(sum);
    }

    /// <summary>
    /// Calculate expected value (mean): E[X] = λ
    /// </summary>
    public static float Mean(float lambda) => lambda;

    /// <summary>
    /// Calculate variance: Var[X] = λ
    /// </summary>
    public static float Variance(float lambda) => lambda;

    /// <summary>
    /// Calculate standard deviation: σ = √λ
    /// </summary>
    public static float StandardDeviation(float lambda) => MathF.Sqrt(lambda);
}
