using Microsoft.AspNetCore.Mvc;
using AnalyzerBlasor.Data;
using AnalyzorBlazor.Models;


namespace AnalyzorBlazor.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _db;

        public DeviceService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Device> Index()
        {
            List<Device> objList = _db.Device.ToList();
            return objList;

        }
    }
}
