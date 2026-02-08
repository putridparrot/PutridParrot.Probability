using PutridParrot.Probability;

namespace Tests.PutridParrot.Probability;

public class BayesTests
{
    [Test]
    public void Bayes_MedicalTestExample()
    {
        // Classic example: Disease test
        // P(Disease) = 0.01 (1% of population has disease)
        // P(Positive|Disease) = 0.99 (99% sensitivity)
        // P(Positive) = 0.0199 (calculated from all cases)
        var pDisease = new P(0.01f);
        var pPositiveGivenDisease = new P(0.99f);
        var pPositive = new P(0.0199f);

        var pDiseaseGivenPositive = BayesTheorem.Calculate(pDisease, pPositiveGivenDisease, pPositive);

        // P(Disease|Positive) = 0.99 * 0.01 / 0.0199 ? 0.497
        Assert.That(pDiseaseGivenPositive.Value, Is.EqualTo(0.497f).Within(0.01));
    }

    [Test]
    public void Bayes_WhenEvidenceIsZero_ThrowsException()
    {
        var pA = new P(0.5f);
        var pBGivenA = new P(0.8f);
        var pB = P.Impossible;
        Assert.Throws<DivideByZeroException>(() => BayesTheorem.Calculate(pA, pBGivenA, pB));
    }
}
