using AutoFixture;
using ChatBotStocksQuotes.Core.Entities;
using ChatBotStocksQuotes.Core.Implementations;
using ChatBotStocksQuotes.Core.Interfaces;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ChatBotStocksQuotes.Tests.Core
{
    public class ChatServiceTests
    {
        private readonly Mock<IChatProvider> _chatProvider;
        private readonly Mock<IChatRepository> _chatRepository;
        private readonly IChatService _chatService;
        private readonly Fixture _fixture;


        public ChatServiceTests()
        {
            _fixture = new Fixture();

            _chatProvider = new Mock<IChatProvider>();
            _chatRepository = new Mock<IChatRepository>();

            _chatService = new ChatService(_chatProvider.Object, _chatRepository.Object);
        }

        [Fact]
        public void AssertFindingAllChats()
        {
            //Arrange
            var chatListArrange = _fixture.Build<Chat>()
                                         .CreateMany(10)
                                         .ToList();

            _chatRepository.Setup(x => x.FindAll()).Returns(chatListArrange);

            //Act
            var chats = _chatService.FindAll();

            //Assert
            Assert.Equal(10, chats.Count());
        }

        [Fact]
        public void SigningToExistingChat()
        {
            //Arrange
            var chatid = Guid.NewGuid();

            var chat = _fixture.Build<Chat>()
                              .With(x => x.Id, chatid)
                              .Create();

            _chatRepository.Setup(x => x.Find(It.Is<Guid>(p => p == chatid))).Returns(chat);

            //Act
            var signedChat = _chatService.SignIn(chatid, "");

            //Assert
            Assert.NotNull(signedChat);
            Assert.Equal(chatid, signedChat.Id);

        }

        [Fact]
        public void SigningToNonExistentChat()
        {
            //Arrange
            var chatid = Guid.NewGuid();

            //Act
            var signedChat = _chatService.SignIn(chatid, "");

            //Assert
            Assert.Null(signedChat);

        }

    }
}
