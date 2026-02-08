using PutridParrot.Probability;

namespace Tests.PutridParrot.Probability;

public class PTests
{
    [TestCase(0.5f, 0.5f)]
    [TestCase(-1, 0)]
    [TestCase(2, 1)]
    public void Constructor_WithProbability_EnsureBounded(float probability, float expected)
    {
        var p = new P(probability);
        Assert.That(p.Value, Is.EqualTo(expected));
    }

    [TestCase(1, 6, 0.1666f)]
    [TestCase(2, 10, 0.2f)]
    public void Constructor_WithWaysAndOutcomes(int numberOfWays, int totalOutcomes, float expected)
    {
        var p = new P(numberOfWays, totalOutcomes);
        Assert.That(p.Value, Is.EqualTo(expected).Within(0.001));
    }

    [TestCase(6, 0.1666f)]
    [TestCase(2, 0.5f)]
    public void Constructor_WithTotalOutcomes(int totalOutcomes, float expected)
    {
        var p = new P(totalOutcomes);
        Assert.That(p.Value, Is.EqualTo(expected).Within(0.001));
    }

    [Test]
    public void Constructor_WithZeroTotalOutcomes_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new P(0));
    }

    [Test]
    public void Constructor_WithNegativeTotalOutcomes_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new P(-5));
    }

    [Test]
    public void Constructor_WithNegativeWays_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new P(-1, 6));
    }

    [Test]
    public void Constructor_WithZeroTotalOutcomesInWaysConstructor_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new P(1, 0));
    }

    [Test]
    public void StaticFields_HaveCorrectValues()
    {
        Assert.That(P.Impossible.Value, Is.EqualTo(0));
        Assert.That(P.Certain.Value, Is.EqualTo(1));
        Assert.That(P.Even.Value, Is.EqualTo(0.5f));
    }

    [Test]
    public void AndOperator_IndependentEvents_RollingANumberForTwoRollsOfADice()
    {
        var p = new P(6) & new P(6);
        Assert.That(p.Value, Is.EqualTo(1.0f / 36).Within(0.001));
    }

    [Test]
    public void OrOperator_MutuallyExclusiveEvents_EventA_Or_EventB_Occurring()
    {
        var p = new P(6) | new P(6);
        Assert.That(p.Value, Is.EqualTo(1.0f / 3).Within(0.001));
    }

    [Test]
    public void AdditionOperator_SameAsMutuallyExclusiveOr()
    {
        var p1 = new P(0.3f);
        var p2 = new P(0.2f);
        var result = p1 + p2;
        Assert.That(result.Value, Is.EqualTo(0.5f).Within(0.001));
    }

    [Test]
    public void SubtractionOperator_DifferenceOfProbabilities()
    {
        var p1 = new P(0.7f);
        var p2 = new P(0.3f);
        var result = p1 - p2;
        Assert.That(result.Value, Is.EqualTo(0.4f).Within(0.001));
    }

    [Test]
    public void NotOperator_Complement()
    {
        var p = new P(0.3f);
        var notP = ~p;
        Assert.That(notP.Value, Is.EqualTo(0.7f).Within(0.001));
    }

    [Test]
    public void MultiplicationOperator_ScalarMultiplication()
    {
        var p = new P(0.5f);
        var result = p * 0.8f;
        Assert.That(result.Value, Is.EqualTo(0.4f).Within(0.001));
    }

    [Test]
    public void MultiplicationOperator_ScalarMultiplication_Reversed()
    {
        var p = new P(0.5f);
        var result = 0.8f * p;
        Assert.That(result.Value, Is.EqualTo(0.4f).Within(0.001));
    }

    [Test]
    public void DivisionOperator_ScalarDivision()
    {
        var p = new P(0.5f);
        var result = p / 2.0f;
        Assert.That(result.Value, Is.EqualTo(0.25f).Within(0.001));
    }

    [Test]
    public void DivisionOperator_ByZero_ThrowsException()
    {
        var p = new P(0.5f);
        Assert.Throws<DivideByZeroException>(() => _ = p / 0.0f);
    }

    [Test]
    public void GreaterThanOperator_ComparesCorrectly()
    {
        var p1 = new P(0.7f);
        var p2 = new P(0.3f);
        Assert.That(p1 > p2, Is.True);
        Assert.That(p2 > p1, Is.False);
    }

    [Test]
    public void LessThanOperator_ComparesCorrectly()
    {
        var p1 = new P(0.3f);
        var p2 = new P(0.7f);
        Assert.That(p1 < p2, Is.True);
        Assert.That(p2 < p1, Is.False);
    }

    [Test]
    public void GreaterThanOrEqualOperator_ComparesCorrectly()
    {
        var p1 = new P(0.7f);
        var p2 = new P(0.7f);
        var p3 = new P(0.3f);
        Assert.That(p1 >= p2, Is.True);
        Assert.That(p1 >= p3, Is.True);
        Assert.That(p3 >= p1, Is.False);
    }

    [Test]
    public void LessThanOrEqualOperator_ComparesCorrectly()
    {
        var p1 = new P(0.3f);
        var p2 = new P(0.3f);
        var p3 = new P(0.7f);
        Assert.That(p1 <= p2, Is.True);
        Assert.That(p1 <= p3, Is.True);
        Assert.That(p3 <= p1, Is.False);
    }

    [Test]
    public void EqualityOperator_ComparesCorrectly()
    {
        var p1 = new P(0.5f);
        var p2 = new P(0.5f);
        var p3 = new P(0.3f);
        Assert.That(p1 == p2, Is.True);
        Assert.That(p1 == p3, Is.False);
    }

    [Test]
    public void InequalityOperator_ComparesCorrectly()
    {
        var p1 = new P(0.5f);
        var p2 = new P(0.3f);
        var p3 = new P(0.5f);
        Assert.That(p1 != p2, Is.True);
        Assert.That(p1 != p3, Is.False);
    }

    [Test]
    public void EqualityOperator_WithNull_ReturnsFalse()
    {
        var p = new P(0.5f);
        Assert.That(p == null, Is.False);
        Assert.That(null == p, Is.False);
    }

    [Test]
    public void Equals_WithSameValue_ReturnsTrue()
    {
        var p1 = new P(0.5f);
        var p2 = new P(0.5f);
        Assert.That(p1.Equals(p2), Is.True);
    }

    [Test]
    public void Equals_WithDifferentValue_ReturnsFalse()
    {
        var p1 = new P(0.5f);
        var p2 = new P(0.3f);
        Assert.That(p1.Equals(p2), Is.False);
    }

    [Test]
    public void GetHashCode_SameValues_SameHashCode()
    {
        var p1 = new P(0.5f);
        var p2 = new P(0.5f);
        Assert.That(p1.GetHashCode(), Is.EqualTo(p2.GetHashCode()));
    }

    [Test]
    public void CompareTo_LessThan_ReturnsNegative()
    {
        var p1 = new P(0.3f);
        var p2 = new P(0.7f);
        Assert.That(p1.CompareTo(p2), Is.LessThan(0));
    }

    [Test]
    public void CompareTo_GreaterThan_ReturnsPositive()
    {
        var p1 = new P(0.7f);
        var p2 = new P(0.3f);
        Assert.That(p1.CompareTo(p2), Is.GreaterThan(0));
    }

    [Test]
    public void CompareTo_Equal_ReturnsZero()
    {
        var p1 = new P(0.5f);
        var p2 = new P(0.5f);
        Assert.That(p1.CompareTo(p2), Is.EqualTo(0));
    }

    [Test]
    public void CompareTo_Null_ReturnsPositive()
    {
        var p = new P(0.5f);
        Assert.That(p.CompareTo(null), Is.GreaterThan(0));
    }

    [Test]
    public void ImplicitConversion_ToFloat_WorksCorrectly()
    {
        var p = new P(0.75f);
        float value = p;
        Assert.That(value, Is.EqualTo(0.75f));
    }

    [Test]
    public void ToString_FormatsCorrectly()
    {
        var p = new P(0.3333f);
        var result = p.ToString();
        Assert.That(result, Does.StartWith("P("));
        Assert.That(result, Does.Contain("0.3333"));
    }

    [Test]
    public void ToString_WithCustomFormat_FormatsCorrectly()
    {
        var p = new P(0.123456f);
        var result = p.ToString("F2");
        Assert.That(result, Is.EqualTo("P(0.12)"));
    }

    [Test]
    public void ToString_WithPercentageFormat_FormatsCorrectly()
    {
        var p = new P(0.75f);
        var result = p.ToString("P0");
        Assert.That(result, Does.Contain("75"));
    }

    [Test]
    public void Not_ReturnsComplement()
    {
        var p = new P(1, 6);
        var notP = p.Not();
        Assert.That(notP.Value, Is.EqualTo(5.0f / 6).Within(0.001));
    }

    [Test]
    public void ProbabilityOfAnEventAndNotAnEvent_ShouldBeOne()
    {
        var p = new P(1, 6);
        var notP = p.Not();
        Assert.That((p + notP).Value, Is.EqualTo(1).Within(0.001));
    }

    [Test]
    public void And_IndependentEvents_TwoCoinFlips()
    {
        var heads1 = new P(0.5f);
        var heads2 = new P(0.5f);
        var bothHeads = heads1.And(heads2);
        Assert.That(bothHeads.Value, Is.EqualTo(0.25f).Within(0.001));
    }

    [Test]
    public void Or_MutuallyExclusiveEvents_Rolling1or2()
    {
        var one = new P(1, 6);
        var two = new P(1, 6);
        var oneOrTwo = one.Or(two);
        Assert.That(oneOrTwo.Value, Is.EqualTo(1.0f / 3).Within(0.001));
    }

    [Test]
    public void OrDependent_SelectingAKingOrDiamond()
    {
        var king = new P(4, 52);
        var diamond = new P(13, 52);
        var p = king.OrDependent(diamond);
        Assert.That(p.Value, Is.EqualTo(16.0f / 52).Within(0.001));
    }

    [Test]
    public void OrDependent_NonMutuallyExclusiveEvents_FormulaShouldSubtractIntersection()
    {
        var pA = new P(0.6f);
        var pB = new P(0.5f);
        // P(A or B) = P(A) + P(B) - P(A and B)
        // = 0.6 + 0.5 - (0.6 * 0.5) = 1.1 - 0.3 = 0.8
        var result = pA.OrDependent(pB);
        Assert.That(result.Value, Is.EqualTo(0.8f).Within(0.001));
    }

    [Test]
    public void Given_ConditionalProbability_CalculatesCorrectly()
    {
        // P(A|B) where P(A) = 0.3, P(B) = 0.5, assuming independence for simplicity
        var pA = new P(0.3f);
        var pB = new P(0.5f);
        var pAGivenB = pA.Given(pB);
        // P(A|B) = P(A and B) / P(B) = (0.3 * 0.5) / 0.5 = 0.3
        Assert.That(pAGivenB.Value, Is.EqualTo(0.3f).Within(0.001));
    }

    [Test]
    public void Given_WhenConditionIsZero_ThrowsException()
    {
        var pA = new P(0.5f);
        var pB = P.Impossible;
        Assert.Throws<DivideByZeroException>(() => pA.Given(pB));
    }

    [Test]
    public void AtLeastOne_RollingAtLeastOneSixInFourRolls()
    {
        var pSix = new P(1, 6);
        var atLeastOneSix = pSix.AtLeastOne(4);
        // 1 - (5/6)^4 ? 0.5177
        Assert.That(atLeastOneSix.Value, Is.EqualTo(0.5177f).Within(0.001));
    }

    [Test]
    public void AtLeastOne_ZeroTrials_ReturnsZero()
    {
        var p = new P(0.5f);
        var result = p.AtLeastOne(0);
        Assert.That(result.Value, Is.EqualTo(0).Within(0.001));
    }

    [Test]
    public void AtLeastOne_NegativeTrials_ThrowsException()
    {
        var p = new P(0.5f);
        Assert.Throws<ArgumentException>(() => p.AtLeastOne(-1));
    }

    [Test]
    public void EdgeCase_ImpossibleEvent_Operations()
    {
        var p = P.Impossible;
        Assert.That(p.Not().Value, Is.EqualTo(1));
        Assert.That((p & new P(0.5f)).Value, Is.EqualTo(0));
    }

    [Test]
    public void EdgeCase_CertainEvent_Operations()
    {
        var p = P.Certain;
        Assert.That(p.Not().Value, Is.EqualTo(0));
        Assert.That((p & new P(0.5f)).Value, Is.EqualTo(0.5f).Within(0.001));
    }

    [Test]
    public void EdgeCase_AdditionResultsClamped()
    {
        var p1 = new P(0.8f);
        var p2 = new P(0.5f);
        var result = p1 + p2;
        // Should be clamped to 1
        Assert.That(result.Value, Is.EqualTo(1.0f));
    }

    [Test]
    public void EdgeCase_SubtractionResultsClamped()
    {
        var p1 = new P(0.3f);
        var p2 = new P(0.8f);
        var result = p1 - p2;
        // Should be clamped to 0
        Assert.That(result.Value, Is.EqualTo(0.0f));
    }
}