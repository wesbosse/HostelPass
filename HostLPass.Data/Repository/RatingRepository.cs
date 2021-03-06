﻿using HostLPass.Core.Domain;
using HostLPass.Core.Repository;
using HostLPass.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostLPass.Data.Repository
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
