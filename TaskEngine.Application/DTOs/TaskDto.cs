namespace TaskEngine.Application.DTOs;

public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    DateTime DueDate,
    bool IsCompleted,
    Guid CategoryId,
    string CategoryName // Solo enviamos el nombre, no el objeto completo
);