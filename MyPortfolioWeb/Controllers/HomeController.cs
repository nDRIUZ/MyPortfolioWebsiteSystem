using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using MyPortfolioWeb.Models;

namespace MyPortfolioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<HomeController> _logger;
        private readonly IAwardRepository _awardRepo;
        private readonly IExperienceRepository _experienceRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly ISettingRepository _settingRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly ITestimonialRepository _testimonialRepo;
        private readonly IWelcomePageRepository _welcomepageRepo;
        private readonly IMapper _mapper;

        public HomeController(
            ILogger<HomeController> logger,
            IAwardRepository awardRepo,
            IExperienceRepository experienceRepo,
            IPortfolioRepository portfolioRepo,
            ISettingRepository settingRepo,
            ISkillRepository skillRepo,
            ITestimonialRepository testimonialRepo,
            IWelcomePageRepository welcomepageRepo,
            IMapper mapper)
        {
            _logger = logger;
            _awardRepo = awardRepo;
            _experienceRepo = experienceRepo;
            _portfolioRepo = portfolioRepo;
            _settingRepo = settingRepo;
            _skillRepo = skillRepo;
            _testimonialRepo = testimonialRepo;
            _welcomepageRepo = welcomepageRepo;
            _mapper = mapper;
        }

        [HttpGet]
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
            ViewData["Settings"] = _mapper.Map<ICollection<Setting>, List<SettingVM>>(getSettings);
            ViewData["Welcome"] = _mapper.Map<ICollection<WelcomePage>, List<WelcomePageShowVM>>(getWelcomePages);
            ViewData["Skills"] = _mapper.Map<ICollection<Skill>, List<SkillShowVM>>(getSkills);
            ViewData["Experiences"] = _mapper.Map<ICollection<Experience>, List<ExperienceVM>>(getExperiences).OrderByDescending(p => p.Current).ThenByDescending(p => p.EndDate).ToList<ExperienceVM>();
            ViewData["Portfolio"] = _mapper.Map<ICollection<Portfolio>, List<PortfolioShowVM>>(getPortfolios);
            ViewData["Awards"] = _mapper.Map<ICollection<Award>, List<AwardShowVM>>(getAwards);
            ViewData["Testimonials"] = _mapper.Map<ICollection<Testimonial>, List<TestimonialShowVM>>(getTestimonials);
            ViewData["ContactStatus"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost(ContactVM Input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(Input.Email);

                    var user = await _userManager.FindByNameAsync("Admin");
                    var emailToSend = user.Email;

                    msz.To.Add(emailToSend); 
                    msz.Subject = Input.Name;
                    msz.Body = Input.Text;
                    SmtpClient smtp = new SmtpClient();

                    // Change to your server and port
                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    // Change it to your email and password
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("yourSMTPemail", "yourPassword");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag["ContactStatus"] = "Message sent! I will get in touch soon!";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag["ContactStatus"] = "Unknown error! Message was not sent :(";
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
