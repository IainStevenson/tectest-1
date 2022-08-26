using Microsoft.Extensions.Logging;
using tectest1.Api.Domain;
using tectest1.Api.Domain.Models;

namespace tectest1.Api.Database
{
    public class MeterReadingUploadRepository : ICreateRepository<MeterReadingUpload>
    {

        private readonly DatabaseContext _context;
        private ILogger<MeterReadingUploadRepository> _logger;
        public MeterReadingUploadRepository(DatabaseContext context, ILogger<MeterReadingUploadRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
    
        bool ICreateRepository<MeterReadingUpload>.Create(MeterReadingUpload entity)
        {
            var existingNewerRecordCount = _context.MeterReadingUploads.Count( c=>c.AccountId == entity.AccountId &&
                                                                                    c.MeterReadingDateTime >= entity.MeterReadingDateTime );
            if (existingNewerRecordCount > 0)
            {
                return false;
            }
            try
            {
                _context.MeterReadingUploads.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding Meter Reading", ex);


            }
            return false;
        }
    }
}