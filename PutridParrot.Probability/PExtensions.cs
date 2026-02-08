using PutridParrot.Probability.Exceptions;

namespace PutridParrot.Probability;

public static class PExtensions
{
    extension(P p)
    {
        /// <summary>
        /// Complement: P(¬A) = 1 - P(A)
        /// </summary>
        /// <example>
        /// If P(rolling a 6) = 1/6, then P(not rolling a 6) = 5/6
        /// </example>
        public P Not() =>
            new(1.0f - p.Value);

        /// <summary>
        /// Intersection (AND) - Independent Events: P(A ∩ B) = P(A) * P(B)
        /// </summary>
        /// <example>
        /// If two coins are flipped, the chance of both being heads is
        /// P(A and B) = 1/2 * 1/2 = 1/4
        /// </example>
        public P And(P b) =>
            new(p.Value * b.Value);

        /// <summary>
        /// Union (OR) - Mutually Exclusive Events: P(A ∪ B) = P(A) + P(B)
        /// </summary>
        /// <example>
        /// Rolling a 1 or 2 on a die: P(1 or 2) = 1/6 + 1/6 = 1/3
        /// </example>
        public P Or(P b) =>
            new(p.Value + b.Value);

        /// <summary>
        /// Union (OR) - Non-Mutually Exclusive Events: P(A ∪ B) = P(A) + P(B) - P(A ∩ B)
        /// </summary>
        /// <example>
        /// Selecting a King or Diamond from a deck:
        /// P(King or Diamond) = P(King) + P(Diamond) - P(King of Diamonds)
        /// = 4/52 + 13/52 - 1/52 = 16/52
        /// </example>
        public P OrDependent(P b) =>
            new(p.Value + b.Value - p.And(b));

        /// <summary>
        /// Conditional probability: P(A | B) = P(A ∩ B) / P(B)
        /// </summary>
        /// <example>
        /// Given a bag with 2 red balls and 2 blue balls (4 in total),
        /// if we know the first ball drawn was red, what's the probability
        /// the second ball is also red?
        /// P(second red | first red) = (1/4) / (1/2) = 1/2... but with
        /// replacement. Without replacement it's more complex.
        /// </example>
        /// <exception cref="DivideByZeroException">Thrown when P(B) = 0</exception>
        public P Given(P b)
        {
            Guard.ThrowIfZero(b.Value, "Cannot calculate conditional probability when P(B) = 0");
            return new(p.And(b).Value / b.Value);
        }

        /// <summary>
        /// Calculate the probability that at least one event occurs (complement of none occurring)
        /// P(at least one) = 1 - P(none) = 1 - (1-P)^n
        /// </summary>
        /// <param name="p">Probability of a single event</param>
        /// <param name="trials">Number of independent trials</param>
        /// <example>
        /// Probability of rolling at least one 6 in 4 rolls:
        /// P(at least one 6) = 1 - (5/6)^4 ≈ 0.518
        /// </example>
        public P AtLeastOne(int trials)
        {
            Guard.ThrowIfNegative(trials, nameof(trials), "Number of trials must be non-negative");
            return new(1.0f - MathF.Pow(1.0f - p.Value, trials));
        }
    }
}