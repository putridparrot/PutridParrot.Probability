using PutridParrot.Probability;

namespace Tests.PutridParrot.Probability;

public class CombinatoricsTests
{
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(5, 120)]
    [TestCase(10, 3628800)]
    public void Factorial_CalculatesCorrectly(int n, long expected)
    {
        var result = Combinatorics.Factorial(n);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Factorial_NegativeNumber_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => Combinatorics.Factorial(-1));
    }

    [TestCase(5, 3, 60)]
    [TestCase(10, 2, 90)]
    [TestCase(4, 4, 24)]
    [TestCase(5, 0, 1)]
    public void Permutations_CalculatesCorrectly(int n, int r, long expected)
    {
        var result = Combinatorics.Permutations(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Permutations_InvalidParameters_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => Combinatorics.Permutations(-1, 2));
        Assert.Throws<ArgumentException>(() => Combinatorics.Permutations(5, -1));
        Assert.Throws<ArgumentException>(() => Combinatorics.Permutations(3, 5));
    }

    [TestCase(5, 3, 10)]
    [TestCase(10, 2, 45)]
    [TestCase(52, 5, 2598960)]
    [TestCase(4, 0, 1)]
    [TestCase(4, 4, 1)]
    public void Combinations_CalculatesCorrectly(int n, int r, long expected)
    {
        var result = Combinatorics.Combinations(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Combinations_InvalidParameters_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => Combinatorics.Combinations(-1, 2));
        Assert.Throws<ArgumentException>(() => Combinatorics.Combinations(5, -1));
        Assert.Throws<ArgumentException>(() => Combinatorics.Combinations(3, 5));
    }

    [Test]
    public void BinomialCoefficient_SameAsCombinations()
    {
        var c = Combinatorics.Combinations(10, 3);
        var bc = Combinatorics.BinomialCoefficient(10, 3);
        Assert.That(bc, Is.EqualTo(c));
    }
}
