﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Repository.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsStartingUow { get; }

        [Obsolete("已经废弃，请使用BeginTransaction")]
        dynamic GetDbContextTransaction() { throw new Exception("已经放弃，请使用BeginTransaction"); }

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead, bool sharedToCap = false);

        void Rollback();

        void Commit();

        Task RollbackAsync(CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
