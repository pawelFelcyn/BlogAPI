using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class PostMapConfiguration : IMapConfiguration
{
    public void ConfigureMappings(Profile profile)
    {
        profile.CreateMap<Post, PostDto>();

        profile.CreateMap<Post, PostDetailsDto>();

        profile.CreateMap<CreatePostDto, Post>()
            .ForMember(p => p.Created, c => c.MapFrom(s => DateTime.UtcNow))
            .ForMember(p => p.LastModyfied, c => c.MapFrom(s => DateTime.UtcNow));
    }
}
