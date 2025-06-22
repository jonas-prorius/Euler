# Project Architecture Documentation

This document explains the architecture and design patterns used in the Project Euler solutions repository.

## Overview

The solution follows a clean architecture approach with clear separation of concerns, making it easy to maintain, test, and extend with new problem solutions.

## Architecture Layers

### 1. Data Layer (EulerDb)

**Purpose**: Handles data persistence and database operations.

**Components**:
- `EulerDbContext`: Entity Framework Core database context
- `EulerDbContextFactory`: Factory for creating database contexts
- `Entities/`: Database entity models (Problem, Test)

**Responsibilities**:
- Database schema definition
- Entity relationships
- Data access configuration

### 2. Domain Layer (EulerDomain)

**Purpose**: Contains business logic and domain services.

**Components**:
- `EulerRepo`: Main repository providing access to domain services
- `Repos/Problems`: Repository for problem-related operations
- `Repos/Tests`: Repository for test-related operations

**Responsibilities**:
- Business logic encapsulation
- Data access abstraction
- Domain-specific operations

### 3. Application Layer (ProblemSolver)

**Purpose**: Core application logic and problem implementations.

**Components**:
- `IProblem`: Interface defining problem contract
- `Problems/`: Individual problem implementations
- `ProblemHelper`: Problem discovery and instantiation
- `TestExtensions`: Extensions for test parameter handling

**Responsibilities**:
- Problem algorithm implementations
- Test execution and configuration
- Problem discovery mechanism

### 4. Utility Libraries

#### EulerMath
**Purpose**: Mathematical utilities specific to Project Euler problems.

**Components**:
- `Prime`: Prime number operations
- `IntegerProperties`: Number manipulation and properties
- `CollectionProperties`: Collection-based mathematical operations
- `Permutator`: Permutation generation utilities

#### EulerHelper
**Purpose**: General-purpose utility functions.

**Components**:
- `Miscellaneous`: Common utility functions (palindromes, sequences, etc.)

### 5. Infrastructure Layer (Prepper)

**Purpose**: Background services and data management.

**Components**:
- `Program`: Service host configuration
- `Workers/TestCreator`: Background service for test data creation

**Responsibilities**:
- Test data initialization
- Background processing
- Service configuration

### 6. Testing Layer (ProblemsTests)

**Purpose**: Automated testing and validation.

**Components**:
- `ProblemTests`: Test runner for problem validation

**Responsibilities**:
- Problem solution verification
- Test execution automation
- Regression testing

## Design Patterns

### Repository Pattern
The `EulerDomain` layer implements the repository pattern to abstract data access:

```csharp
public class EulerRepo
{
    public Problems Problems { get; }
    public Tests Tests { get; }
    // ...
}
```

### Factory Pattern
Database context creation uses the factory pattern for dependency injection:

```csharp
public class EulerDbContextFactory
{
    public EulerDbContext CreateDbContext() { ... }
}
```

### Strategy Pattern
Each problem implements the `IProblem` interface, allowing for polymorphic execution:

```csharp
public interface IProblem
{
    bool IsSelfContained { get; }
    Task<string> Run(Test test);
}
```

### Extension Methods
Utility functions are implemented as extension methods for better usability:

```csharp
public static bool IsPrime(this long number) { ... }
public static long DigitSum(this long number) { ... }
```

## Data Flow

### Problem Execution Flow

1. **Test Configuration**: Tests are configured with parameters in `TestCreator`
2. **Problem Discovery**: `ProblemHelper` discovers available problem implementations
3. **Parameter Deserialization**: `TestExtensions.GetParameters<T>()` deserializes test parameters
4. **Problem Execution**: The specific problem's `Run()` method is called
5. **Result Validation**: Test framework compares results with expected values

### Adding a New Problem Flow

1. **Implementation**: Create new `Problem{NNNN}.cs` in `Problems/` folder
2. **Configuration**: Add test configuration in `TestCreator`
3. **Discovery**: `ProblemHelper` automatically discovers the new problem
4. **Testing**: Run tests to validate implementation

## Configuration Management

### Problem Parameters
Problems that require configuration implement parameter classes:

```csharp
public class Problem0001Config : IProblemParameters
{
    public int Roof { get; set; }
}
```

### Test Configuration
Tests are configured with:
- **Problem ID**: Links to specific problem implementation
- **Is Main Test**: Distinguishes between sample and full tests
- **Parameters**: JSON-serialized configuration object
- **Expected Answer**: For validation (optional for main tests)

## Dependency Injection

The application uses .NET's built-in dependency injection:

```csharp
services.AddDbContextFactory<EulerDbContext>(options => ...);
services.AddSingleton(dbFactory);
services.AddSingleton(new EulerRepo(dbFactory));
```

## Error Handling

### Null Safety
The codebase uses nullable reference types and proper null checking:

```csharp
public static T GetParameters<T>(this Test test) where T : IProblemParameters
{
    if (string.IsNullOrEmpty(test.Parameters))
        throw new InvalidOperationException($"Test {test.Id} has no parameters");
    // ...
}
```

### Problem Discovery
Missing problems throw descriptive exceptions:

```csharp
if (problem == null)
    throw new InvalidOperationException($"Problem {problemId:0000} not found");
```

## Performance Considerations

### Database Efficiency
- Entity Framework with proper relationship configuration
- Async/await patterns for database operations
- Connection factory pattern for connection management

### Algorithm Optimization
- Mathematical utilities optimized for common operations
- Extension methods for clean, readable code
- Efficient prime number algorithms

### Memory Management
- Proper disposal patterns with `using` statements
- Factory pattern to manage object lifetimes
- Stateless problem implementations

## Testing Strategy

### Unit Testing
- Individual problem validation with known test cases
- Small test cases for algorithm verification
- Full problem validation for completeness

### Integration Testing
- End-to-end test execution
- Database integration testing
- Configuration validation

## Extensibility Points

### Adding New Problems
1. Implement `IProblem` interface
2. Add test configuration
3. Problem is automatically discovered

### Adding New Utilities
1. Mathematical functions → `EulerMath`
2. General utilities → `EulerHelper`
3. Follow established patterns and documentation

### Adding New Test Types
1. Extend `Test` entity if needed
2. Update `TestExtensions` for new parameter types
3. Modify test discovery logic if required

## Security Considerations

### Database Security
- Parameterized queries through Entity Framework
- Connection string configuration
- No direct SQL execution

### Input Validation
- Parameter validation in problem implementations
- Type-safe configuration classes
- Exception handling for invalid inputs

## Monitoring and Logging

### Built-in Logging
- .NET built-in logging framework
- Entity Framework query logging
- Application lifecycle logging

### Error Tracking
- Comprehensive exception handling
- Descriptive error messages
- Problem-specific error contexts

## Future Enhancements

### Potential Improvements
1. **Performance Metrics**: Add timing and memory usage tracking
2. **Parallel Execution**: Support for parallel problem solving
3. **Result Caching**: Cache results for expensive computations
4. **Web Interface**: Add web-based problem browser and runner
5. **Benchmarking**: Compare algorithm performance across solutions

### Architectural Evolution
1. **Microservices**: Split into focused services if scale requires
2. **Event Sourcing**: Track problem-solving history and attempts
3. **Plugin Architecture**: Support for external problem implementations
4. **Cloud Deployment**: Support for cloud-based execution

This architecture provides a solid foundation for solving Project Euler problems while maintaining code quality, testability, and extensibility.