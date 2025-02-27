using AAP.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAP.Application.Interfaces
{
    public interface IAccountService
    {
        Task Register(UserDTO userdto, string password);
        Task Login(UserDTO userdto);
    }
}
