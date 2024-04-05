using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnalyzorBlazor.Models
{
    public class DeviceComponent
    {
        [Key]
        public int Id { get; set; }

        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public virtual Component Component { get; set; }

        public Stages Stage { get; set; }
        public int? Value { get; set; }
        public int? Qty { get; set; }
        public string? Comment { get; set; }
        public int Pos { get; set; } = 1;
        public ComponentType Type { get; set; }
    }
}
