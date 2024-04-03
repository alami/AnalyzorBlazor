using AnalyzorBlazor.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyzorBlazor.Models
{
    public class Template
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }

        [Display(Name = "% Lost")]
        public float PersntLost { get; set; } = 12.5F;

        [Display(Name = "% Result")]
        public float PersntResult { get; set; } = 10;

        [Display(Name = "Shipping price")]
        public int Shipping_price { get; set; } = 50;
        [Display(Name = "Tester time")]
        public int Tester_time { get; set; } = 150;
        [Display(Name = "Tester time price")]
        public int Tester_time_price { get; set; }
        [Display(Name = "Receiver time")]
        public int Receiver_time { get; set; }
        [Display(Name = "Receiver time price")]
        public int Receiver_time_price { get; set; }
        [Display(Name = "Lister time")]
        public int Lister_time { get; set; }
        [Display(Name = "Lister time price")]
        public int Lister_time_price { get; set; }
        [Display(Name = "Fullfilment time")]
        public int Fullfilment_time { get; set; }
        [Display(Name = "Fullfilment time price")]
        public int Fullfilment_time_price { get; set; }
        [Display(Name = "Market fees")]
        public int Market_fees { get; set; }
        [Display(Name = "Dissassembly time")]
        public int Dissassembly_time { get; set; }
        [Display(Name = "Disasembler time price")]
        public int Disasembler_time_price { get; set; }
        [Display(Name = "Position in the list")]
        public int Pos { get; set; } = 1;
        public bool Visible { get; set; } = true;
        public Stages Stage { get; set; } = Stages.Origin;

        public int? DeviceId { get; set; } = null;
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }

    }
}
