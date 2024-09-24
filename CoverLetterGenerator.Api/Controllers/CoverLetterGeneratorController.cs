using CoverLetterGenerator.Api.Models;
using CoverLetterGenerator.Service.Models;
using CoverLetterGenerator.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoverLetterGenerator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoverLetterGeneratorController : ControllerBase
    {
        private readonly ICoverLetterService _coverLetterService;

        public CoverLetterGeneratorController(ICoverLetterService coverLetterService)
        {
            _coverLetterService = coverLetterService;
        }


        [HttpPost(Name = "GenerateCoverLetter")]
        public IActionResult GenerateCoverLetter(CoverLetterViewModel coverLetterViewModel)
        {
            var coverLetterModel = new CoverLetterModel()
            {
                CompanyAddress = coverLetterViewModel.CompanyAddress,
                CompanyCity = coverLetterViewModel.CompanyCity,
                CompanyName = coverLetterViewModel.CompanyName,
                CompanyZip = coverLetterViewModel.CompanyZip,
                JobPosition = coverLetterViewModel.JobPosition
            };
            _coverLetterService.ProcessFile(coverLetterModel);
            return Ok();
        }





    }
}
