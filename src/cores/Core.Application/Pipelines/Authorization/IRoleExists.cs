﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Authorization
{
   public interface IRoleExists
    {

        public string[] Roles { get; }
    }
}
