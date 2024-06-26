using server_elearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Repository
{
    public interface IJWTManagerRepository
    { 
        Tokens Authenticate(Users users,UserID user);
    }
}
