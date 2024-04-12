using Microsoft.AspNetCore.Mvc;
using AnalyzerBlasor.Data;
using AnalyzorBlazor.Models;
using Azure;
using AnalyzerBlasor.Services.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using System;
using AnalyzorBlazor.Models.Dto;


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
                Template template = _db.Template.FirstOrDefault (u=>u.Name==device.Template);
                device.Shipping_price = template.Shipping_price;
                device.TesterTime = template.Tester_time;
                device.Tester_time_price = template.Tester_time_price;
                device.Receiver_time = template.Receiver_time;
                device.Receiver_time_price = template.Receiver_time_price;
                device.Lister_time = template.Lister_time;
                device.Lister_time_price = template.Lister_time_price;
                device.Fullfilment_time = template.Fullfilment_time;
                device.Fullfilment_time_price = template.Fullfilment_time_price;
                device.Market_fees = template.Market_fees;
                device.Dissassembly_time = template.Dissassembly_time;
                device.Disasembler_time_price = template.Disasembler_time_price;
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

        public async Task<List<DeviceComponent>> GetDevComp(int id, ComponentType type)
        {
            List<DeviceComponent> objList = _db.DeviceComponent.Where(u=>u.DeviceId==id&&u.Type==type).ToList();
            return objList;
        }
        public async Task<Responses<int>> EditAcc(int id, Device Device, List<CompReadOnlyDto> AccList)
        {
            Responses<int> responses = new();
            try
            {
                Device.Stage = Stages.Tester;
                //device.OtherComments = Device.OtherComments;
                Device.UpdateT = DateTime.Now;
                _db.Device.Update(Device);

                List<DeviceComponent> OldAccList =
                    _db.DeviceComponent.Where(u=>u.DeviceId==Device.Id && u.Type == ComponentType.Accessories && u.Stage== Stages.Tester).ToList();
                if (OldAccList.Count>1) _db.DeviceComponent.RemoveRange(OldAccList);

                for (int i = 0; i < AccList.Count(); i++)
                {
                    if (AccList[i].Visible)
                    {
                        DeviceComponent deviceComponent = new DeviceComponent()
                        {
                            DeviceId = Device.Id,
                            ComponentId = AccList[i].Id,
                            Type = ComponentType.Accessories, // stageTesterVM.AccessoriesList[i].Type,
                            Value = AccList[i].Price,
                            Qty = AccList[i].Qty,
                            Stage = Stages.Tester
                        };
                        _db.DeviceComponent.Add(deviceComponent);
                    }
                }
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

    }
}
