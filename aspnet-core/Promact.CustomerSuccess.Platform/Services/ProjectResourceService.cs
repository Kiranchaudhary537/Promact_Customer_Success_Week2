using Microsoft.AspNetCore.Authorization;
using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Service
{
    [Authorize]
    public class ProjectResourcesService : ApplicationService,
        IProjectResourceService
    {
        IRepository<ProjectResource, Guid> _repository;
        public ProjectResourcesService(IRepository<ProjectResource, Guid> repository)
        {
                _repository = repository;   
        }

        [Authorize("Project Resource Read")]
        public async Task<List<ProjectResource>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        [Authorize("Project Resource Read")]
        public async Task<ProjectResource> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }  


        [Authorize("Project Resource Create")]
        public async Task<ProjectResource> CreateAsync(CreateProjectResourceDto input)
        {
            var entity = ObjectMapper.Map<CreateProjectResourceDto, ProjectResource>(input);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        [Authorize("Project Resource Update")]
        public async Task<ProjectResource> UpdateAsync(Guid id, UpdateProjectResourceDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
            return entity;
        }

        [Authorize("Project Resource Delete")]
        public async Task<String> DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id, autoSave: true);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed"+ex.ToString();
            }

        }

    }

}
