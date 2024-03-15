using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Promact.CustomerSuccess.Platform.Services.Service;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class AuditHistoryService : ApplicationService,
        IAuditHistoryService
    {
        private readonly IRepository<AuditHistory, Guid> _repository;
        public AuditHistoryService(IRepository<AuditHistory, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<AuditHistory> CreateAsync(CreateAuditHistoryDto input)
        {
            var entity = ObjectMapper.Map<CreateAuditHistoryDto, AuditHistory>(input);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        public async Task<string> DeleteAsyncById(Guid id)
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

        public  async Task<List<AuditHistory>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<AuditHistory> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task UpdateAsync(Guid id, UpdateAuditHistoryDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
        }
    }
}
