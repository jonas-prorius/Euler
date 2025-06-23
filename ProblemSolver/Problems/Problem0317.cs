using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EulerDb.Entities;
using EulerHelper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProblemSolver.Problems
{
    /// <summary>
    /// A firecracker explodes at a height of 100 m above level ground.It breaks into a large number of very small fragments, which move in every direction; all of them have the same initial velocity of 20 m/s.
    /// We assume that the fragments move without air resistance, in a uniform gravitational field with g = 9.81 m/s2.
    /// Find the volume(in m3) of the region through which the fragments move before reaching the ground.Give your answer rounded to four decimal places.
    /// </summary>
    public class Problem0317 : IProblem
    {
        public bool IsSelfContained => true;

        public Task<string> Run(Test test)
        {
            Problem0317Config config = test.GetParameters<Problem0317Config>();

            List<Fragment> fragments = new();

            for (long d = 0; d < 360; d++)
            {
                fragments.Add(new Fragment
                {
                    X = 0,
                    Y = config.Height,
                    Vx = config.InitSpeed * Math.Cos(d * Math.PI / 180),
                    Vy = config.InitSpeed * Math.Sin(d * Math.PI / 180),
                });
            }

            double dTime = 1;
            Dictionary<double, double> maxPosistionLog = new();

            while (fragments.Any(f => f.Y > 0))
            {
                foreach (Fragment fragment in fragments.Where(f => f.Y > 0))
                    fragment.Vy -= ((config.Gravity / 2) * dTime);

                foreach (Fragment fragment in fragments.Where(f => f.Y > 0))
                {
                    fragment.X += fragment.Vx * dTime;
                    fragment.Y += fragment.Vy * dTime;

                    if (!maxPosistionLog.ContainsKey(fragment.Y) || maxPosistionLog[fragment.Y] < fragment.X)
                        maxPosistionLog[fragment.Y] = fragment.X;
                }

                foreach (Fragment fragment in fragments.Where(f => f.Y > 0))
                    fragment.Vy -= ((config.Gravity / 2) * dTime);
            }

            double area = 0;

            var maxPositionLogArray = maxPosistionLog.OrderByDescending(p => p.Key).ToArray();

            for (int pi = 0; pi < maxPositionLogArray.Length; pi++)
            {
                double dy = maxPositionLogArray[pi].Key;
                if (pi < maxPositionLogArray.Length - 1)
                    dy -= maxPositionLogArray[pi + 1].Key;

                area += Math.Abs(maxPositionLogArray[pi].Value) * dy;
            }

            double volume = area * 2 * Math.PI;

            return Task.FromResult((area * 2 * Math.PI).ToString("0.0000"));
        }
    }

    public class Fragment
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }
    }

    public class Problem0317Config : IProblemParameters
    {
        public double Height { get; set; }
        public double InitSpeed { get; set; }
        public double Gravity { get; set; }
    }
}
