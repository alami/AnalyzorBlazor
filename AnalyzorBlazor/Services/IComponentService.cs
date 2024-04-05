using AnalyzerBlasor.Services.Base;
using AnalyzorBlazor.Models;

namespace AnalyzorBlazor.Services
{
    public interface IComponentService
    {
        Task<List<Component>> Get();
        Task<Responses<Component>> Get(int id);
        Task<Responses<Component>> GetForUpdate(int id);
        Task<Responses<int>> Create(Component component);
        Task<Responses<int>> Edit(int id, Component component);
        Task<Responses<int>> Delete(int id);
    }
}
