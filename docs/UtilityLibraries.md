# Utility Libraries Documentation

This document provides detailed information about the utility libraries available for solving Project Euler problems.

## EulerMath Library

The EulerMath library contains mathematical functions specifically designed for Project Euler problems.

### Prime Number Operations (Prime.cs)

#### `bool IsPrime(this long number)`
Determines if a number is prime using an optimized algorithm.

**Example:**
```csharp
long num = 17;
bool isPrime = num.IsPrime(); // returns true
```

#### `bool IsPrime(this long number, IEnumerable<long> allSmallerPrimes)`
Efficiently checks primality using a pre-calculated list of smaller primes.

**Example:**
```csharp
var primes = new List<long> { 2, 3, 5, 7, 11 };
bool isPrime = 13.IsPrime(primes); // returns true
```

#### `long NextPrime(this long after)`
Finds the next prime number after the given number.

**Example:**
```csharp
long nextPrime = 10L.NextPrime(); // returns 11
```

### Integer Properties (IntegerProperties.cs)

#### `bool IsDivisibleBy(this long number, long divisor)`
Checks if a number is divisible by another number.

**Example:**
```csharp
bool isDivisible = 15L.IsDivisibleBy(3); // returns true
```

#### `bool IsDivisibleBy(long number, long[] divisors)`
Checks if a number is divisible by all numbers in an array.

**Example:**
```csharp
var divisors = new long[] { 2, 3, 5 };
bool isDivisible = IsDivisibleBy(30, divisors); // returns true
```

#### `bool IsEven(this long number)`
Checks if a number is even.

**Example:**
```csharp
bool isEven = 8L.IsEven(); // returns true
```

#### `List<long> GetPrimeFactors(this long number)`
Returns all prime factors of a number.

**Example:**
```csharp
var factors = 12L.GetPrimeFactors(); // returns [2, 2, 3]
```

#### `long? GetFirstPrimeFactor(this long number)`
Returns the first (smallest) prime factor of a number.

**Example:**
```csharp
long? firstFactor = 15L.GetFirstPrimeFactor(); // returns 3
```

#### `long? GetLargestFactor(this long number)`
Returns the largest prime factor of a number.

**Example:**
```csharp
long? largestFactor = 15L.GetLargestFactor(); // returns 5
```

#### `long DigitSum(this long number)`
Calculates the sum of all digits in a number.

**Example:**
```csharp
long sum = 123L.DigitSum(); // returns 6 (1+2+3)
```

#### `long MinimizedDigitSum(this long number)`
Repeatedly sums digits until a single digit remains.

**Example:**
```csharp
long minimized = 789L.MinimizedDigitSum(); // returns 6 (7+8+9=24, 2+4=6)
```

#### `long DigitProduct(this long number)`
Calculates the product of all digits in a number.

**Example:**
```csharp
long product = 123L.DigitProduct(); // returns 6 (1*2*3)
```

### Collection Properties (CollectionProperties.cs)

#### `long Product(this IEnumerable<long> source)`
Calculates the product of all elements in a collection.

**Example:**
```csharp
var numbers = new List<long> { 2, 3, 4 };
long product = numbers.Product(); // returns 24
```

### Permutation Utilities (Permutator.cs)

Provides functionality for generating permutations of collections, useful for problems involving arrangement of elements.

## EulerHelper Library

The EulerHelper library contains general-purpose utility functions.

### Miscellaneous Utilities (Miscellaneous.cs)

#### `bool IsPalindrome(string s)`
Checks if a string reads the same forwards and backwards.

**Example:**
```csharp
bool isPalindrome = Miscellaneous.IsPalindrome("12321"); // returns true
```

#### `long NextCollatz(long current)`
Calculates the next number in the Collatz sequence (3n+1 problem).

**Example:**
```csharp
long next = Miscellaneous.NextCollatz(5); // returns 16 (3*5+1)
long next2 = Miscellaneous.NextCollatz(4); // returns 2 (4/2)
```

#### `List<long> CreateLongList(long from, long to, long interval = 1)`
Creates a list of consecutive numbers within a range.

**Example:**
```csharp
// Create list [1, 2, 3, 4, 5]
var list = Miscellaneous.CreateLongList(1, 5);

// Create list [2, 4, 6, 8, 10]
var evenList = Miscellaneous.CreateLongList(2, 10, 2);
```

## Common Usage Patterns

### Finding Prime Numbers
```csharp
// Check if a number is prime
if (number.IsPrime())
{
    // Handle prime number
}

// Find all primes up to a limit
var primes = new List<long>();
for (long i = 2; i <= limit; i++)
{
    if (i.IsPrime())
        primes.Add(i);
}
```

### Working with Digits
```csharp
// Sum of digits
long digitSum = number.DigitSum();

// Product of digits
long digitProduct = number.DigitProduct();

// Check if number is palindrome
bool isPalindromic = Miscellaneous.IsPalindrome(number.ToString());
```

### Mathematical Calculations
```csharp
// Calculate factorial using Product extension
var factorial = Miscellaneous.CreateLongList(1, n).Product();

// Find sum of multiples
var multiples = Miscellaneous.CreateLongList(1, 100)
    .Where(x => x % 3 == 0 || x % 5 == 0)
    .Sum();
```

### Performance Tips

1. **Prime checking**: For multiple prime checks, pre-calculate a list of known primes and use the overload that accepts the prime list.

2. **Large numbers**: Be aware of `long` overflow for very large calculations. Consider using `BigInteger` for problems requiring arbitrary precision.

3. **Digit operations**: For repeated digit operations on the same number, consider caching results.

4. **Collection operations**: The utility methods are optimized for readability. For performance-critical code, consider specialized implementations.

## Integration with Problem Solutions

These utilities are designed to work seamlessly with the Project Euler problem framework:

```csharp
public class Problem0001 : IProblem
{
    public bool IsSelfContained => true;

    public Task<string> Run(Test test)
    {
        var config = test.GetParameters<Problem0001Config>();
        
        // Use utility functions
        var result = Miscellaneous.CreateLongList(1, config.Limit - 1)
            .Where(n => n % 3 == 0 || n % 5 == 0)
            .Sum();
            
        return Task.FromResult(result.ToString());
    }
}
```

## Adding New Utilities

When adding new utility functions:

1. Place mathematical functions in `EulerMath`
2. Place general utilities in `EulerHelper`
3. Use extension methods where appropriate
4. Follow the established naming conventions
5. Add comprehensive XML documentation
6. Include usage examples in this documentation