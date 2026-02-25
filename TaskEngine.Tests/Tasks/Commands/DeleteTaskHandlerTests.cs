using Moq;
using Xunit;
using AutoMapper;
using TaskEngine.Application.Tasks.Commands.DeleteTask;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Domain.Entities;

namespace TaskEngine.Tests.Tasks.Commands; // <--- Asegúrate que no sea .Task solo

public class DeleteTaskHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly DeleteTaskHandler _handler;

    public DeleteTaskHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _mapperMock = new Mock<IMapper>();

        // Usamos el orden correcto: Mapper primero, luego Repository
        _handler = new DeleteTaskHandler(_mapperMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Delete_Correctamente_CuandoExiste()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var task = new TaskItem { Id = taskId };

        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);
        _repositoryMock.Setup(r => r.DeleteAsync(task)).ReturnsAsync(1);

        var command = new DeleteTaskCommand(taskId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
    }
}