using System.ComponentModel.DataAnnotations;

namespace AnalyzorBlazor.Models
{
    public enum Stages
    {
        Init = 1,
        Tester = 2,
        Analyser = 3,
        Analyser2 = 4,
        Calculate = 5,
        Result = 6,
        Origin = 7,
    }
    public enum Сonclusions
    {
        ForOnHold = 1,
        For5050 = 2,
        ForParts = 3,
        BuyOnEconomics = 4,
    }
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Device Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "S/N")]
        public string? SN { get; set; }        
        [Display(Name = "Model")]
        public string? Model { get; set; }

        [Display(Name = "Model Name")]
        public string? ModelName { get; set; }
        [Display(Name = "Comments with specs(from Tester)")]
        public string? Comments { get; set; }
        [Display(Name = "Other Comments for Tester")]
        public string? OtherComments { get; set; }

        [Display(Name = "Qty Laptops")]
        [Range(1, int.MaxValue)]
        public int? TotalCount { get; set; }
        
        [Display(Name = "Tester Time (min)")]
        [Range(1, int.MaxValue)]
        public int? TesterTime { get; set; }

        [Display(Name = "% Lost")]        
        public float? PersntLost { get; set; }

        [Display(Name = "Hold unit, $")]        
        public float? HoldUnit { get; set; }

        [Display(Name = "% Result")]
        public float? PersntResult { get; set; }

        [Display(Name = "Result for 1 hold unit-B32")]
        public float? Result { get; set; }
        public int Type { get; set; } = 1;

        [Display(Name = "Other Comments for Analyzer)")]
        public string? OtherCommentsAz { get; set; }

        [Display(Name = "Result for All units-B33")]
        public float? TotalAll { get; set; }

        [Display(Name = "Different Result-B34")]
        public float? DiffResult { get; set; }

        [Display(Name = "Final Conclusion-BB")]
        public Сonclusions? Сonclusion { get; set; }
        public Stages? Stage { get; set; }

        [Display(Name = "Shipping price")]
        public int? Shipping_price { get; set; }
        [Display(Name = "Tester time price")]
        public int? Tester_time_price { get; set; }
        [Display(Name = "Receiver time")]
        public int? Receiver_time { get; set; }
        [Display(Name = "Receiver time price")]
        public int? Receiver_time_price { get; set; }
        [Display(Name = "Lister time")]
        public int? Lister_time { get; set; }
        [Display(Name = "Lister time price")]
        public int? Lister_time_price { get; set; }
        [Display(Name = "Fullfilment time")]
        public int? Fullfilment_time { get; set; }
        [Display(Name = "Fullfilment time price")]
        public int? Fullfilment_time_price { get; set; }
        [Display(Name = "Market fees")]
        public int? Market_fees { get; set; }
        [Display(Name = "Dissassembly time")]
        public int? Dissassembly_time { get; set; }
        [Display(Name = "Disasembler time price")]
        public int? Disasembler_time_price { get; set; }

        [Display(Name = "On hold/nit-F32")] 
        public float? F32 { get; set; }
        [Display(Name = "On hold for all-F33")] 
        public float? F33 { get; set; }
        [Display(Name = "On hold/unit-G32")] 
        public float? G32 { get; set; }
        [Display(Name = "On hold for all-G33")] 
        public float? G33 { get; set; }
        [Display(Name = "Create Date By Tester")]
        public DateTime? CreateT { get; set; }
        [Display(Name = "Last Update Date By Tester")]
        public DateTime? UpdateT { get; set; }
        [Display(Name = "Create Date By Analyzer")]
        public DateTime? CreateA { get; set; }
        [Display(Name = "Last Update Date By Analyzer")]
        public DateTime? UpdateA { get; set; }
        [Display(Name = "Template Name")]
        public string Template { get; set; }
    }
}
