﻿using RozetkaBackEnd.Core.Entites.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Interfaces
{
    public interface IJwtService
    {
        Task Create(RefreshToken token);
        Task Delete(RefreshToken token);
        Task Update(RefreshToken token);
        Task<RefreshToken?> Get(string token);
        Task<IEnumerable<RefreshToken>> GetAll();
    }
}
