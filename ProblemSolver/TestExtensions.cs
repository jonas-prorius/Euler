using System;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ProblemSolver
{
    /// <summary>
    /// Extension methods for Test entity to handle parameter serialization and problem execution.
    /// </summary>
    public static class TestExtensions
    {
        /// <summary>
        /// Deserializes test parameters into the specified configuration type.
        /// </summary>
        /// <typeparam name="T">Configuration type implementing IProblemParameters</typeparam>
        /// <param name="test">The test instance</param>
        /// <returns>Deserialized configuration object</returns>
        /// <exception cref="InvalidOperationException">Thrown when parameters cannot be deserialized</exception>
        public static T GetParameters<T>(this Test test) where T : IProblemParameters
        {
            if (string.IsNullOrEmpty(test.Parameters))
                throw new InvalidOperationException($"Test {test.Id} has no parameters to deserialize");

            T? result = JsonConvert.DeserializeObject<T>(test.Parameters);
            if (result == null)
                throw new InvalidOperationException($"Failed to deserialize parameters for test {test.Id}");

            return result;
        }

        /// <summary>
        /// Sets test parameters by serializing the configuration object.
        /// </summary>
        /// <typeparam name="T">Configuration type implementing IProblemParameters</typeparam>
        /// <param name="test">The test instance</param>
        /// <param name="parameters">The configuration object to serialize</param>
        /// <param name="dbFactory">Database context factory</param>
        public static void SetParameters<T>(this Test test, T parameters, EulerDbContextFactory dbFactory) where T : IProblemParameters
        {
            using EulerDbContext db = dbFactory.CreateDbContext();
            Test? testEntity = db.Tests.First(t => t.Id == test.Id);
            testEntity.Parameters = JsonConvert.SerializeObject(parameters);
            db.SaveChanges();
        }

        /// <summary>
        /// Sets test parameters asynchronously by serializing the configuration object.
        /// </summary>
        /// <typeparam name="T">Configuration type implementing IProblemParameters</typeparam>
        /// <param name="test">The test instance</param>
        /// <param name="parameters">The configuration object to serialize</param>
        /// <param name="dbFactory">Database context factory</param>
        public static async Task SetParametersAsync<T>(this Test test, T parameters, EulerDbContextFactory dbFactory) where T : IProblemParameters
        {
            using EulerDbContext db = dbFactory.CreateDbContext();
            Test testEntity = await db.Tests.FirstAsync(t => t.Id == test.Id);
            testEntity.Parameters = JsonConvert.SerializeObject(parameters);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Executes the problem associated with this test.
        /// </summary>
        /// <param name="test">The test instance</param>
        /// <returns>The problem result as a string</returns>
        public static async Task<string> Run(this Test test)
            => await ProblemHelper.GetProblemInstance(test.ProblemId).Run(test);
    }
}
