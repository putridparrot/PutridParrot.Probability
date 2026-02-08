using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability.Distributions;

/// <summary>
/// Normal (Gaussian) distribution approximations
/// </summary>
public static class Normal
{
    /// <summary>
    /// Calculate probability density function (PDF) at x
    /// f(x) = (1 / (σ√(2π))) × e^(-(x-μ)²/(2σ²))
    /// </summary>
    /// <param name="x">Value to evaluate</param>
    /// <param name="mean">Mean (μ)</param>
    /// <param name="stdDev">Standard deviation (σ)</param>
    /// <returns>Probability density at x</returns>
    public static float Pdf(float x, float mean, float stdDev)
    {
        Guard.ThrowIfNotPositive(stdDev, nameof(stdDev), "Standard deviation must be positive");

        var coefficient = 1.0f / (stdDev * MathF.Sqrt(2 * MathF.PI));
        var exponent = -MathF.Pow(x - mean, 2) / (2 * stdDev * stdDev);
        return coefficient * MathF.Exp(exponent);
    }

    /// <summary>
    /// Approximate cumulative distribution function (CDF) using error function
    /// P(X ≤ x) ≈ 0.5 × (1 + erf((x - μ) / (σ√2)))
    /// </summary>
    /// <param name="x">Upper bound</param>
    /// <param name="mean">Mean (μ)</param>
    /// <param name="stdDev">Standard deviation (σ)</param>
    /// <returns>Approximate probability that X ≤ x</returns>
    public static P Cdf(float x, float mean, float stdDev)
    {
        Guard.ThrowIfNotPositive(stdDev, nameof(stdDev), "Standard deviation must be positive");

        var z = (x - mean) / (stdDev * MathF.Sqrt(2));
        return new P(0.5f * (1 + Erf(z)));
    }

    /// <summary>
    /// Standard normal Z-score: z = (x - μ) / σ
    /// </summary>
    public static float ZScore(float x, float mean, float stdDev)
    {
        Guard.ThrowIfNotPositive(stdDev, nameof(stdDev), "Standard deviation must be positive");

        return (x - mean) / stdDev;
    }

    /// <summary>
    /// Error function approximation using Abramowitz and Stegun formula
    /// </summary>
    private static float Erf(float x)
    {
        // Constants for approximation
        const float a1 = 0.254829592f;
        const float a2 = -0.284496736f;
        const float a3 = 1.421413741f;
        const float a4 = -1.453152027f;
        const float a5 = 1.061405429f;
        const float p = 0.3275911f;

        var sign = x < 0 ? -1 : 1;
        x = MathF.Abs(x);

        var t = 1.0f / (1.0f + p * x);
        var y = 1.0f - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * MathF.Exp(-x * x);

        return sign * y;
    }
}
