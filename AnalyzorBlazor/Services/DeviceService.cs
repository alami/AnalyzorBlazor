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
        public async Task<Responses<int>> Next(int id, Stages stage)
        {
            Responses<int> responses = new();
            try
            {
                var device = await _db.Device.FindAsync(id);
                if (device == null)
                {
                    responses.Success = false;
                    responses.Message = $"Device/Next/{id} Not found";
                }
                else
                {
                    if (stage == Stages.Analyser)
                    {                        
                    device.UpdateT = DateTime.Now;
                    device.Stage = Stages.Analyser;
                    } else if (stage == Stages.Result)
                    {
                        device.UpdateA = DateTime.Now;
                        device.Stage = Stages.Result;
                    }

                    _db.SaveChanges();
                    responses.Success = true;
                    responses.Message = $"Device {id} sent to Analyzor";
                }
            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Device/Next/{id} Exception {ex.Message}";
            }
            return responses;
        }
        public async Task<Responses<int>> Create(Device device)
        {
            Responses<int> responses = new();            
            try
            {
                Template template = _db.Template.FirstOrDefault (u=>u.Name==device.Template);
                if (device.TotalCount == null) device.TotalCount = 1;
                if (device.TesterTime == null) device.TesterTime = template.Tester_time;
                if (device.TesterTime2 == null) device.TesterTime2 = template.Tester_time2;
                if (device.Analyzer_time == null) device.TesterTime = template.Analyzer_time;
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
        public async Task<Responses<int>> EditByA(int id, Device device)
        {
            Responses<int> responses = new();
            try
            {
                device.UpdateA = DateTime.Now;
                _db.Device.Update(device);
                _db.SaveChanges();
                responses.Success = true;

            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"/analyzer/edit/{id} Exception {ex}";
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
                //Device.Stage = Stages.Tester;
                //device.OtherComments = Device.OtherComments;
                Device.UpdateT = DateTime.Now;
                _db.Device.Update(Device);

                List<DeviceComponent> OldAccList =
                    _db.DeviceComponent.Where(u=>u.DeviceId==Device.Id && u.Type == ComponentType.Accessories).ToList();
                                                        // && u.Stage== Stages.Tester
                if (OldAccList.Count>0) _db.DeviceComponent.RemoveRange(OldAccList);

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
                            Comment = AccList[i].Comment,
                            //Stage = Stages.Tester
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
        public async Task<Responses<int>> EditAccByA(int id, Device Device, List<CompReadOnlyDto> AccList)
        {
            Responses<int> responses = new();
            try
            {
                //Device.Stage = Stages.Tester;
                //device.OtherComments = Device.OtherComments;
                Device.UpdateT = DateTime.Now;
                _db.Device.Update(Device);

                List<DeviceComponent> OldAccList =
                    _db.DeviceComponent.Where(u=>u.DeviceId==Device.Id && u.Type == ComponentType.Accessories).ToList();
                                                                        // && u.Stage== Stages.Tester
                if (OldAccList.Count>0) _db.DeviceComponent.RemoveRange(OldAccList);

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
                            Comment = AccList[i].Comment,
                            //Stage = Stages.Analyser
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
        public async Task<Responses<int>> EditComp(int id, Device Device, List<CompReadOnlyDto> CompList, Stages stage, ComponentType compType)
        {
            Responses<int> responses = new();            
            try
            {
                Device.Stage = stage;
                if (stage == Stages.Tester)
                {
                    Device.UpdateT = DateTime.Now;//[Device.OtherComments]
                } else
                {
                    Device.UpdateA = DateTime.Now;//[Device.OtherComments]
                }
          
                List<DeviceComponent> OldList =
                    _db.DeviceComponent.Where(u=>u.DeviceId==Device.Id && u.Type == compType).ToList();
                                                // && u.Stage== stage
                if (OldList.Count>0) _db.DeviceComponent.RemoveRange(OldList);

                for (int i = 0; i < CompList.Count(); i++)
                {
                    if (CompList[i].Visible)
                    {
                        DeviceComponent deviceComponent = new DeviceComponent()
                        {
                            DeviceId = Device.Id,
                            ComponentId = CompList[i].Id,
                            Type = compType, // stageTesterVM.AccessoriesList[i].Type,
                            Value = CompList[i].Price,
                            Qty = CompList[i].Qty,
                            Comment = CompList[i].Comment,
                            //Stage = stage
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
                if(stage == Stages.Tester)
                    responses.Message = $"/tester/addacc/{id} : Exception {ex.Message}";
                else if (stage == Stages.Tester)
                    responses.Message = $"/analyser/addparts/{id} : Exception {ex.Message}";
                else responses.Message = $"Uknown '{stage}' : Exception {ex.Message}";
            }
            return responses;
        }

    }
}
