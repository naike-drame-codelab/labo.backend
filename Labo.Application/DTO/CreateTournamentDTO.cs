using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labo.Application.Validators;
using Labo.Domain.Enums;

namespace Labo.Application.DTO
{
    public class CreateTournamentDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Location { get; set; }

        [Required]
        public int MinPlayers { get; set; }

        [Required]
        public int MaxPlayers { get; set; }

        [MinOrEqualMax]
        public int? MinElo { get; set; }
        [MinOrEqualMax]
        public int? MaxElo { get; set; }

        public required Category[] Categories { get; set; }

        [Required]
        [GreaterThanTodayAndMinPlayers]
        public DateTime EndOfRegistrationDate { get; set; }

        public bool WomenOnly { get; set; }

    }
}
