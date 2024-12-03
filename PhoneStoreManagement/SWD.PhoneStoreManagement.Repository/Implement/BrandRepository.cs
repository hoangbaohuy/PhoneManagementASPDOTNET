﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class BrandRepository : IBrandRepository
    {
        private readonly PhoneStoreDbContext _context;
        private readonly IMapper _mapper;
        public BrandRepository(PhoneStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Set<Brand>().ToListAsync();
        }
        public async Task CreateBrandssAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandssAsync(Brand brand)
        {
             _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }
    }
}
