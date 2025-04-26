using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ArticleController : ControllerBase
{
    private readonly ArticleService _articleService;

    public ArticleController(ArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet("article/{userName}")]
    public async Task<IActionResult> GetArticles(string userName)
    {
        try
        {
            var articles = await _articleService.GetArticlesForUserAsync(userName);
            return Ok(articles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
