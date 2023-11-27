using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class AnimeLover
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        public string Role { get; private set; }

        public string Auth0UserId { get; private set; }

        public AnimeLover(int id, string username, string email, string role, string auth0UserId)
        {
            Id = id;
            Username = username;
            Email = email;
            Role = role;
            Auth0UserId = auth0UserId;
        }
    }
}