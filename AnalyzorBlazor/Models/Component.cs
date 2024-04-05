using System.ComponentModel.DataAnnotations;

namespace AnalyzorBlazor.Models
{
    public enum ComponentType
    {
        Evaluate = 1,
        Parts = 2,
        Accessories = 3,
    }
    public class Component
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Comment")]
        public string? Comment { get; set; }
        [Display(Name = "Value")]
        [Range(1, int.MaxValue)]
        public int? Price { get; set; }
        [Display(Name = "Q-ty")]
        [Range(1, int.MaxValue)]
        public int? Time { get; set; }
        public bool Visible { get; set; }

        [Display(Name = "Pos.in list")]
        [Range(1, int.MaxValue)]
        public int Pos { get; set; } = 1;
        public ComponentType Type { get; set; }

    }
}
