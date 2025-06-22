# Contributing to Project Euler Solutions

Thank you for your interest in contributing to this Project Euler solutions repository! This guide will help you understand how to add new problem solutions and improve the existing codebase.

## Adding a New Problem Solution

### Step 1: Create the Problem Class

Create a new file in the `ProblemSolver/Problems/` directory following the naming convention `Problem{NNNN}.cs` where `NNNN` is the zero-padded problem number.

Example: `Problem0042.cs` for Project Euler problem #42.

### Step 2: Implement the Problem Interface

Use this template for your problem implementation:

```csharp
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerMath; // If you need mathematical utilities
using EulerHelper; // If you need general utilities

namespace ProblemSolver.Problems
{
    /// <summary>
    /// [Copy the problem statement from Project Euler here]
    /// </summary>
    public class Problem{NNNN} : IProblem
    {
        public bool IsSelfContained => true; // Set to false if you need external dependencies
        
        public Task<string> Run(Test test)
        {
            // If your problem needs parameters, get them like this:
            Problem{NNNN}Config config = test.GetParameters<Problem{NNNN}Config>();
            
            // Implement your solution logic here
            long result = SolveTheProblem(config);
            
            return Task.FromResult(result.ToString());
        }
        
        private long SolveTheProblem(Problem{NNNN}Config config)
        {
            // Your implementation here
            return 0;
        }
    }
    
    // Only create this class if your problem needs configuration parameters
    public class Problem{NNNN}Config : IProblemParameters
    {
        public int ExampleParameter { get; set; }
        // Add other parameters as needed
    }
}
```

### Step 3: Add Test Configuration

If your problem requires test data, add it to the `Prepper/Workers/TestCreator.cs` file in the `ExecuteAsync` method:

```csharp
// For problems that need parameters:
new Test({NNNN}, false, JsonConvert.SerializeObject(new Problem{NNNN}Config { ExampleParameter = 10 }), "expected_small_result"),
new Test({NNNN}, true, JsonConvert.SerializeObject(new Problem{NNNN}Config { ExampleParameter = 1000 })),

// For problems that don't need parameters:
new Test({NNNN}, true, null),
```

### Step 4: Test Your Implementation

1. Build the solution:
   ```bash
   dotnet build
   ```

2. Run the tests to verify your implementation:
   ```bash
   dotnet test
   ```

## Code Style Guidelines

### Naming Conventions
- **Classes**: PascalCase (e.g., `Problem0001`, `ProblemHelper`)
- **Methods**: PascalCase (e.g., `Run`, `GetProblemInstance`)
- **Properties**: PascalCase (e.g., `IsSelfContained`, `MaxValue`)
- **Variables**: camelCase (e.g., `result`, `config`)
- **Private fields**: _camelCase (e.g., `_dbContext`)

### Documentation
- Always include the Project Euler problem statement in the class XML documentation
- Add XML documentation for public methods and properties
- Use clear, descriptive variable names

### Performance Considerations
- Use appropriate data types (prefer `long` for large numbers)
- Consider algorithmic efficiency for complex problems
- Leverage existing utilities in `EulerMath` and `EulerHelper` when possible

## Available Utility Libraries

### EulerMath
Mathematical functions specifically useful for Project Euler problems:
- `Prime.IsPrime(long number)`: Check if a number is prime
- `Prime.NextPrime(long after)`: Find the next prime after a given number
- `IntegerProperties.GetPrimeFactors(long number)`: Get prime factorization
- `IntegerProperties.DigitSum(long number)`: Sum of digits
- `IntegerProperties.DigitProduct(long number)`: Product of digits
- `CollectionProperties.Product()`: Product of collection elements

### EulerHelper
General utility functions:
- `Miscellaneous.IsPalindrome(string s)`: Check if string is palindrome
- `Miscellaneous.NextCollatz(long current)`: Next number in Collatz sequence
- `Miscellaneous.CreateLongList(long from, long to, long interval)`: Generate number ranges

## Example: Complete Problem Implementation

Here's a complete example for Problem #6 (Sum square difference):

```csharp
using System.Linq;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerHelper;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// The sum of the squares of the first ten natural numbers is 1² + 2² + ... + 10² = 385.
    /// The square of the sum of the first ten natural numbers is (1 + 2 + ... + 10)² = 55² = 3025.
    /// Hence the difference between the sum of the squares and the square of the sum is 3025 − 385 = 2640.
    /// Find the difference between the sum of the squares and the square of the sum of the first one hundred natural numbers.
    /// </summary>
    public class Problem0006 : IProblem
    {
        public bool IsSelfContained => true;

        public Task<string> Run(Test test)
        {
            Problem0006Config config = test.GetParameters<Problem0006Config>();
            
            var numbers = Miscellaneous.CreateLongList(1, config.UpperLimit);
            
            long sumOfSquares = numbers.Sum(n => n * n);
            long sum = numbers.Sum();
            long squareOfSum = sum * sum;
            
            long difference = squareOfSum - sumOfSquares;
            
            return Task.FromResult(difference.ToString());
        }
    }

    public class Problem0006Config : IProblemParameters
    {
        public long UpperLimit { get; set; }
    }
}
```

## Troubleshooting

### Common Issues
1. **Nullable reference warnings**: Ensure proper null checking or use `!` operator when you're certain a value is not null
2. **Build errors**: Check that all necessary using statements are included
3. **Test failures**: Verify your algorithm with the small test case first

### Getting Help
- Review existing problem implementations for patterns
- Check the utility libraries for functions that might simplify your solution
- Ensure you understand the Project Euler problem statement completely

## Pull Request Guidelines

When submitting a pull request:
1. Include a clear description of which problem you've solved
2. Ensure all tests pass
3. Follow the established code style
4. Include both small test cases and the full problem solution
5. Verify that your solution produces the correct answer for the Project Euler problem

Thank you for contributing to this project!