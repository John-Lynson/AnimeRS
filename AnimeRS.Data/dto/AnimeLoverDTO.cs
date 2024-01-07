using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Data.dto
{
    public class AnimeLoverDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Auth0UserId { get; set; }
    }
}