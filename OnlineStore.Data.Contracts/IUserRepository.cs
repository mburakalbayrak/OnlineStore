﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
