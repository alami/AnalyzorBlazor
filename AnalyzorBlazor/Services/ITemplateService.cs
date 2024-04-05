using AnalyzerBlasor.Services.Base;
using AnalyzorBlazor.Models;
using Azure;

namespace AnalyzorBlazor.Services
{
    public interface ITemplateService
    {
        Task<List<Template>> Get();
        Task<Responses<Template>> Get(int id);
        Task<Responses<Template>> GetForUpdate(int id);
        Task<Responses<int>> Create(Template template);
        Task<Responses<int>> Edit(int id, Template template);
        Task<Responses<int>> Delete(int id);

    }
}
