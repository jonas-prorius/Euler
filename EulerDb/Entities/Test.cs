using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerDb.Entities
{
    [Table("test")]
    public class Test
    {
        protected internal Test()
        {
            Id = new();
            Problem = new();
            IsProblem = new();
        }

        public Test(Problem problem, bool isProblem, string? parameters, string? answer)
        {
            Id = new();
            Problem = problem;
            IsProblem = isProblem;
            Parameters = parameters;
            Answer = answer;
        }

        public Test(int problemId, bool isProblem, string? parameters, string? answer)
        {
            Id = new();
            ProblemId = problemId;
            Problem = new(problemId);
            IsProblem = isProblem;
            Parameters = parameters;
            Answer = answer;
        }

        public Test(Problem problem, bool isProblem, string? parameters)
        {
            Id = new();
            Problem = problem;
            IsProblem = isProblem;
            Parameters = parameters;
        }

        public Test(int problemId, bool isProblem, string? parameters)
        {
            Id = new();
            ProblemId = problemId;
            Problem = new(problemId);
            IsProblem = isProblem;
            Parameters = parameters;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("id", Order = 1)]
        public int Id { get; private set; }

        [Required]
        [Column("problem_id")]
        public int ProblemId { get; private set; }

        [Required]
        [Column("is_problem")]
        public bool IsProblem { get; set; }

        [Column("parameters")]
        public string? Parameters { get; set; }

        [Column("answer")]
        public string? Answer { get; set; }

        public virtual Problem Problem { get; set; }

        public override string ToString()
            => $"{nameof(ProblemId)}: {ProblemId}, {nameof(Parameters)}: {Parameters}";
    }
}
