﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Infrastructure
{
    public class JwtOptions
    {
        public required string Issuer { get; init; }

        public required string Audience { get; init; }

        public required string SecretKey { get; init; }
    }
}
