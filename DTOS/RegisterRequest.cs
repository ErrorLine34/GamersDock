using System.ComponentModel.DataAnnotations;

namespace GamersDock.Dtos
{
    public record RegisterRequest
    {
        [Required]
        public string UserName { get; init; } = null!;

        [Required]
        public string Password { get; init; } = null!;
    }
}