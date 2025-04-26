public interface IUserProfileService
{
    Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile);
}