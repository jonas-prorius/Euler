using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerDb.Entities
{
    [Table("key_value")]
    public class KeyValue
    {
        protected internal KeyValue()
        {
            Key = default;
        }

        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("key", Order = 1)]
        public string Key { get; private set; }

        [Column("value", Order = 2)]
        public string? Value { get; private set; }

        public override string ToString()
            => $"{Key}";
    }
}
