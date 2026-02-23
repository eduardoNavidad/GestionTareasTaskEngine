namespace TaskEngine.Domain.Entities;

public class LoginResponseModel
{
    public string UserName { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }

}
