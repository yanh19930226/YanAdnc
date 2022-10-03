﻿using Adnc.Infra.Repository.Entities;
using System.Collections.ObjectModel;

namespace Adnc.Usr.Entities;

/// <summary>
/// 角色
/// </summary>
public class SysRole : Entity
{
    public long? DeptId { get; set; }

    public string Name { get; set; }

    public int Ordinal { get; set; }

    public long? Pid { get; set; }

    public string Tips { get; set; }

    public int? Version { get; set; }

    public virtual Collection<SysRelation> Relations { get; set; }
}