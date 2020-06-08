using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using MyPortfolioWeb.Models;
using Newtonsoft.Json.Linq;

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
            if (TempData["Name"] != null && TempData["Email"] != null && TempData["Text"] != null)
            {
                ViewBag.Name = TempData["Name"].ToString();
                ViewBag.Email = TempData["Email"].ToString();
                ViewBag.Text = TempData["Text"].ToString();
            }
            ViewData["ContactStatus"] = null;
            if (TempData["contactStatus"] != null)
            {
                ViewBag.ContactStatus = TempData["contactStatus"].ToString();
            } else
            {
                ViewBag.ContactStatus = null;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactVM Input)
        {
            if (ModelState.IsValid)
            {
                if (!ReCaptchaPassed(HttpContext.Request.Form["gcaptcha"]))
                {
                    TempData["contactStatus"] = "Aren't you a robot? If not - email me at YOUREMAIL@MAIL.com";
                    return RedirectToAction("Index");
                } else {
                    try
                    {
                        SmtpClient client = new SmtpClient("smtp.gmail.com");
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("YOUR@gmail.com", "YourPassword");

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(Input.Email);
                        mailMessage.To.Add("EMAILTOSENDFORM@gmail.com");
                        mailMessage.Body = Input.Text;
                        mailMessage.Subject = Input.Name + " - " + Input.Email;
                        await client.SendMailAsync(mailMessage);

                        ModelState.Clear();
                        TempData["contactStatus"] = "Message sent! I will get in touch soon!";
                    }
                    catch (Exception ex)
                    {
                        TempData["contactStatus"] = "Unknown error :( Please contact me at YOUREMAIL@MAIL.com";
                        TempData["Name"] = Input.Name;
                        TempData["Email"] = Input.Email;
                        TempData["Text"] = Input.Text;
                        ModelState.Clear();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public static bool ReCaptchaPassed(string gRecaptchaResponse)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=YOUR_SECRET_API_KEY&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
                return false;

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
                return false;

            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
