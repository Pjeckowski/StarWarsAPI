using AutoMapper;
using StarWars.Core.Domain;
using StarWars.Web.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace StarWars.Bootstrap
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<CharacterDTO, Character>()
                .ForMember(dest => dest.Friends, source => source.MapFrom(so => MapFriendsToDomain(so.Friends)))
                .ReverseMap()
                .ForMember(dest => dest.Friends, source => source.MapFrom(so => MapFriendsToDTO(so.Friends)));

            CreateMap<Episode, EpisodeDTO>()                
                .ReverseMap();

            CreateMap<Episode, EpisodeWithCharactersDTO>()
                .ForMember(dest => dest.Characters, opt => opt.MapFrom(so => MapEpisodeCharactersToDTO(so.Characters)))
                .ReverseMap();
        }

        private static List<Character> MapFriendsToDomain(List<string> source)
        {
            var friends = new List<Character>();

            if (null == source)
                return friends;

            foreach(var fName in source)
            {
                friends.Add(new Character { Name = fName });
            }
            return friends;
        }

        private static List<string> MapFriendsToDTO(List<Character> source)
        {
            var friends = new List<string>();

            if (null == source)
                return friends;

            foreach(var friend in source)
            {
                friends.Add(friend.Name);
            }

            return friends;
        }

        private static List<string> MapEpisodeCharactersToDTO(List<Character> source)
        {
            var characterNames = new List<string>();

            if (null == source)
                return characterNames;

            foreach (var character in source)
                characterNames.Add(character.Name);

            return characterNames;
        }
    }
}
