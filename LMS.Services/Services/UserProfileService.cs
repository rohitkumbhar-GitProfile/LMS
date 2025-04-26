public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _repository;

    public UserProfileService(IUserProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile)
    {
        return await _repository.AddUserProfileAsync(userProfile);
    }
}