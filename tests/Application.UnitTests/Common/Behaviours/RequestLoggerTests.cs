using LibraryApp.Application.Common.Behaviours;
using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Genres.Commands.CreateGenre;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace LibraryApp.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateGenreCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateGenreCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateGenreCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateGenreCommand() { Name = "New Genre", Description = "New Description"}, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateGenreCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateGenreCommand() { Name = "New Genre", Description = "New Description" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
