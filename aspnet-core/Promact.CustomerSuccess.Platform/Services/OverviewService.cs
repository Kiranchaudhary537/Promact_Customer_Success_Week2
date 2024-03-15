using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class OverviewService : ApplicationService,
        IOverviewService
    {
        IRepository<Overview, Guid> _repository;
        public OverviewService(IRepository<Overview, Guid> repository)
        {
            _repository = repository;
        }

public async Task<Overview> CreateAsync(CreateOverviewDto input)
        {
            try
            {
                var entity = ObjectMapper.Map<CreateOverviewDto, Overview>(input);
                await _repository.InsertAsync(entity, autoSave: true);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public async Task<List<Overview>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public  async Task<Overview> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task<Overview> UpdateAsync(Guid id, UpdateOverviewDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
            return entity;
        }
    }
}
