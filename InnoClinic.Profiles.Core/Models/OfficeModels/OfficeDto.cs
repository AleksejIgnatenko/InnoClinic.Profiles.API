﻿namespace InnoClinic.Profiles.Core.Models.OfficeModels
{
    public class OfficeDto
    {
        public Guid Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string OfficeNumber { get; set; } = string.Empty;
        public string RegistryPhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
