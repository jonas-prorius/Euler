# Project Euler Solutions in C#/.NET

[![.NET](https://github.com/jonas-prorius/Euler/actions/workflows/dotnet.yml/badge.svg)](https://github.com/jonas-prorius/Euler/actions/workflows/dotnet.yml)

This repository contains a comprehensive solution framework for [Project Euler](https://projecteuler.net/) problems implemented in C#/.NET. The codebase is designed for extensibility, maintainability, and ease of adding new problem solutions.

## Repository Structure

```
├── EulerDb/              # Database layer with entities and Entity Framework context
├── EulerDomain/          # Domain services and repository pattern implementation
├── EulerHelper/          # General utility functions (palindromes, Collatz, etc.)
├── EulerMath/            # Mathematical utilities specific to Euler problems
├── Prepper/              # Background service for test data creation and setup
├── ProblemSolver/        # Core problem implementations and interfaces
│   ├── Problems/         # Individual problem solution classes (Problem0001.cs, etc.)
│   ├── IProblem.cs       # Problem interface definition
│   ├── ProblemHelper.cs  # Problem discovery and instantiation utilities
│   └── TestExtensions.cs # Extensions for test parameter handling
├── ProblemsTests/        # Unit tests for problem validation
└── docs/                 # Additional documentation and guides
```

## Key Components

### Problem Interface
All problems implement the `IProblem` interface:
- `IsSelfContained`: Indicates if the problem can run without external dependencies
- `Run(Test test)`: Executes the problem logic and returns the result

### Mathematical Utilities
- **EulerMath**: Prime number operations, integer properties, collections utilities
- **EulerHelper**: General purpose functions like palindrome checking, Collatz sequences

### Testing Framework
Problems are validated through a comprehensive testing system that supports:
- Parameterized test configurations
- Both small test cases and full problem verification
- Automatic problem discovery and execution

## Quick Start

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (for database-backed testing) or modify connection string for other providers

### Building the Solution
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

## Adding New Problems

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed instructions on adding new Project Euler problem solutions.

## Project Architecture

This solution uses a clean architecture approach:
1. **Data Layer** (EulerDb): Entity Framework models and database context
2. **Domain Layer** (EulerDomain): Business logic and repository patterns  
3. **Application Layer** (ProblemSolver): Problem implementations and core logic
4. **Utility Libraries** (EulerMath, EulerHelper): Reusable mathematical and helper functions
5. **Infrastructure** (Prepper): Background services for data management

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to add new problems and improve the codebase.
