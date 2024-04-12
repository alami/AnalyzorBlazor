using AnalyzerBlasor.Services.Base;
using AnalyzorBlazor.Models;
using AnalyzorBlazor.Models.Dto;

namespace AnalyzorBlazor.Services
{
    public interface IComponentService
    {
        Task<List<Component>> Get();
        Task<List<CompReadOnlyDto>> GetAll();

        Task<Responses<Component>> Get(int id);
        Task<Responses<Component>> GetForUpdate(int id);
        Task<Responses<int>> Create(Component component);
        Task<Responses<int>> Edit(int id, Component component);
        Task<Responses<int>> Delete(int id);
    }
}
