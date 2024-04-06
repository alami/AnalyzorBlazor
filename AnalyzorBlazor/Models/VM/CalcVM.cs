namespace AnalyzorBlazor.Models.VM
{
    public class CalcVM
    {
        public Device Device { get; set; }
        public List<DeviceComponent>? DevCompAssList { get; set; }
        public List<DeviceComponent>? DevCompPartsList { get; set; }
        public List<Component>? CompList { get; set; }
    }
}
