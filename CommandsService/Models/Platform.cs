using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Models
{
    public class Platform
    {
        [Required]
        public int Id { get; set; }
        
        [Key]
        [Required]
        public int ExternalID { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ICollection<Command> Commands { get; set; }
        public Platform()
        {
            Commands = new List<Command>();
        }
    }
}