using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerDb.Entities
{
    [Table("factor")]
    public class Factor
    {
        protected internal Factor()
        {
            Id = new();
            Number = new();
            FactorNumber = new();
        }

        public Factor(Number number, Number factorNumber)
        {
            Id = new();
            Number = number;
            FactorNumber = factorNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("id", Order = 1)]
        public long Id { get; private set; }

        [Required]
        [Column("number_id")]
        public long NumberId { get; private set; }

        [Required]
        [Column("factor_number_id")]
        public long FactorNumberId { get; private set; }

        public virtual Number Number { get; set; }

        public virtual Number FactorNumber { get; set; }
    }
}
