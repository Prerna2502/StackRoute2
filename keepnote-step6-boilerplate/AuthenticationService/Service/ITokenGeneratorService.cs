using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Service
{
    public interface ITokenGeneratorService
    {
        string GenerateJWTToken(string userid);
    }
}
