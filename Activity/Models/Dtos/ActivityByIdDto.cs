using System.ComponentModel.DataAnnotations;

namespace Activity.Models.Dtos {
    public class ActivityByIdDto {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Type { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public required DurationDto Duration { get; set; }
        public required string Barometer { get; set; }
        public byte[]? Image { get; set; }
    }
}
