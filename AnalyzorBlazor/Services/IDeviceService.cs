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
        Task<Responses<int>> Create(Device book);
        Task<Responses<int>> Edit(int id, Device book);
        Task<Responses<int>> Delete(int id);

    }
}
