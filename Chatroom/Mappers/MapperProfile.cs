using AutoMapper;
using Chatroom.Models;
using Repository.Entities;

namespace Chatroom.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ChatMessage, ChatMessageViewModel>()
                .ReverseMap();

            CreateMap<ChatRoom, ChatRoomViewModel>()
                .ReverseMap();
        }
    }
}
