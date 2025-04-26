using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserProfile([FromBody] UserProfile userProfile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdProfile = await _userProfileService.CreateUserProfileAsync(userProfile);
        return CreatedAtAction(nameof(AddUserProfile), new { id = createdProfile.Id }, createdProfile);
    }
}