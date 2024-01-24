﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Services
{
    public class ExpectationService : IExpectationService
    {
        private IMapper _mapper;
        private AppDbContext _appDbContext;
        public ExpectationService(IMapper mapper, AppDbContext appDbContext) 
        { 
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<List<ExpectationDto>> AddExpectation(ExpectationDto expectationDto)
        {
            var expection = _mapper.Map<Expectation>(expectationDto);
            _appDbContext.Add(expection);
           await _appDbContext.SaveChangesAsync();

            var expectationUp = await _appDbContext.Expectations
               .ProjectTo<ExpectationDto>(_mapper.ConfigurationProvider)
               .ToListAsync();

            return expectationUp;
        }
    }
}