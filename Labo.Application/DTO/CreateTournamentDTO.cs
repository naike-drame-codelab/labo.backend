using System.ComponentModel.DataAnnotations;
using Labo.Application.Validators;
using Labo.Domain.Enums;

namespace Labo.Application.DTO
{
    [MinOrEqualMax]
    public record class CreateTournamentDTO(
        [Required] string Name,
        string? Location,
        [Required] int MinPlayers,
        [Required] int MaxPlayers,
        [Range(0, 3000)] int? MinElo,
        [Range(0, 3000)] int? MaxElo,
        [Required, MinLength(1, ErrorMessage = "At least one category is required.")] Category[] Categories,
        [Required, GreaterThanTodayAndMinPlayers] DateTime EndOfRegistrationDate,
        bool WomenOnly = false
    );
}
