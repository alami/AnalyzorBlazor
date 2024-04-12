using System.ComponentModel.DataAnnotations;

namespace AnalyzorBlazor.Models.Dto
{
    public class CompReadOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? Qty { get; set; }
        public bool Visible { get; set; }
    }
}
