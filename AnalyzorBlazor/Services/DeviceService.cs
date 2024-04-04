using Microsoft.AspNetCore.Mvc;
using AnalyzerBlasor.Data;
using AnalyzorBlazor.Models;
using Azure;
using AnalyzerBlasor.Services.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;


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
                    _db.SaveChanges();
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
        public async Task<Responses<int>> Create(Device device)
        {
            Responses<int> responses = new();
            try
            {
                device.CreateT = DateTime.Now;
                _db.Device.Add(device);
                _db.SaveChanges();
                responses.Success = true;

            } catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"/Tester/create/ Exception {ex}";
            }
            return responses;
        }

        public async Task<Responses<int>> Edit(int id, Device device)
        {
            Responses<int> responses = new();
            try
            {
                device.UpdateT = DateTime.Now;
                _db.Device.Update(device);
                _db.SaveChanges();
                responses.Success = true;

            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"/Tester/edit/{id} Exception {ex}";
            }
            return responses;
        }
        public async Task<Responses<Device>> GetForUpdate(int id)
        {
            Responses<Device> responses = new();
            try
            {
                var data = await _db.Device.FindAsync(id);

                responses.Data = data;// mapper.Map<AuthorUpdateDto>(data),
                responses.Success = true;
                
            }
            catch (Exception ex) {
                responses.Success = false;
                responses.Message = $"/Tester/update/{id} Exception {ex}";
            }
            return responses;
        }
    }
}
