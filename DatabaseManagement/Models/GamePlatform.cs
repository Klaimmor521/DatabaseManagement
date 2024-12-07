using System;
using System.Collections.Generic;

namespace DatabaseManagement.Models
{
    public class GamePlatform
    {
        public int GamePlatformId { get; set; }
        public string PlatformName { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}