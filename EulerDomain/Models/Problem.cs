using EulerDb;
using System;
using System.Collections.Generic;
using System.Linq;

using EulerDb;

using EulerMath;
using Microsoft.EntityFrameworkCore;
using DbE = EulerDb.Entities;

namespace EulerDomain.Models
{
    public class Problem : DbE.Problem
    {
        private readonly EulerDbContextFactory _dbFactory;

        public Problem(DbE.Problem problem, EulerDbContextFactory dbFactory) : base(problem.Id)
        {
            _dbFactory = dbFactory;
        }
    }
}
