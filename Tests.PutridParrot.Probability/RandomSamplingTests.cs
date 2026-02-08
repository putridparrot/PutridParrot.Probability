using PutridParrot.Probability;

namespace Tests.PutridParrot.Probability;

public class RandomSamplingTests
{
    [Test]
    public void Sample_CertainEvent_AlwaysReturnsTrue()
    {
        var p = P.Certain;
        var results = p.Sample(100).ToList();
        Assert.That(results.All(r => r), Is.True);
    }

    [Test]
    public void Sample_ImpossibleEvent_AlwaysReturnsFalse()
    {
        var p = P.Impossible;
        var results = p.Sample(100).ToList();
        Assert.That(results.All(r => !r), Is.True);
    }

    [Test]
    public void Sample_FairCoin_ApproximatelyHalf()
    {
        var p = new P(0.5f);
        var random = new Random(42); // Fixed seed for reproducibility
        var successes = p.CountSuccesses(1000, random);
        
        // Should be roughly 500, allow 10% variance
        Assert.That(successes, Is.InRange(400, 600));
    }

    [Test]
    public void CountSuccesses_WithZeroTrials_ReturnsZero()
    {
        var p = new P(0.5f);
        var successes = p.CountSuccesses(0);
        Assert.That(successes, Is.EqualTo(0));
    }

    [Test]
    public void CountSuccesses_NegativeTrials_ThrowsException()
    {
        var p = new P(0.5f);
        Assert.Throws<ArgumentException>(() => p.CountSuccesses(-1));
    }

    [Test]
    public void EstimateThroughSampling_ApproximatesOriginalProbability()
    {
        var p = new P(0.3f);
        var random = new Random(42);
        var estimated = p.EstimateThroughSampling(10000, random);
        
        // Should be close to 0.3, allow 5% variance
        Assert.That(estimated.Value, Is.InRange(0.25f, 0.35f));
    }

    [Test]
    public void SampleFrom_DiscreteDistribution_ReturnsValidOutcome()
    {
        var outcomes = new[]
        {
            ("A", new P(0.5f)),
            ("B", new P(0.3f)),
            ("C", new P(0.2f))
        };

        var random = new Random(42);
        var result = RandomSampling.SampleFrom(outcomes, random);
        
        Assert.That(result, Is.AnyOf("A", "B", "C"));
    }

    [Test]
    public void SampleFrom_MultipleSamples_ReturnsCorrectCount()
    {
        var outcomes = new[]
        {
            (1, new P(0.5f)),
            (2, new P(0.5f))
        };

        var random = new Random(42);
        var results = RandomSampling.SampleFrom(outcomes, 100, random).ToList();
        
        Assert.That(results.Count, Is.EqualTo(100));
    }

    [Test]
    public void SampleFrom_EmptyOutcomes_ThrowsException()
    {
        var outcomes = Array.Empty<(int, P)>();
        Assert.Throws<ArgumentException>(() => RandomSampling.SampleFrom(outcomes));
    }

    [Test]
    public void SampleFrom_Distribution_ApproximatelyCorrect()
    {
        var outcomes = new[]
        {
            ("Heads", new P(0.7f)),
            ("Tails", new P(0.3f))
        };

        var random = new Random(42);
        var samples = RandomSampling.SampleFrom(outcomes, 1000, random).ToList();
        var headsCount = samples.Count(s => s == "Heads");
        
        // Should be roughly 700, allow 10% variance
        Assert.That(headsCount, Is.InRange(600, 800));
    }
}
