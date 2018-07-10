using AutoMapper;
using DataAccessLayer.Entity;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EntityDTOMapper
    {

        /// <summary>
        /// Configure automapper registration
        /// </summary>
        public static MapperConfiguration Configure()
        {
            //.Mapper.Map//CreateMap<UserDTO,User>()
            var config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<UserDTO, User>().ReverseMap();
               cfg.CreateMap<TweetDTO, Tweet>().ReverseMap();
               cfg.CreateMap<HashDTO, Hash>().ReverseMap();
               //cfg.CreateMap<FollowDTO, Follow>().ReverseMap();
               //cfg.CreateMap<HashDTO, Hash>().ReverseMap();
               
               //please add here fof other entities and DTOs
               //For member is used when both entity have some property with different name
               //cfg.CreateMap<UserDTO, User>()
               //.ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country))
               //.ReverseMap();
           });
            
            return config;
        }
    }
}
