﻿using Adnc.Shared.Application.Contracts.Dtos;

namespace Adnc.Usr.Application.Contracts.Dtos;

public class RoleSetPermissonsDto : IDto
{
    public long RoleId { set; get; }
    public long[] Permissions { get; set; }
}