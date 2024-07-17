﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImageHandle
    {
        Task<string?> AddImage(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment);
    }
}