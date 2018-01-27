﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Entities
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
