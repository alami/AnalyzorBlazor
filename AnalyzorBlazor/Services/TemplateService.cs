using Microsoft.AspNetCore.Mvc;
using AnalyzerBlasor.Data;
using AnalyzorBlazor.Models;
using Azure;
using AnalyzerBlasor.Services.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;


namespace AnalyzorBlazor.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ApplicationDbContext _db;
        public TemplateService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Template>> Get()
        {            
            List<Template> objList = _db.Template.ToList();
            return objList;
        }
        public async Task<Responses<Template>> Get(int id)
        {
            Responses<Template> responses = new ();
            if (id == null || id == 0)
            {
                responses.Success = false;
                responses.Message = $"Template/Details/{id} = 0";
            }
            try
            {
                var obj = await _db.Template.FindAsync(id);                
                if (obj == null)
                {
                    responses.Success = false;
                    responses.Message = $"Template/Details/{id} Not found";
                } else
                {
                    responses.Data = obj;
                    responses.Success = true;
                };
            } catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Template/Details/{id} Exception {ex.Message}";
            }
            return responses;
        }
        public async Task<Responses<int>> Delete(int id)
        {
            Responses<int> responses = new();
            try
            {
                var obj = await _db.Template.FindAsync(id);
                if (obj == null)
                {
                    responses.Success = false;                    
                    responses.Message = $"Template/Delete/{id} Not found";                    
                }
                else
                {
                    _db.Template.Remove(obj);
                    //_db.SaveChanges();
                    responses.Success = true;
                    responses.Message = $"Template/Delete/{id} was deleted";
                }                
            } catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"Template/Delete/{id} Exception {ex.Message}";
            }
            return responses;
        }
        public async Task<Responses<int>> Create(Template template)
        {
            Responses<int> responses = new();
            try
            {                
                _db.Template.Add(template);
                _db.SaveChanges();
                responses.Success = true;

            }
            catch (Exception ex)
            {
                responses.Success = false;
                responses.Message = $"/template/create/ Exception {ex}";
            }
            return responses;
        }

        public async Task<Responses<int>> Edit(int id, Template device)
        {
            throw new NotImplementedException();
        }
        public async Task<Responses<Template>> GetForUpdate(int id)
        {
            Responses<Template> responses = new();
            try
            {
                var data = await _db.Template.FindAsync(id);

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
