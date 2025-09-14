using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SfCompulsory_cs.Models
{
    [Table("USERS")]
    public class User
    {
        [Key]
        [Column("ID_USER")]
        public long IdUser { get; set; }

        [Required]
        [Column("CLIENT_NAME")]
        [MaxLength(255)]
        public string ClientName { get; set; }

        [Column("EMAIL")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Column("REGISTER_DATE")]
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

        [Column("BET_MAX_VALUE")]
        public double? BetMaxValue { get; set; }

        [Required]
        [Column("USERNAME")]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [Column("PASSWORD")]
        [MaxLength(255)]
        public string Password { get; set; }

        [Column("USER_PIX_KEY")]
        [MaxLength(255)]
        public string? UserPixKey { get; set; }
    }
}
