namespace TaskEngine.Application.DTOs;

public record TaskUpdateDto(
    Guid Id,
    string Title,
    string Description,
    bool IsCompleted,
    Guid CategoryId
);