using PutridParrot.Probability;
using PutridParrot.Probability.Distributions;

namespace Tests.PutridParrot.Probability;

public class DistributionTests
{
    [Test]
    public void Binomial_CoinFlips_CalculatesCorrectly()
    {
        // 3 heads in 5 coin flips
        var p = Binomial.Probability(5, 3, new P(0.5f));
        Assert.That(p.Value, Is.EqualTo(0.3125f).Within(0.001));
    }

    [Test]
    public void Binomial_Mean_CalculatesCorrectly()
    {
        var mean = Binomial.Mean(10, new P(0.3f));
        Assert.That(mean, Is.EqualTo(3.0f).Within(0.001));
    }

    [Test]
    public void Binomial_Variance_CalculatesCorrectly()
    {
        var variance = Binomial.Variance(10, new P(0.3f));
        Assert.That(variance, Is.EqualTo(2.1f).Within(0.001));
    }

    [Test]
    public void Binomial_StandardDeviation_CalculatesCorrectly()
    {
        var stdDev = Binomial.StandardDeviation(10, new P(0.3f));
        Assert.That(stdDev, Is.EqualTo(1.449f).Within(0.001));
    }

    [Test]
    public void Binomial_CumulativeProbability_CalculatesCorrectly()
    {
        // P(X ? 2) for 5 trials with p=0.5
        var p = Binomial.CumulativeProbability(5, 2, new P(0.5f));
        Assert.That(p.Value, Is.EqualTo(0.5f).Within(0.001));
    }

    [Test]
    public void Normal_Pdf_StandardNormal_CalculatesCorrectly()
    {
        // Standard normal at x=0 should be 1/?(2?) ? 0.3989
        var pdf = Normal.Pdf(0, 0, 1);
        Assert.That(pdf, Is.EqualTo(0.3989f).Within(0.001));
    }

    [Test]
    public void Normal_ZScore_CalculatesCorrectly()
    {
        var z = Normal.ZScore(110, 100, 15);
        Assert.That(z, Is.EqualTo(0.6667f).Within(0.001));
    }

    [Test]
    public void Normal_Cdf_StandardNormal_AtZero()
    {
        // CDF at mean should be 0.5
        var p = Normal.Cdf(0, 0, 1);
        Assert.That(p.Value, Is.EqualTo(0.5f).Within(0.01));
    }

    [Test]
    public void Poisson_Probability_CalculatesCorrectly()
    {
        // ?=3, k=2
        var p = Poisson.Probability(2, 3.0f);
        Assert.That(p.Value, Is.EqualTo(0.224f).Within(0.001));
    }

    [Test]
    public void Poisson_Mean_EqualsLambda()
    {
        var mean = Poisson.Mean(5.0f);
        Assert.That(mean, Is.EqualTo(5.0f));
    }

    [Test]
    public void Poisson_Variance_EqualsLambda()
    {
        var variance = Poisson.Variance(5.0f);
        Assert.That(variance, Is.EqualTo(5.0f));
    }

    [Test]
    public void Poisson_StandardDeviation_CalculatesCorrectly()
    {
        var stdDev = Poisson.StandardDeviation(9.0f);
        Assert.That(stdDev, Is.EqualTo(3.0f).Within(0.001));
    }

    [Test]
    public void Poisson_CumulativeProbability_CalculatesCorrectly()
    {
        var p = Poisson.CumulativeProbability(3, 2.0f);
        Assert.That(p.Value, Is.GreaterThan(0) & Is.LessThan(1));
    }
}
