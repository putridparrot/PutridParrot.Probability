using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability;

/// <summary>
/// Provides combinatorial calculation utilities for probability theory
/// </summary>
public static class Combinatorics
{
    /// <summary>
    /// Calculate factorial: n!
    /// </summary>
    /// <param name="n">Non-negative integer</param>
    /// <returns>n! = n × (n-1) × ... × 1</returns>
    /// <exception cref="ArgumentException">Thrown when n is negative</exception>
    /// <exception cref="OverflowException">Thrown when result exceeds long.MaxValue</exception>
    public static long Factorial(int n)
    {
        Guard.ThrowIfNegative(n, nameof(n), "Factorial is not defined for negative numbers");

        if (n <= 1)
            return 1;

        checked
        {
            long result = 1;
            for (var i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }

    /// <summary>
    /// Calculate permutations: nPr = n! / (n-r)!
    /// Number of ways to arrange r items from n items (order matters)
    /// </summary>
    /// <param name="n">Total number of items</param>
    /// <param name="r">Number of items to arrange</param>
    /// <returns>Number of permutations</returns>
    /// <exception cref="ArgumentException">Thrown when n or r is negative, or r > n</exception>
    public static long Permutations(int n, int r)
    {
        Guard.ThrowIfNegative(n, nameof(n));
        Guard.ThrowIfNegative(r, nameof(r));
        Guard.ThrowIfGreaterThan(r, n, nameof(r), nameof(n));

        if (r == 0)
            return 1;

        checked
        {
            long result = 1;
            for (var i = n; i > n - r; i--)
            {
                result *= i;
            }
            return result;
        }
    }

    /// <summary>
    /// Calculate combinations: nCr = n! / (r! × (n-r)!)
    /// Number of ways to choose r items from n items (order doesn't matter)
    /// </summary>
    /// <param name="n">Total number of items</param>
    /// <param name="r">Number of items to choose</param>
    /// <returns>Number of combinations</returns>
    /// <exception cref="ArgumentException">Thrown when n or r is negative, or r > n</exception>
    public static long Combinations(int n, int r)
    {
        Guard.ThrowIfNegative(n, nameof(n));
        Guard.ThrowIfNegative(r, nameof(r));
        Guard.ThrowIfGreaterThan(r, n, nameof(r), nameof(n));

        if (r == 0 || r == n)
            return 1;

        // Optimize by using the smaller of r or n-r
        r = Math.Min(r, n - r);

        checked
        {
            long result = 1;
            for (var i = 0; i < r; i++)
            {
                result = result * (n - i) / (i + 1);
            }
            return result;
        }
    }

    /// <summary>
    /// Calculate binomial coefficient (same as Combinations)
    /// </summary>
    public static long BinomialCoefficient(int n, int k) => Combinations(n, k);
}
