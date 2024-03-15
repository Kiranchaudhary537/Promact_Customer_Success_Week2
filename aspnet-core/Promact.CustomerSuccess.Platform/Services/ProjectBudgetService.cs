using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Service;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class ProjectBudgetService : ApplicationService,
        IProjectBudgetService
    {
        IRepository<ProjectBudget, Guid> _repository;
        public ProjectBudgetService(IRepository<ProjectBudget, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<ProjectBudget> CreateAsync(CreateProjectBudgetDto input)
        {
            var entity = ObjectMapper.Map<CreateProjectBudgetDto, ProjectBudget>(input);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id, autoSave: true);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed" + ex.ToString();
            }

        }

        public async Task<List<ProjectBudget>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<ProjectBudget> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task<ProjectBudget> UpdateAsync(Guid id, UpdateProjectBudgetDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
            return entity;
        }
    }
}
