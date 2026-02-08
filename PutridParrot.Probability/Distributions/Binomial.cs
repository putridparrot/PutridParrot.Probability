using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability.Distributions;

/// <summary>
/// Binomial distribution: P(X = k) = nCk × p^k × (1-p)^(n-k)
/// Models the number of successes in n independent trials
/// </summary>
public static class Binomial
{
    /// <summary>
    /// Calculate probability of exactly k successes in n trials
    /// </summary>
    /// <param name="n">Number of trials</param>
    /// <param name="k">Number of successes</param>
    /// <param name="p">Probability of success in a single trial</param>
    /// <returns>Probability of exactly k successes</returns>
    /// <exception cref="ArgumentException">Thrown when parameters are invalid</exception>
    public static P Probability(int n, int k, P p)
    {
        Guard.ThrowIfNegative(n, nameof(n), "Number of trials must be non-negative");
        Guard.ThrowIfNegative(k, nameof(k), "Number of successes must be non-negative");
        Guard.ThrowIfGreaterThan(k, n, nameof(k), nameof(n));

        var nCk = Combinatorics.Combinations(n, k);
        var pk = MathF.Pow(p.Value, k);
        var pnk = MathF.Pow(1 - p.Value, n - k);

        return new P(nCk * pk * pnk);
    }

    /// <summary>
    /// Calculate probability of k or fewer successes in n trials (cumulative)
    /// </summary>
    public static P CumulativeProbability(int n, int k, P p)
    {
        Guard.ThrowIfNegative(n, nameof(n), "Number of trials must be non-negative");
        Guard.ThrowIfNegative(k, nameof(k), "Number of successes must be non-negative");
        Guard.ThrowIfGreaterThan(k, n, nameof(k), nameof(n));

        float sum = 0;
        for (var i = 0; i <= k; i++)
        {
            sum += Probability(n, i, p).Value;
        }
        return new P(sum);
    }

    /// <summary>
    /// Calculate expected value (mean): E[X] = n × p
    /// </summary>
    public static float Mean(int n, P p) => n * p.Value;

    /// <summary>
    /// Calculate variance: Var[X] = n × p × (1 - p)
    /// </summary>
    public static float Variance(int n, P p) => n * p.Value * (1 - p.Value);

    /// <summary>
    /// Calculate standard deviation: σ = √(n × p × (1 - p))
    /// </summary>
    public static float StandardDeviation(int n, P p) => MathF.Sqrt(Variance(n, p));
}
