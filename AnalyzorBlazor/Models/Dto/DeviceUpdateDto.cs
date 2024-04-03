using System.ComponentModel.DataAnnotations;

namespace AnalyzorBlazor.Models.Dto
{
    public class DeviceUpdateDto
    {
        [Display(Name = "Device Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "S/N")]
        public string? SN { get; set; }
        [Display(Name = "Model")]
        public string? Model { get; set; }

        public string? Comments { get; set; }
        [Display(Name = "Other Comments for Tester")]
        public string? OtherComments { get; set; }

        [Display(Name = "Qty Laptops")]
        [Range(1, int.MaxValue)]
        public int? TotalCount { get; set; }

        [Display(Name = "Tester Time (min)")]
        [Range(1, int.MaxValue)]
        public int? TesterTime { get; set; }
    }
}
