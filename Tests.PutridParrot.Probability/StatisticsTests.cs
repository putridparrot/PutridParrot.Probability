using PutridParrot.Probability;

namespace Tests.PutridParrot.Probability;

public class StatisticsTests
{
    [Test]
    public void ExpectedValue_DiceRoll_CalculatesCorrectly()
    {
        var outcomes = new[]
        {
            (1f, new P(1, 6)),
            (2f, new P(1, 6)),
            (3f, new P(1, 6)),
            (4f, new P(1, 6)),
            (5f, new P(1, 6)),
            (6f, new P(1, 6))
        };

        var ev = Statistics.ExpectedValue(outcomes);
        Assert.That(ev, Is.EqualTo(3.5f).Within(0.001));
    }

    [Test]
    public void ExpectedValue_InvalidProbabilitySum_ThrowsException()
    {
        var outcomes = new[]
        {
            (1f, new P(0.3f)),
            (2f, new P(0.3f))
        };

        Assert.Throws<ArgumentException>(() => Statistics.ExpectedValue(outcomes));
    }

    [Test]
    public void Variance_CalculatesCorrectly()
    {
        var outcomes = new[]
        {
            (1f, new P(0.5f)),
            (3f, new P(0.5f))
        };

        var variance = Statistics.Variance(outcomes);
        Assert.That(variance, Is.EqualTo(1.0f).Within(0.001));
    }

    [Test]
    public void StandardDeviation_CalculatesCorrectly()
    {
        var outcomes = new[]
        {
            (1f, new P(0.5f)),
            (3f, new P(0.5f))
        };

        var stdDev = Statistics.StandardDeviation(outcomes);
        Assert.That(stdDev, Is.EqualTo(1.0f).Within(0.001));
    }

    [Test]
    public void Mode_ReturnsHighestProbability()
    {
        var outcomes = new[]
        {
            (1f, new P(0.2f)),
            (2f, new P(0.5f)),
            (3f, new P(0.3f))
        };

        var mode = Statistics.Mode(outcomes);
        Assert.That(mode, Is.EqualTo(2f));
    }

    [Test]
    public void Median_CalculatesCorrectly()
    {
        var outcomes = new[]
        {
            (1f, new P(0.25f)),
            (2f, new P(0.25f)),
            (3f, new P(0.25f)),
            (4f, new P(0.25f))
        };

        var median = Statistics.Median(outcomes);
        Assert.That(median, Is.EqualTo(2f).Or.EqualTo(3f));
    }

    [Test]
    public void Covariance_IndependentVariables_NearZero()
    {
        var outcomes = new[]
        {
            (1f, 1f, new P(0.25f)),
            (1f, 2f, new P(0.25f)),
            (2f, 1f, new P(0.25f)),
            (2f, 2f, new P(0.25f))
        };

        var cov = Statistics.Covariance(outcomes);
        Assert.That(cov, Is.EqualTo(0).Within(0.001));
    }

    [Test]
    public void Correlation_PerfectPositiveCorrelation()
    {
        var outcomes = new[]
        {
            (1f, 1f, new P(0.5f)),
            (2f, 2f, new P(0.5f))
        };

        var corr = Statistics.Correlation(outcomes);
        Assert.That(corr, Is.EqualTo(1.0f).Within(0.001));
    }

    [Test]
    public void Statistics_EmptyOutcomes_ThrowsException()
    {
        var outcomes = Array.Empty<(float, P)>();
        Assert.Throws<ArgumentException>(() => Statistics.ExpectedValue(outcomes));
    }
}
