using PutridParrot.Probability.Exceptions;
using System.Text.Json.Serialization;

namespace PutridParrot.Probability;

public class P : IEquatable<P>, IComparable<P>, IFormattable
{
    public const float DefaultTolerance = 0.0001f;

    public static readonly P Impossible = new(0.0f);
    public static readonly P Certain = new(1.0f);
    public static readonly P Even = new(0.5f);

    /// <summary>
    /// A probability represented by a value
    /// within the range [0, 1] where 0
    /// represents an outcome never occurring
    /// whereas a 1 meaning it always occurs.
    /// </summary>
    /// <param name="value"></param>
    [JsonConstructor]
    public P(float value)
    {
        Value = Math.Clamp(value, 0, 1);
    }

    /// <summary>
    /// Shorthand for a probability with a single way
    /// for an outcome
    /// </summary>
    /// <example>
    /// P(6) is equivalent to P(1, 6)
    /// </example>
    /// <param name="totalOutcomes"></param>
    /// <exception cref="ArgumentException">Thrown when totalOutcomes is less than or equal to zero</exception>
    public P(int totalOutcomes) :
        this(1.0f / totalOutcomes)
    {
        Guard.ThrowIfNotPositive(totalOutcomes, nameof(totalOutcomes), "Total outcomes must be positive");
    }

    /// <summary>
    /// Probability of an event, taken
    /// from the number of ways something can happen
    /// and the total number of outcomes
    /// </summary>
    /// <example>
    /// P(1, 6) represents that probability
    /// of a number being rolled on a die
    /// i.e. 1/6
    /// </example>
    /// <param name="numberOfWays"></param>
    /// <param name="totalOutcomes"></param>
    /// <exception cref="ArgumentException">Thrown when totalOutcomes is less than or equal to zero</exception>
    /// <exception cref="ArgumentException">Thrown when numberOfWays is negative</exception>
    public P(int numberOfWays, int totalOutcomes) :
        this((float)numberOfWays / totalOutcomes)
    {
        Guard.ThrowIfNotPositive(totalOutcomes, nameof(totalOutcomes), "Total outcomes must be positive");
        Guard.ThrowIfNegative(numberOfWays, nameof(numberOfWays), "Number of ways cannot be negative");
    }

    public float Value { get; }

    public static implicit operator float(P p) =>
        p.Value;

    /// <summary>
    /// Intersection (AND) - Independent Events: P(A ∩ B) = P(A) * P(B)
    /// </summary>
    public static P operator &(P a, P b) =>
        new(a.Value * b.Value);

    /// <summary>
    /// Union (OR) - Mutually Exclusive Events: P(A ∪ B) = P(A) + P(B)
    /// </summary>
    public static P operator |(P a, P b) =>
        new(a.Value + b.Value);

    /// <summary>
    /// Addition - Mutually Exclusive Events: P(A) + P(B)
    /// </summary>
    public static P operator +(P a, P b) =>
        new(a.Value + b.Value);

    /// <summary>
    /// Complement (NOT) - P(¬A) = 1 - P(A)
    /// </summary>
    public static P operator ~(P p) =>
        new(1.0f - p.Value);

    public static bool operator >(P a, P b) =>
        a.Value > b.Value;

    public static bool operator <(P a, P b) =>
        a.Value < b.Value;

    public static bool operator >=(P a, P b) =>
        a.Value >= b.Value;

    public static bool operator <=(P a, P b) =>
        a.Value <= b.Value;

    /// <summary>
    /// Equality comparison
    /// </summary>
    public static bool operator ==(P? a, P? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return Math.Abs(a.Value - b.Value) < DefaultTolerance;
    }

    /// <summary>
    /// Inequality comparison
    /// </summary>
    public static bool operator !=(P? a, P? b) =>
        !(a == b);

    /// <summary>
    /// Subtraction - P(A) - P(B)
    /// </summary>
    public static P operator -(P a, P b) =>
        new(a.Value - b.Value);

    /// <summary>
    /// Scalar multiplication - P(A) * scalar
    /// </summary>
    public static P operator *(P p, float scalar) =>
        new(p.Value * scalar);

    /// <summary>
    /// Scalar multiplication - scalar * P(A)
    /// </summary>
    public static P operator *(float scalar, P p) =>
        new(p.Value * scalar);

    /// <summary>
    /// Scalar division - P(A) / scalar
    /// </summary>
    /// <exception cref="DivideByZeroException">Thrown when scalar is zero</exception>
    public static P operator /(P p, float scalar)
    {
        Guard.ThrowIfZero(scalar, "Cannot divide probability by zero");
        return new(p.Value / scalar);
    }

    public override bool Equals(object? obj) =>
        obj is P other && this == other;

    public bool Equals(P? other) =>
        this == other;

    public override int GetHashCode() =>
        Value.GetHashCode();

    public int CompareTo(P? other)
    {
        return other is null ? 1 : Value.CompareTo(other.Value);
    }

    public override string ToString() =>
        ToString("F4", null);

    public string ToString(string? format) =>
        ToString(format, null);

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        format ??= "F4";
        return $"P({Value.ToString(format, formatProvider)})";
    }
}