namespace FindATrade.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FindATrade.Services.Data;
    using FindATrade.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LikesController : BaseController
    {
        private readonly ILikeService likeService;

        public LikesController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LikesOutputModel>> Post(PostLikeInputModel model)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                await this.likeService.SetLike(model.CompanyId, userId);

                var likesCount = await this.likeService.GetLikeCount(model.CompanyId);

                return new LikesOutputModel { LikesCount = likesCount };
            }
            catch (System.Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }
        }
    }
}
