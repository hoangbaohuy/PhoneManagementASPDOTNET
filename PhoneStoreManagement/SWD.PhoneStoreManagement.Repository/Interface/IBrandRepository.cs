﻿using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task CreateBrandssAsync(Brand brand);
        Task UpdateBrandssAsync(Brand brand);

    }
}
