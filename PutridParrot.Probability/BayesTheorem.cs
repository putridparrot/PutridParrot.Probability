using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability;

/// <summary>
/// Provides methods for applying Bayes' Theorem to probability calculations
/// </summary>
public static class BayesTheorem
{
    /// <summary>
    /// Bayes' Theorem: P(A | B) = P(B | A) * P(A) / P(B)
    /// </summary>
    /// <param name="pA">Prior probability P(A)</param>
    /// <param name="pBGivenA">Likelihood P(B | A)</param>
    /// <param name="pB">Evidence P(B)</param>
    /// <example>
    /// Medical test accuracy:
    /// P(Disease | Positive) = P(Positive | Disease) * P(Disease) / P(Positive)
    /// </example>
    /// <exception cref="DivideByZeroException">Thrown when P(B) = 0</exception>
    public static P Calculate(P pA, P pBGivenA, P pB)
    {
        Guard.ThrowIfZero(pB.Value, "Cannot apply Bayes' theorem when P(B) = 0");
        return new(pBGivenA.Value * pA.Value / pB.Value);
    }
}
