using FindATrade.Services.Data;
using FindATrade.Web.ViewModels.Likes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FindATrade.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.likeService.SetLike(model.CompanyId, userId);

            var likesCount = await this.likeService.GetLikeCount(model.CompanyId);

            return new LikesOutputModel { LikesCount = likesCount };
        }
    }
}
