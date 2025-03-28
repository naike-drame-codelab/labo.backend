using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Domain.Enums;

namespace Labo.Domain.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? MinElo { get; set; } 
        public int? MaxElo { get; set; }
        public Category[] Categories { get; set; } = null!;
        public Status Status { get; set; }
        public int CurrentRound { get; set; }
        public bool WomenOnly { get; set; }
        public DateTime EndOfRegistrationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public List<Member>? Players { get; set; }

    }
}
