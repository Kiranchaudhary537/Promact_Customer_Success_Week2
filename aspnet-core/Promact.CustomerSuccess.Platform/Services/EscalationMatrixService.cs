using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class EscalationMatrixService : ApplicationService,
        IEscalationMatrixService
    {
        private readonly IRepository<EscalationMatrix, Guid> _repository;
        public EscalationMatrixService(IRepository<EscalationMatrix, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<EscalationMatrix> CreateAsync(CreateEscalationMatrixDto input)
        {
            var entity = ObjectMapper.Map<CreateEscalationMatrixDto, EscalationMatrix>(input);
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

        public async Task<List<EscalationMatrix>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<EscalationMatrix> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task UpdateAsync(Guid id, UpdateEscalationMatrixDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
        }
    }
}
