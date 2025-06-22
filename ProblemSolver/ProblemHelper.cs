using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolver
{
    /// <summary>
    /// Helper class for discovering and instantiating Problem implementations.
    /// </summary>
    public static class ProblemHelper
    {
        /// <summary>
        /// Gets all types that implement Project Euler problems.
        /// </summary>
        /// <returns>List of problem types</returns>
        public static List<Type> GetProblemTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !string.IsNullOrEmpty(t.FullName))
                .Where(t => t.FullName!.Contains("ProblemSolver.Problems.Problem"))
                .ToList();
        }

        /// <summary>
        /// Creates an instance of a specific problem by its ID.
        /// </summary>
        /// <param name="problemId">The Project Euler problem number</param>
        /// <returns>An instance of the problem implementation</returns>
        /// <exception cref="InvalidOperationException">Thrown when the problem is not found</exception>
        public static IProblem GetProblemInstance(int problemId)
        {
            Type? problem = GetProblemTypes()
                .FirstOrDefault(t => t.FullName == $"ProblemSolver.Problems.Problem{problemId:0000}");

            if (problem == null)
                throw new InvalidOperationException($"Problem {problemId:0000} not found");

            return (IProblem)Activator.CreateInstance(problem)!;
        }

        /// <summary>
        /// Creates instances of all available problem implementations.
        /// </summary>
        /// <returns>List of all problem instances</returns>
        public static List<IProblem> GetProblemInstances()
        {
            List<Type> problems = GetProblemTypes();

            return problems
                .Select(t => (IProblem)Activator.CreateInstance(t)!)
                .ToList();
        }
    }
}
