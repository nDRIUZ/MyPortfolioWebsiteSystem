using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using MyPortfolioWeb.Models;

namespace MyPortfolioWeb.Areas.Identity.Pages.Account.Manage
{
    public class TestimonialsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITestimonialRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TestimonialsModel(
            UserManager<IdentityUser> userManager,
            ITestimonialRepository repo,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public TestimonialVM Input { get; set; }

        public IOrderedEnumerable<Testimonial> Testimonials { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync()
        {
            var getAll = await _repo.FindAll();
            if (getAll.Count > 0)
            {
                var sendAll = getAll.OrderByDescending(p => p.Date);
                if (sendAll != null)
                {
                    Testimonials = sendAll;
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            var createInfo = _mapper.Map<Testimonial>(Input);
            if (Input.Img != null)
            {
                var getAll= await _repo.FindAll();
                var getCount = getAll.Count();
                string ext = System.IO.Path.GetExtension(Input.Img.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png" && ext.ToLower() != ".jpeg")
                {
                    StatusMessage = "Only image files are allowed(.jpg, .jpeg and .png)";
                    return Page();
                }
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/testimonials");
                var newFileName = getCount++ + ext;
                string filePath = Path.Combine(uploadsFolder, newFileName);
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                Input.Img.CopyTo(fileStream);
                createInfo.Img = newFileName;
            } else
                {
                    createInfo.Img = null;
                }
            
            var CreatedSuccess = await _repo.Create(createInfo);
            if (!CreatedSuccess)
            {
                throw new InvalidOperationException($"Unexpected error occurred while adding changes.");
            }


            StatusMessage = "Testimonial added!";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var toDel = await _repo.FindById(id);
            if (toDel == null)
            {
                return NotFound();
            }
            await _repo.Delete(toDel);
            return RedirectToPage();
        }
    }
}
