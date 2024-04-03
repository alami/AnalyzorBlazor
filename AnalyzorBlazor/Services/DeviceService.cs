using Microsoft.AspNetCore.Mvc;
using AnalyzerBlasor.Data;
using AnalyzorBlazor.Models;
using Azure;
using AnalyzerBlasor.Services.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        public async Task<Responses<Device>> Get(int id)
        {
            Responses<Device> responses = new ();
            if (id == null || id == 0)
            {
                responses.Success = false;
                responses.Message = $"Device/Details/{id} = 0";
            }
            try
            {
                var obj = await _db.Device.FindAsync(id);                
                if (obj == null)
                {
                    responses.Success = false;
                    responses.Message = $"Device/Details/{id} Not found";
                } else
                {
                    responses.Data = obj;
                    responses.Success = true;
                };
            } catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Device/Details/{id} Exception {ex.Message}";
            }
            return responses;
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
                responses.Message = $"Device/Delete/{id} Exception {ex.Message}";
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
        public Task<Responses<Device>> GetForUpdate(int id)
        {
            throw new NotImplementedException();
        }
    }
}
