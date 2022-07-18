using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerDb.Entities
{
    [Table("question")]
    public class Problem
    {
        protected internal Problem()
        {
            Id = new();

            Tests = new List<Test>();
        }

        public Problem(int id)
        {
            Id = id;

            Tests = new List<Test>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("id", Order = 1)]
        public int Id { get; private set; }

        public virtual IList<Test> Tests { get; set; }
    }
}
