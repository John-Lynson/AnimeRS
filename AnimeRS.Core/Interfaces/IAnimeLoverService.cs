using AnimeRS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Interfaces
{
    public interface IAnimeLoverService
    {
        AnimeLover GetAnimeLoverById(int id);
        AnimeLover GetAnimeLoverByUsername(string username);
        AnimeLover GetByAuth0UserId(string auth0UserId);
        void AddAnimeLover(AnimeLover animeLover);
        void UpdateAnimeLover(AnimeLover animeLover);
        void DeleteAnimeLover(int id);
    }
}

