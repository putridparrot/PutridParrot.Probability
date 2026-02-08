namespace PutridParrot.Probability;

/// <summary>
/// Statistical operations for probability collections
/// </summary>
public static class Statistics
{
    /// <summary>
    /// Calculate expected value (weighted average): E[X] = Σ(x × P(x))
    /// </summary>
    /// <param name="outcomes">Collection of (value, probability) pairs</param>
    /// <returns>Expected value</returns>
    /// <exception cref="ArgumentException">Thrown when outcomes is empty or probabilities don't sum to 1</exception>
    public static float ExpectedValue(IEnumerable<(float value, P probability)> outcomes)
    {
        var outcomesList = outcomes.ToList();

        if (!outcomesList.Any())
        {
            throw new ArgumentException("Outcomes cannot be empty", nameof(outcomes));
        }

        var probabilitySum = outcomesList.Sum(o => o.probability.Value);
        if (MathF.Abs(probabilitySum - 1.0f) > P.DefaultTolerance)
        {
            throw new ArgumentException($"Probabilities must sum to 1.0 (got {probabilitySum})", nameof(outcomes));
        }

        return outcomesList.Sum(o => o.value * o.probability.Value);
    }

    /// <summary>
    /// Calculate variance: Var[X] = E[X²] - (E[X])²
    /// </summary>
    /// <param name="outcomes">Collection of (value, probability) pairs</param>
    /// <returns>Variance</returns>
    public static float Variance(IEnumerable<(float value, P probability)> outcomes)
    {
        var outcomesList = outcomes.ToList();

        if (!outcomesList.Any())
        {
            throw new ArgumentException("Outcomes cannot be empty", nameof(outcomes));
        }

        var mean = ExpectedValue(outcomesList);
        var expectedSquare = outcomesList.Sum(o => o.value * o.value * o.probability.Value);
        
        return expectedSquare - (mean * mean);
    }

    /// <summary>
    /// Calculate standard deviation: σ = √Var[X]
    /// </summary>
    public static float StandardDeviation(IEnumerable<(float value, P probability)> outcomes)
    {
        return MathF.Sqrt(Variance(outcomes));
    }

    /// <summary>
    /// Calculate mode (most likely outcome)
    /// </summary>
    /// <param name="outcomes">Collection of (value, probability) pairs</param>
    /// <returns>Value with highest probability</returns>
    public static float Mode(IEnumerable<(float value, P probability)> outcomes)
    {
        var outcomesList = outcomes.ToList();

        if (!outcomesList.Any())
        {
            throw new ArgumentException("Outcomes cannot be empty", nameof(outcomes));
        }

        return outcomesList.OrderByDescending(o => o.probability.Value).First().value;
    }

    /// <summary>
    /// Calculate median (50th percentile)
    /// </summary>
    /// <param name="outcomes">Collection of (value, probability) pairs sorted by value</param>
    /// <returns>Median value</returns>
    public static float Median(IEnumerable<(float value, P probability)> outcomes)
    {
        var sorted = outcomes.OrderBy(o => o.value).ToList();

        if (!sorted.Any())
        {
            throw new ArgumentException("Outcomes cannot be empty", nameof(outcomes));
        }

        float cumulative = 0;
        foreach (var (value, probability) in sorted)
        {
            cumulative += probability.Value;
            if (cumulative >= 0.5f)
                return value;
        }

        return sorted.Last().value;
    }

    /// <summary>
    /// Calculate covariance between two random variables
    /// Cov(X,Y) = E[(X - E[X])(Y - E[Y])]
    /// </summary>
    public static float Covariance(
        IEnumerable<(float x, float y, P probability)> jointOutcomes)
    {
        var outcomes = jointOutcomes.ToList();

        if (!outcomes.Any())
        {
            throw new ArgumentException("Outcomes cannot be empty", nameof(jointOutcomes));
        }

        var meanX = outcomes.Sum(o => o.x * o.probability.Value);
        var meanY = outcomes.Sum(o => o.y * o.probability.Value);

        return outcomes.Sum(o => (o.x - meanX) * (o.y - meanY) * o.probability.Value);
    }

    /// <summary>
    /// Calculate correlation coefficient: ρ = Cov(X,Y) / (σX × σY)
    /// </summary>
    public static float Correlation(
        IEnumerable<(float x, float y, P probability)> jointOutcomes)
    {
        var outcomes = jointOutcomes.ToList();
        
        var xOutcomes = outcomes
            .GroupBy(o => o.x)
            .Select(g => (g.Key, new P(g.Sum(o => o.probability.Value))));
        
        var yOutcomes = outcomes
            .GroupBy(o => o.y)
            .Select(g => (g.Key, new P(g.Sum(o => o.probability.Value))));

        var stdX = StandardDeviation(xOutcomes);
        var stdY = StandardDeviation(yOutcomes);
        var cov = Covariance(outcomes);

        if (stdX == 0 || stdY == 0)
        {
            throw new InvalidOperationException("Cannot calculate correlation when standard deviation is zero");
        }

        return cov / (stdX * stdY);
    }
}
