# Quick Reference Card

## Creating Probabilities
```csharp
new P(0.75f)        // From value
new P(3, 10)        // From ratio (3/10)
new P(6)            // Shorthand (1/6)
P.Impossible        // 0
P.Certain           // 1
P.Even              // 0.5
```

## Operators
```csharp
p1 & p2             // AND (independent)
p1 | p2             // OR (mutually exclusive)
~p                  // NOT (complement)
p1 + p2             // Addition
p1 - p2             // Subtraction
p * 0.5f            // Scalar multiply
p / 2.0f            // Scalar divide
p1 > p2             // Comparison
p1 == p2            // Equality
```

## Extensions
```csharp
p.Not()             // Complement
p1.And(p2)          // Independent AND
p1.Or(p2)           // Mutually exclusive OR
p1.OrDependent(p2)  // Non-mutually exclusive OR
p1.Given(p2)        // Conditional P(A|B)
p.AtLeastOne(n)     // At least one success in n trials
```

## Combinatorics
```csharp
Combinatorics.Factorial(n)
Combinatorics.Permutations(n, r)    // nPr
Combinatorics.Combinations(n, r)    // nCr
Combinatorics.BinomialCoefficient(n, k)
```

## Distributions

### Binomial
```csharp
Binomial.Probability(n, k, p)              // P(X = k)
Binomial.CumulativeProbability(n, k, p)    // P(X ≤ k)
Binomial.Mean(n, p)
Binomial.Variance(n, p)
Binomial.StandardDeviation(n, p)
```

### Normal
```csharp
Normal.Pdf(x, mean, stdDev)       // Probability density
Normal.Cdf(x, mean, stdDev)       // Cumulative probability
Normal.ZScore(x, mean, stdDev)    // Standard score
```

### Poisson
```csharp
Poisson.Probability(k, lambda)              // P(X = k)
Poisson.CumulativeProbability(k, lambda)    // P(X ≤ k)
Poisson.Mean(lambda)
Poisson.Variance(lambda)
Poisson.StandardDeviation(lambda)
```

## Statistics
```csharp
Statistics.ExpectedValue(outcomes)        // E[X]
Statistics.Variance(outcomes)             // Var[X]
Statistics.StandardDeviation(outcomes)    // σ
Statistics.Mode(outcomes)                 // Most likely
Statistics.Median(outcomes)               // 50th percentile
Statistics.Covariance(jointOutcomes)      // Cov(X,Y)
Statistics.Correlation(jointOutcomes)     // ρ
```

## Random Sampling
```csharp
p.Sample()                          // Single sample
p.Sample(n)                         // n samples
p.CountSuccesses(trials)            // Count true results
p.EstimateThroughSampling(n)        // Monte Carlo
RandomSampling.SampleFrom(outcomes) // Sample from distribution
```

## Bayes' Theorem
```csharp
BayesTheorem.Calculate(pA, pBGivenA, pB)
// P(A|B) = P(B|A) × P(A) / P(B)
```

## JSON Serialization
```csharp
JsonSerializer.Serialize(p)
JsonSerializer.Deserialize<P>(json)
```

## Formatting
```csharp
p.ToString()        // P(0.7500)
p.ToString("F2")    // P(0.75)
p.ToString("P0")    // P(75 %)
```

## Common Patterns

### Dice Roll
```csharp
var six = new P(1, 6);                      // Rolling a 6
var notSix = six.Not();                     // Not rolling a 6
var twoSixes = six & six;                   // Two 6s
var atLeastOneSix = six.AtLeastOne(4);      // At least one 6 in 4 rolls
```

### Coin Flips
```csharp
var heads = new P(0.5f);
var threeHeads = Binomial.Probability(3, 3, heads);  // All heads in 3 flips
```

### Card Games
```csharp
var totalHands = Combinatorics.Combinations(52, 5);
var royalFlush = new P(4, (int)totalHands);
```

### Quality Control
```csharp
var defectRate = new P(0.05f);
var defects = defectRate.CountSuccesses(1000);
```

### Medical Testing
```csharp
var pDisease = new P(0.01f);
var pPositiveGivenDisease = new P(0.99f);
var pPositive = new P(0.0199f);
var pDiseaseGivenPositive = BayesTheorem.Calculate(
    pDisease, pPositiveGivenDisease, pPositive
);
```
