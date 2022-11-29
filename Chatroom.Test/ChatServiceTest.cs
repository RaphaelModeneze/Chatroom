using AutoMapper;
using Chatroom.Models;
using Chatroom.Repository.Interfaces;
using Chatroom.Services;
using Chatroom.Services.Interfaces;
using Moq;
using Repository.Entities;

namespace Chatroom.Test
{
    public class ChatServiceTest
    {
        private Mock<IChatRepository> _chatRepositoryMock;
        private Mock<IRabbitMQService> _rabbitMQServiceMock;
        private Mock<IMapper> _mapperMock;
        private ChatService _chatService;

        [SetUp]
        public void Setup()
        {
            _chatRepositoryMock = new Mock<IChatRepository>();
            _rabbitMQServiceMock = new Mock<IRabbitMQService>();
            _mapperMock = new Mock<IMapper>();
            _chatService = new ChatService(_mapperMock.Object, _chatRepositoryMock.Object, _rabbitMQServiceMock.Object);
        }

        [Test]
        public async Task Test_GetMessages()
        {
            //arrange
            var chatmessages = MockHelper.GetMessages();
            var ListChatMessageViewModel = MockHelper.GetListChatMessageViewModel();
            _chatRepositoryMock.Setup(x => x.GetMessages(It.IsAny<int>())).Returns(Task.FromResult<IList<ChatMessage>>(chatmessages));
            _mapperMock.Setup(x => x.Map<List<ChatMessageViewModel>>(chatmessages)).Returns(ListChatMessageViewModel);

            //act
            var result = await _chatService.GetMessages(1, "123");

            //assert
            Assert.That(ListChatMessageViewModel, Is.EqualTo(result));
        }
    }
}