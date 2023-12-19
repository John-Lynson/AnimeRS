using AnimeRS.Core.Models;
using AnimeRS.Data.dto;

public class AnimeLoverConverter
{
    public static AnimeLoverDTO ConvertToDto(AnimeLover animeLover)
    {
        return new AnimeLoverDTO
        {
            Id = animeLover.Id,
            Username = animeLover.Username,
            Role = animeLover.Role
            // Voeg andere relevante velden toe
        };
    }

    public static AnimeLover ConvertToDomain(AnimeLoverDTO animeLoverDTO)
    {
        return new AnimeLover
        {
            // Aannemende dat AnimeLover een constructor heeft die deze velden accepteert
            Username = animeLoverDTO.Username,
            Role = animeLoverDTO.Role,
            // Voeg andere velden toe indien nodig
        };
    }
}
