using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Models;

namespace MyPortfolioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAwardRepository _awardRepo;
        private readonly IExperienceRepository _experienceRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly ISettingRepository _settingRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly ITestimonialRepository _testimonialRepo;
        private readonly IWelcomePageRepository _welcomepageRepo;
        public HomeController(
            ILogger<HomeController> logger,
            IAwardRepository awardRepo,
            IExperienceRepository experienceRepo,
            IPortfolioRepository portfolioRepo,
            ISettingRepository settingRepo,
            ISkillRepository skillRepo,
            ITestimonialRepository testimonialRepo,
            IWelcomePageRepository welcomepageRepo)
        {
            _logger = logger;
            _awardRepo = awardRepo;
            _experienceRepo = experienceRepo;
            _portfolioRepo = portfolioRepo;
            _settingRepo = settingRepo;
            _skillRepo = skillRepo;
            _testimonialRepo = testimonialRepo;
            _welcomepageRepo = welcomepageRepo;
        }

        public async Task<IActionResult> Index()
        {
            // ViewDatas to make menu
            List<string> menu = new List<string>();
            var getAwards = await _awardRepo.FindAll();
            var getExperiences = await _experienceRepo.FindAll();
            var getPortfolios = await _portfolioRepo.FindAll();
            var getSettings = await _settingRepo.FindAll();
            var getSkills = await _skillRepo.FindAll();
            var getTestimonials = await _testimonialRepo.FindAll();
            var getWelcomePages = await _welcomepageRepo.FindAll();
            if (getAwards.Count > 0)
            {
                menu.Add("Awards");
            }
            if (getExperiences.Count > 0)
            {
                menu.Add("Experiences");
            }
            if (getPortfolios.Count > 0)
            {
                menu.Add("Portfolios");
            }
            if (getSkills.Count > 0)
            {
                menu.Add("Skills");
            }
            if (getTestimonials.Count > 0)
            {
                menu.Add("Testimonials");
            }
            ViewData["Menu"] = menu;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
