using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.Models.Entities {
    public class Activity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Type { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public required string Barometer { get; set; }
        public byte[]? Image { get; set; }

        public Guid ApiKeyId { get; set; }
        public required ApiKey ApiKey { get; set; }
    }
}
