﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto.GetDto
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int CategoryId { get; set; }


    }
}
