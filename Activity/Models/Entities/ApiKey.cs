using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Activity.Models.Entities {
    public class ApiKey {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required string Value { get; set; }

        public ICollection<Activity> Activities { get; set; } = [];
    }
}
