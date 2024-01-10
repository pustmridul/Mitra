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
        Task<List<ExpectationDto>> AddExpectation(Expectation expectationDto);
    }
}
