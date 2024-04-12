using AnalyzerBlasor.Data;
using AnalyzerBlasor.Services.Base;
using AnalyzorBlazor.Models;
using AnalyzorBlazor.Models.Dto;
using AutoMapper;

namespace AnalyzorBlazor.Services
{
    public class ComponentService : IComponentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper mapper;
        public ComponentService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            this.mapper = mapper;
        }
        public async Task<List<Component>> Get()
        {
            List<Component> objList = _db.Component.Where(u=>u.Type==ComponentType.Accessories||u.Type==ComponentType.Parts).ToList();
            return objList;
        }
        public async Task<List<CompReadOnlyDto>> GetAll()
        {
            List<Component> objList = _db.Component.Where(u=>u.Type==ComponentType.Accessories||u.Type==ComponentType.Parts).ToList();
            List<CompReadOnlyDto> CompReadOnlyDtoList = new();
            foreach (var item in objList)
            {
                CompReadOnlyDto comp = new ();
                comp = mapper.Map<CompReadOnlyDto>(item);
                CompReadOnlyDtoList.Add(comp);
            }
            return CompReadOnlyDtoList;
        }
        public async Task<Responses<Component>> Get(int id)
        {
            Responses<Component> responses = new();
            if (id == null || id == 0)
            {
                responses.Success = false;
                responses.Message = $"Component/Details/{id} = 0";
            }
            try
            {
                var obj = await _db.Component.FindAsync(id);
                if (obj == null)
                {
                    responses.Success = false;
                    responses.Message = $"Component/Details/{id} Not found";
                }
                else
                {
                    responses.Data = obj;
                    responses.Success = true;
                };
            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Component/Details/{id} Exception {ex.Message}";
            }
            return responses;
        }
        public async Task<Responses<int>> Delete(int id)
        {
            Responses<int> responses = new();
            try
            {
                var obj = await _db.Component.FindAsync(id);
                if (obj == null)
                {
                    responses.Success = false;
                    responses.Message = $"Component/Delete/{id} Not found";
                }
                else
                {
                    _db.Component.Remove(obj);
                    _db.SaveChanges();
                    responses.Success = true;
                    responses.Message = $"Component/Delete/{id} was deleted";
                }
            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Component/Delete/{id} Exception {ex.Message}";
            }
            return responses;
        }
        public async Task<Responses<int>> Create(Component component)
        {

            Responses<int> responses = new();
            try
            {                
                _db.Component.Add(component);
                _db.SaveChanges();
                responses.Success = true;

            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"/Tester/create/ Exception {ex}";
            }
            return responses;
        }
        public async Task<Responses<int>> Edit(int id, Component component)
        {
            Responses<int> responses = new();
            try
            {
                _db.Component.Update(component);
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
        public async Task<Responses<Component>> GetForUpdate(int id)
        {
            Responses<Component> responses = new();
            try
            {
                var data = await _db.Component.FindAsync(id);

                responses.Data = data;// mapper.Map<AuthorUpdateDto>(data),
                responses.Success = true;

            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"/Tester/update/{id} Exception {ex}";
            }
            return responses;
        }
    }
}
