using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolver
{
    public static class ProblemHelper
    {
        public static List<Type> GetProblemTypes()
        {
            var a = AppDomain.CurrentDomain.GetAssemblies();
            var b = a.SelectMany(a => a.GetTypes());
            var c = b.Where(t => !string.IsNullOrEmpty(t?.FullName));
            var d = c.Where(t => t.FullName.Contains("ProblemSolver.Problems.Problem"));

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !string.IsNullOrEmpty(t?.FullName))
                .Where(t => t.FullName.Contains("ProblemSolver.Problems.Problem"))
                .ToList();
        }

        public static IProblem GetProblemInstance(int problemId)
        {
            var problem = GetProblemTypes()
                .First(t => t.FullName == $"ProblemSolver.Problems.Problem{problemId:0000}");

            return (IProblem)Activator.CreateInstance(problem);
        }

        public static List<IProblem> GetProblemInstances()
        {
            var problems = GetProblemTypes();

            return problems
                .Select(t => (IProblem)Activator.CreateInstance(t))
                .ToList();
        }
    }
}
