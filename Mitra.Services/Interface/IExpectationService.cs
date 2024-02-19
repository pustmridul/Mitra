using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IExpectationService
    {
        Task<List<ExpectationAddDto>> AddExpectation(ExpectationAddDto expectationDto, int id);
        Task<ExpectationDto> GetExpByEventAndDonorId(int donorId, int eventId);
        Task<List<ExpectationDto>> GetNOtDonateYetByEventId(int eventId);

        Task<IPaginatedResponse<ExpectationDto>> GetExpectationList(int page, int pageSize);
    }
}
