using KaizenCaseStudyOne.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaizenCaseStudyOne.Api.Controllers
{
    [Route("api/codes")]
    [ApiController]
    public class PackagingCodesController : ControllerBase
    {
        private readonly IPackagingCodeService packagingCodeService;
        public PackagingCodesController(IPackagingCodeService packagingCodeService)
        {
            this.packagingCodeService = packagingCodeService;
        }

        [HttpGet("generate")]
        public IActionResult GetPackagingCode()
        {
            try
            {
                var packagingCode = packagingCodeService.GeneratePackagingCode();
                return Ok(packagingCode);
            }
            catch

            {
                ModelState.AddModelError("Error", "Error occured while generating token");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("verify/{packagingCode}")]
        public IActionResult CheckPackagingCode (string packagingCode)
        {
            try
            {
                var verificationResult = packagingCodeService.ValidatePackagingCode(packagingCode);
                return Ok(verificationResult);
            }
            catch

            {
                ModelState.AddModelError("Error", "Error occured while validating token");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
