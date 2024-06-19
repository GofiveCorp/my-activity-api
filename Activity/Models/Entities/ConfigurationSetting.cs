using System.ComponentModel.DataAnnotations;

namespace Activity.Models.Entities {
    public class ConfigurationSetting {
        [Key]
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
