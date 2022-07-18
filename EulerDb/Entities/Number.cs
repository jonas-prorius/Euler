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

            Factors = new List<Number>();
            FactorToNumbers = new List<Number>();

            if (Id < 1)
                IsPrimeNumber = false;
        }

        public Number(long id)
        {
            Id = id;

            Factors = new List<Number>();
            FactorToNumbers = new List<Number>();

            if (Id < 1)
                IsPrimeNumber = false;
        }

        public Number(long id, bool? isPrimeNumber)
        {
            Id = id;
            IsPrimeNumber = isPrimeNumber;

            Factors = new List<Number>();
            FactorToNumbers = new List<Number>();

            if (Id < 1)
                IsPrimeNumber = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("id", Order = 1)]
        public long Id { get; private set; }

        [Column("is_prime_number")]
        public bool? IsPrimeNumber { get; set; }

        public virtual IList<Number> Factors { get; set; }

        public virtual IList<Number> FactorToNumbers { get; set; }

        public void SetPrime(IList<Number> allSmallerPrimes)
        {
            if (IsPrimeNumber.HasValue)
                return;

            if (allSmallerPrimes?.Any(n => !n.IsPrimeNumber.HasValue) ?? false)
                throw new Exception();

            IsPrimeNumber = Id.IsPrime(allSmallerPrimes.Select(p => p.Id).ToList());
        }
    }
}
