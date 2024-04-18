using AnalyzerBlasor.Services.Base;
using AnalyzorBlazor.Models;
using AnalyzorBlazor.Models.Dto;
using Azure;

namespace AnalyzorBlazor.Services
{
    public interface IDeviceService
    {
        Task<List<Device>> Get();
        Task<Responses<Device>> Get(int id);
        Task<Responses<Device>> GetForUpdate(int id);
        Task<Responses<int>> Create(Device device);
        Task<Responses<int>> Edit(int id, Device device);
        Task<Responses<int>> Delete(int id);
        Task<Responses<int>> Next(int id,Stages stage);
        Task<List<DeviceComponent>> GetDevComp(int id, ComponentType type);
        Task<Responses<int>> EditAcc(int id, Device device, List<CompReadOnlyDto> AccList);
        Task<Responses<int>> EditComp(int id, Device device, List<CompReadOnlyDto> CompList, Stages stage, ComponentType compType);

    }
}
