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
        public string Role { get; private set; }
        public string Auth0UserId { get; private set; }

        public virtual ICollection<Review> Reviews { get; private set; } = new List<Review>();

        public AnimeLover(string username, string role, string auth0UserId)
        {
            Username = username;
            Role = role;
            Auth0UserId = auth0UserId;
        }
    }
}