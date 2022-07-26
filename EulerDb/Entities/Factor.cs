using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerDb.Entities
{
    [Table("factor")]
    public class Factor
    {
        protected internal Factor()
        {
            Number = new();
            FactorNumber = new();
        }

        public Factor(Number number, Number factorNumber)
        {
            Number = number;
            FactorNumber = factorNumber;
        }

        public Factor(long numberId, long factorNumberId)
        {
            NumberId = numberId;
            FactorNumberId = factorNumberId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("number_id", Order = 1)]
        public long NumberId { get; private set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("factor_number_id", Order = 2)]
        public long FactorNumberId { get; private set; }

        public virtual Number Number { get; set; }

        public virtual Number FactorNumber { get; set; }
    }
}
