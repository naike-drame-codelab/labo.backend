using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Domain.Entities;
using Labo.Domain.Enums;

namespace Labo.Application.DTO
{
    public record class CreateTournamentResultDTO(Tournament t)
    {
        public int Id { get; set; } = t.Id;
        public string Name { get; set; } = t.Name;
        public string? Location { get; set; } = t.Location;
        public int MinPlayers { get; set; } = t.MinPlayers;
        public int MaxPlayers { get; set; } = t.MaxPlayers;
        public int? MinElo { get; set; } = t.MinElo;
        public int? MaxElo { get; set; } = t.MaxElo;
        public Category[] Categories { get; set; } = t.Categories;
        public Status Status { get; set; } = t.Status;
        public int CurrentRound { get; set; } = t.CurrentRound;
        public bool WomenOnly { get; set; } = t.WomenOnly;
        public DateTime EndOfRegistrationDate { get; set; } = t.EndOfRegistrationDate;
        public DateTime CreatedAt { get; set; } = t.CreatedAt;
        public DateTime LastUpdatedAt { get; set; } = t.LastUpdatedAt;

    }
}
