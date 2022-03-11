using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}