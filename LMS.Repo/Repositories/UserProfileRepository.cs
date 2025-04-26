using LMS.Repo;

public interface IUserProfileRepository
{
    Task<UserProfile> AddUserProfileAsync(UserProfile userProfile);
}


public class UserProfileRepository : IUserProfileRepository
{
    private readonly AppDbContext _context;

    public UserProfileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfile> AddUserProfileAsync(UserProfile userProfile)
    {
        _context.UserProfile.Add(userProfile);
        await _context.SaveChangesAsync();
        return userProfile;
    }
}