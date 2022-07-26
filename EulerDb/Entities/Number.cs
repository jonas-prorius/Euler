using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EulerMath;

namespace EulerDb.Entities
{
    [Table("number")]
    public class Number
    {
        protected internal Number()
        {
            Id = new();

            Factors = new List<Factor>();
            FactorToNumbers = new List<Factor>();

            if (Id < 1)
                IsPrimeNumber = false;
        }

        public Number(long id, bool? isPrimeNumber = null)
        {
            Id = id;
            IsPrimeNumber = isPrimeNumber;

            Factors = new List<Factor>();
            FactorToNumbers = new List<Factor>();

            if (Id < 1)
                IsPrimeNumber = false;
            else
                IsPrimeNumber = isPrimeNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("id", Order = 1)]
        public long Id { get; private set; }

        [Column("is_prime_number")]
        public bool? IsPrimeNumber { get; set; }

        [Column("has_factors")]
        public bool? HasFactors { get; set; }

        public virtual IList<Factor> Factors { get; set; }

        public virtual IList<Factor> FactorToNumbers { get; set; }
    }
}
