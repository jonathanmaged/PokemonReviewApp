﻿using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.Dto.GetDto
{
    public class OwnerDto
    {
        [Required]
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        [Required]
        public int? CountryId { get; set; }
        
    }
}
