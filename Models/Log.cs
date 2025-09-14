using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SfCompulsory_cs.Models
{
    [Table("LOGS")]
    public class Log
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [Column("LEVEL")]
        [MaxLength(50)]
        public string Level { get; set; }

        [Required]
        [Column("MESSAGE")]
        [MaxLength(1000)]
        public string Message { get; set; }

        [Column("TIMESTAMP")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // FK opcional para o User
        [Column("USER_ID")]
        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
