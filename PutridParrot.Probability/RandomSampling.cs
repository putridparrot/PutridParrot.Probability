using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability;

/// <summary>
/// Extension methods for random sampling based on probabilities
/// </summary>
public static class RandomSampling
{
    private static readonly Random DefaultRandom = new();

    /// <param name="p">Probability of returning true</param>
    extension(P p)
    {
        /// <summary>
        /// Generate a random boolean based on probability
        /// </summary>
        /// <param name="random">Optional random number generator</param>
        /// <returns>True with probability p, False with probability (1-p)</returns>
        public bool Sample(Random? random = null)
        {
            random ??= DefaultRandom;
            return random.NextDouble() < p.Value;
        }

        /// <summary>
        /// Generate multiple random samples
        /// </summary>
        /// <param name="count">Number of samples to generate</param>
        /// <param name="random">Optional random number generator</param>
        /// <returns>Collection of boolean samples</returns>
        public IEnumerable<bool> Sample(int count, Random? random = null)
        {
            Guard.ThrowIfNegative(count, nameof(count), "Count must be non-negative");

            random ??= DefaultRandom;
            for (var i = 0; i < count; i++)
            {
                yield return p.Sample(random);
            }
        }

        /// <summary>
        /// Count successes in multiple samples (useful for simulation)
        /// </summary>
        public int CountSuccesses(int trials, Random? random = null)
        {
            Guard.ThrowIfNegative(trials, nameof(trials), "Trials must be non-negative");

            return p.Sample(trials, random).Count(success => success);
        }

        /// <summary>
        /// Estimate probability through simulation (Monte Carlo)
        /// </summary>
        /// <param name="samples">Number of samples to take</param>
        /// <param name="random">Optional random number generator</param>
        /// <returns>Estimated probability based on sampling</returns>
        public P EstimateThroughSampling(int samples, Random? random = null)
        {
            Guard.ThrowIfNotPositive(samples, nameof(samples), "Samples must be positive");

            var successes = p.CountSuccesses(samples, random);
            return new P(successes, samples);
        }
    }

    /// <summary>
    /// Sample from a discrete distribution
    /// </summary>
    /// <typeparam name="T">Type of outcome</typeparam>
    /// <param name="outcomes">Collection of (outcome, probability) pairs</param>
    /// <param name="random">Optional random number generator</param>
    /// <returns>Randomly selected outcome based on probabilities</returns>
    public static T SampleFrom<T>(IEnumerable<(T outcome, P probability)> outcomes, Random? random = null)
    {
        var outcomesList = outcomes.ToList();

        if (!outcomesList.Any())
        {
            throw new ArgumentException("Outcomes cannot be empty", nameof(outcomes));
        }

        random ??= DefaultRandom;
        var randomValue = (float)random.NextDouble();
        var cumulative = 0.0f;

        foreach (var (outcome, probability) in outcomesList)
        {
            cumulative += probability.Value;
            if (randomValue <= cumulative)
                return outcome;
        }

        return outcomesList.Last().outcome;
    }

    /// <summary>
    /// Generate multiple samples from a discrete distribution
    /// </summary>
    public static IEnumerable<T> SampleFrom<T>(
        IEnumerable<(T outcome, P probability)> outcomes,
        int count,
        Random? random = null)
    {
        Guard.ThrowIfNegative(count, nameof(count), "Count must be non-negative");

        var outcomesList = outcomes.ToList();
        for (var i = 0; i < count; i++)
        {
            yield return SampleFrom(outcomesList, random);
        }
    }
}
