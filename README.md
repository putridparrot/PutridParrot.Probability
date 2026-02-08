# PutridParrot.Probability

A .NET probability library with statistical distributions, and simulation capabilities.

## Key Features

- **Type-safe probability values** - Values automatically clamped to [0,1]
- **Intuitive operators** - Use `&`, `|`, `~`, `+`, `-` for probability operations
- **Combinatorics** - Factorial, permutations, combinations
- **Distributions** - Binomial, Normal, Poisson
- **Statistics** - Expected value, variance, correlation, and more
- **Random sampling** - Monte Carlo simulation support
- **JSON serialization** - Easy storage and transmission
- **Production-ready** - Full validation, error handling, and 100+ tests

## Installation

```bash
dotnet add package PutridParrot.Probability
```

## Quick Start

```csharp
using PutridParrot.Probability;

// Create probabilities
var coinFlip = new P(0.5f);           // 50% chance
var diceRoll = new P(1, 6);           // 1 in 6 chance (rolling a specific number)
var certainty = P.Certain;            // 100% (static field)

// Combine probabilities
var bothHeads = coinFlip & coinFlip;  // AND: P(A ∩ B) = 0.25
var either = coinFlip | coinFlip;     // OR: P(A ∪ B) = 1.0
var notHeads = ~coinFlip;             // NOT: P(¬A) = 0.5

// Extensions
var rolling1or2 = new P(1, 6).Or(new P(1, 6));     // 1/3
var atLeastOneSix = new P(1, 6).AtLeastOne(4);     // ~0.518
```

## Core Concepts

### Creating Probabilities

```csharp
// From a value (0 to 1)
var p1 = new P(0.75f);

// From favorable/total outcomes
var p2 = new P(3, 10);          // 3 out of 10 = 0.3

// Shorthand for 1 favorable outcome
var p3 = new P(6);              // 1 out of 6 = 0.1666...

// Static fields
var impossible = P.Impossible;   // 0
var certain = P.Certain;        // 1
var even = P.Even;              // 0.5
```

### Operators

```csharp
var pA = new P(0.6f);
var pB = new P(0.4f);

// Independent events (AND): P(A ∩ B) = P(A) × P(B)
var both = pA & pB;             // 0.24

// Mutually exclusive (OR): P(A ∪ B) = P(A) + P(B)
var either = pA | pB;           // 1.0 (clamped)

// Complement (NOT): P(¬A) = 1 - P(A)
var notA = ~pA;                 // 0.4

// Arithmetic
var sum = pA + pB;              // 1.0 (clamped)
var diff = pA - pB;             // 0.2
var scaled = pA * 0.5f;         // 0.3
var divided = pA / 2.0f;        // 0.3

// Comparison
bool greater = pA > pB;         // true
bool equal = pA == pB;          // false
```

### Extension Methods

```csharp
var p = new P(1, 6);

// Complement
var notP = p.Not();             // 5/6

// Independent AND
var twoSixes = p.And(p);        // 1/36

// Mutually exclusive OR
var oneOrTwo = p.Or(new P(1, 6));  // 1/3

// Non-mutually exclusive OR
var kingOrDiamond = new P(4, 52).OrDependent(new P(13, 52));  // 16/52

// Conditional probability: P(A|B)
var pAGivenB = pA.Given(pB);

// At least one success in n trials
var atLeastOne = p.AtLeastOne(4);  // 1 - (5/6)^4
```

## Combinatorics

```csharp
using PutridParrot.Probability;

// Factorial: n!
var fact5 = Combinatorics.Factorial(5);  // 120

// Permutations: nPr = n! / (n-r)!
var perm = Combinatorics.Permutations(10, 3);  // 720

// Combinations: nCr = n! / (r! × (n-r)!)
var comb = Combinatorics.Combinations(52, 5);  // 2,598,960

// Binomial coefficient (alias for Combinations)
var binom = Combinatorics.BinomialCoefficient(10, 3);  // 120
```

## Probability Distributions

### Binomial Distribution

```csharp
using PutridParrot.Probability.Distributions;

// Probability of exactly k successes in n trials
var p = Binomial.Probability(
    n: 10,                      // trials
    k: 3,                       // successes
    p: new P(0.5f)             // probability per trial
);  // 0.117

// Cumulative: P(X ≤ k)
var cumulative = Binomial.CumulativeProbability(10, 3, new P(0.5f));

// Statistics
var mean = Binomial.Mean(10, new P(0.5f));                    // 5.0
var variance = Binomial.Variance(10, new P(0.5f));            // 2.5
var stdDev = Binomial.StandardDeviation(10, new P(0.5f));     // 1.58
```

### Normal Distribution

```csharp
using PutridParrot.Probability.Distributions;

// Probability density function
var pdf = Normal.Pdf(
    x: 100,
    mean: 100,
    stdDev: 15
);

// Cumulative distribution function
var cdf = Normal.Cdf(110, 100, 15);

// Z-score (standard score)
var z = Normal.ZScore(110, 100, 15);  // 0.667
```

### Poisson Distribution

```csharp
using PutridParrot.Probability.Distributions;

// Probability of exactly k events
var p = Poisson.Probability(
    k: 3,                       // events
    lambda: 2.5f               // average rate
);  // 0.214

// Cumulative: P(X ≤ k)
var cumulative = Poisson.CumulativeProbability(3, 2.5f);

// Statistics (for Poisson, mean = variance = lambda)
var mean = Poisson.Mean(2.5f);                    // 2.5
var variance = Poisson.Variance(2.5f);            // 2.5
var stdDev = Poisson.StandardDeviation(2.5f);     // 1.58
```

## Statistics

```csharp
using PutridParrot.Probability;

var outcomes = new[]
{
    (1f, new P(1, 6)),
    (2f, new P(1, 6)),
    (3f, new P(1, 6)),
    (4f, new P(1, 6)),
    (5f, new P(1, 6)),
    (6f, new P(1, 6))
};

// Expected value: E[X] = Σ(x × P(x))
var ev = Statistics.ExpectedValue(outcomes);  // 3.5

// Variance: Var[X] = E[X²] - (E[X])²
var variance = Statistics.Variance(outcomes);  // 2.917

// Standard deviation: σ = √Var[X]
var stdDev = Statistics.StandardDeviation(outcomes);  // 1.708

// Mode (most likely value)
var mode = Statistics.Mode(outcomes);

// Median (50th percentile)
var median = Statistics.Median(outcomes);

// Covariance and correlation for joint distributions
var jointOutcomes = new[]
{
    (1f, 2f, new P(0.25f)),
    (1f, 3f, new P(0.25f)),
    (2f, 2f, new P(0.25f)),
    (2f, 3f, new P(0.25f))
};

var cov = Statistics.Covariance(jointOutcomes);
var corr = Statistics.Correlation(jointOutcomes);
```

## Random Sampling & Simulation

```csharp
using PutridParrot.Probability;

var p = new P(0.3f);

// Single sample
bool result = p.Sample();  // true or false

// Multiple samples
var samples = p.Sample(1000).ToList();

// Count successes
int successes = p.CountSuccesses(1000);

// Monte Carlo estimation
var estimated = p.EstimateThroughSampling(10000);  // ≈ 0.3

// Sample from discrete distribution
var outcomes = new[]
{
    ("Red", new P(0.5f)),
    ("Blue", new P(0.3f)),
    ("Green", new P(0.2f))
};

var color = RandomSampling.SampleFrom(outcomes);
var colors = RandomSampling.SampleFrom(outcomes, 100).ToList();
```

## Bayes' Theorem

```csharp
using PutridParrot.Probability;

// Medical test example
var pDisease = new P(0.01f);              // 1% have disease
var pPositiveGivenDisease = new P(0.99f); // 99% sensitivity
var pPositive = new P(0.0199f);           // P(positive test)

// P(Disease|Positive) = P(Positive|Disease) × P(Disease) / P(Positive)
var pDiseaseGivenPositive = BayesTheorem.Calculate(
    pDisease,
    pPositiveGivenDisease,
    pPositive
);  // ≈ 0.497
```

## JSON Serialization

```csharp
using System.Text.Json;
using PutridParrot.Probability;

var p = new P(0.75f);

// Serialize
var json = JsonSerializer.Serialize(p);
// {"Value":0.75}

// Deserialize
var deserialized = JsonSerializer.Deserialize<P>(json);

// Works with collections
var probabilities = new List<P> { new P(0.1f), new P(0.5f), new P(0.9f) };
var jsonList = JsonSerializer.Serialize(probabilities);
```

## Advanced Examples

### Poker Hand Probability

```csharp
// Probability of being dealt a specific 5-card hand
var totalHands = Combinatorics.Combinations(52, 5);  // 2,598,960

// Royal flush (10, J, Q, K, A of same suit)
var royalFlushes = 4;  // One per suit
var pRoyalFlush = new P(royalFlushes, (int)totalHands);  // 0.00000154

// Four of a kind
var fourOfAKind = 13 * 48;  // 13 ranks, 48 choices for 5th card
var pFourOfAKind = new P(fourOfAKind, (int)totalHands);  // 0.00024
```

### Quality Control Simulation

```csharp
// Simulate defect detection with 95% detection rate
var detectionRate = new P(0.95f);
var trials = 1000;

// How many defects detected out of 1000?
var detected = detectionRate.CountSuccesses(trials);
Console.WriteLine($"Detected {detected} out of {trials} defects");

// Probability of detecting at least one defect in 5 samples
var atLeastOne = detectionRate.AtLeastOne(5);  // 0.9997
```

### A/B Testing

```csharp
// Test two conversion rates
var conversionA = new P(0.10f);  // 10% conversion
var conversionB = new P(0.12f);  // 12% conversion

var samples = 1000;
var successesA = conversionA.CountSuccesses(samples);
var successesB = conversionB.CountSuccesses(samples);

Console.WriteLine($"A: {successesA}/{samples}, B: {successesB}/{samples}");
```

## Interface Support

```csharp
// IEquatable<P>
var p1 = new P(0.5f);
var p2 = new P(0.5f);
bool equal = p1.Equals(p2);  // true

// IComparable<P>
var probabilities = new[] { new P(0.3f), new P(0.1f), new P(0.5f) };
var sorted = probabilities.OrderBy(p => p).ToList();

// IFormattable
var p = new P(0.123456f);
Console.WriteLine(p.ToString());        // P(0.1235)
Console.WriteLine(p.ToString("F2"));    // P(0.12)
Console.WriteLine(p.ToString("P0"));    // P(12 %)
```

## Error Handling

```csharp
// All invalid inputs throw meaningful exceptions

// Division by zero
Assert.Throws<ArgumentException>(() => new P(0));
Assert.Throws<ArgumentException>(() => new P(1, 0));
Assert.Throws<DivideByZeroException>(() => new P(0.5f) / 0);

// Negative values
Assert.Throws<ArgumentException>(() => new P(-5));
Assert.Throws<ArgumentException>(() => new P(-1, 6));

// Invalid ranges
Assert.Throws<ArgumentException>(() => Combinatorics.Combinations(3, 5));
```

## Best Practices

1. **Use static fields** for common probabilities:
   ```csharp
   var certain = P.Certain;     // Better than new P(1.0f)
   var impossible = P.Impossible;
   var coinFlip = P.Even;
   ```

2. **Use constructors appropriately**:
   ```csharp
   var specific = new P(0.75f);      // When you know exact probability
   var ratio = new P(3, 4);          // When you have favorable/total
   var single = new P(6);            // For 1 out of n
   ```

3. **Validate probabilities sum to 1** for distributions:
   ```csharp
   var outcomes = new[] { (1f, new P(0.5f)), (2f, new P(0.5f)) };
   var ev = Statistics.ExpectedValue(outcomes);  // Validates sum
   ```

4. **Use fixed random seeds** for reproducible tests:
   ```csharp
   var random = new Random(42);
   var result = p.Sample(random);
   ```

## Requirements

- .NET 10 or later
- C# 14.0

## License

[Your License Here]

## Contributing

Contributions welcome! Please ensure all tests pass and add tests for new features.

## Acknowledgments

Built with modern C# features including extensions, pattern matching, and file-scoped namespaces.
