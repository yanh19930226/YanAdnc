using Adnc.Infra.EfCore.MySQL.Transaction;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.EFCore.Data.MySql
{
    public class MySqlUnitOfWork<TDbContext> : UnitOfWork<TDbContext>
    where TDbContext : MySqlDbContext
    {
        private ICapPublisher? _publisher;

        public MySqlUnitOfWork(
            TDbContext context
            , ICapPublisher? publisher = null)
            : base(context)
        {
            _publisher = publisher;
        }

        protected override IDbContextTransaction GetDbContextTransaction(
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            , bool distributed = false)
        {
            if (distributed)
                if (_publisher is null)
                    throw new ArgumentException("CapPublisher is null");
                else
                    return AdncDbContext.Database.BeginTransaction(_publisher, false);
            else
                return AdncDbContext.Database.BeginTransaction(isolationLevel);
        }
    }
}
