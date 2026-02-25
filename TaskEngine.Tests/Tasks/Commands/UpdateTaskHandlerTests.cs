using Moq;
using Xunit;
using AutoMapper;
using TaskEngine.Application.Tasks.Commands.UpdateTask;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Domain.Entities;

namespace TaskEngine.Tests.Tasks.Commands;

public class UpdateTaskHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepoMock;
    private readonly Mock<ICategoryRepository> _categoryRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateTaskHandler _handler;

    public UpdateTaskHandlerTests()
    {
        _taskRepoMock = new Mock<ITaskRepository>();
        _categoryRepoMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();

        // Inyectamos los 3 mocks en el orden de tu constructor
        _handler = new UpdateTaskHandler(_taskRepoMock.Object, _mapperMock.Object, _categoryRepoMock.Object);
    }

    Fact]
public async System.Threading.Tasks.Task Update_ShouldReturnTaskDto_WhenEverythingIsOk()
    {
        // ARRANGE
        var taskId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var command = new UpdateTaskCommand(taskId, "Nuevo", "Desc", true, categoryId);

        var existingTask = new TaskItem { Id = taskId };
        var existingCategory = new Category { Id = categoryId };

        _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
        _categoryRepoMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);

        // Aquí es donde daba error: especificamos el retorno explícitamente si es necesario
        _taskRepoMock.Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
                     .Returns(System.Threading.Tasks.Task.FromResult(1)); // Usamos Returns en lugar de ReturnsAsync para evitar el conflicto del nombre Task

        // ACT
        var result = await _handler.Handle(command, CancellationToken.None);

        // ASSERT
        Assert.NotNull(result);
    }
}