using System.ComponentModel.DataAnnotations;

namespace AnalyzorBlazor.Models.Dto
{
    public class CompReadOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int? Price { get; set; }
        [Range(1, int.MaxValue)]
        public int? Qty { get; set; }
        public string? Comment { get; set; }
        public bool Visible { get; set; }
    }
}
