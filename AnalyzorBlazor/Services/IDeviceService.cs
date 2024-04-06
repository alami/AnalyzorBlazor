using AnalyzerBlasor.Services.Base;
using AnalyzorBlazor.Models;
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
        Task<Responses<int>> AEdit(int id, Device device);
        Task<List<DeviceComponent>> GetDevComp(int id, ComponentType type);

    }
}
