﻿using CommonItems.Enums;

namespace CrimeService.Models.Dtos
{
    public class CrimeCreateDto
    {
        public CrimeType CrimeType { get; set; }
        public string? Description { get; set; }
        public string PlaceOfCrime { get; set; }
        public string WitnessEmail { get; set; }
        public string EnforcementId { get; set; }
    }
}
