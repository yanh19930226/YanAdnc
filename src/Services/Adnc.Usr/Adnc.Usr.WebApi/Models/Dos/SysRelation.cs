﻿using Adnc.Infra.Repository.Entities;

namespace Adnc.Usr.Entities;

/// <summary>
/// 菜单角色关系
/// </summary>
public class SysRelation : Entity
{
    public long MenuId { get; set; }

    public long RoleId { get; set; }

    public virtual SysRole Role { get; set; }

    public virtual SysMenu Menu { get; set; }
}