﻿using JS.Base.WS.API.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Base.WS.API.Services.IServices
{
    public interface ICommonService
    {
        List<RoleDto> GetRoles();
        List<UserDto> GetUsers();
    }
}