using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

/// <summary>
/// AutoMapper mapping profile
/// </summary>
public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        // Source -> target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}