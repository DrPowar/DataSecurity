namespace Server.DTO
{
    public record ChangePasswordRequest(Guid UserId, string OldPassword, string NewPassword);
}
