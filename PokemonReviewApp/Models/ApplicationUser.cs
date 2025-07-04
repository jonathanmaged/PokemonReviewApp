﻿using Microsoft.AspNetCore.Identity;

namespace PokemonReviewApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
    }
}
