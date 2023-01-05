using AutoMapper;

namespace AllainNZWalks.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile() 
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();
        }
    }
}
