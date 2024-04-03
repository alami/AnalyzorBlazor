using Microsoft.AspNetCore.Mvc;
using AnalyzerBlasor.Data;
using AnalyzorBlazor.Models;
using Azure;
using AnalyzerBlasor.Services.Base;


namespace AnalyzorBlazor.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _db;
        public DeviceService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Device>> Get()
        {
            List<Device> objList = _db.Device.ToList();
            return objList;
        }
        public async Task<Responses<int>> Delete(int id)
        {
            Responses<int> responses = new();
            try
            {
                var obj = await _db.Device.FindAsync(id);
                if (obj == null)
                {
                    responses.Success = false;                    
                    responses.Message = $"Device/Delete/{id} Not found";                    
                }
                else
                {
                    _db.Device.Remove(obj);
                    //_db.SaveChanges();
                    responses.Success = true;
                    responses.Message = $"Device/Delete/{id} was deleted";
                }                
            } catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Device/Delete/{id} Exception";
            }
            return responses;
        }
        public Task<Responses<int>> Create(Device device)
        {
            throw new NotImplementedException();
        }

        public Task<Responses<int>> Edit(int id, Device device)
        {
            throw new NotImplementedException();
        }


        public Task<Responses<Device>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Responses<Device>> GetForUpdate(int id)
        {
            throw new NotImplementedException();
        }
    }
}
