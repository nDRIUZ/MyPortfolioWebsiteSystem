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
    public class WelcomePageModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWelcomePageRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public WelcomePageModel(
            UserManager<IdentityUser> userManager,
            IWelcomePageRepository repo,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public WelcomePageVM Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            public int Id { get; set; }
            [Display(Name = "Background Image")]
            public IFormFile Img { get; set; }
            [Display(Name = "Your job title")]
            [Required]
            public string WelcomeWorkingTitle { get; set; }
            [Display(Name = "Short text about you")]
            [Required]
            public string WelcomeText { get; set; }
        }
        private async Task LoadAsync(int Id)
        {
            if (Id != 0)
            {
                var getWelcomePage = await _repo.FindById(Id);
                if (getWelcomePage != null)
                {
                    Input = new WelcomePageVM
                    {
                        WelcomeWorkingTitle = getWelcomePage.WelcomeWorkingTitle,
                        WelcomeText = getWelcomePage.WelcomeText
                    };
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
            var getAll = await _repo.FindAll();
            var id = 0;
            if (getAll.Count > 0)
            {
                id = getAll.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                var tempLink = await _repo.FindById(id);
                if (tempLink.Img != null)
                { 
                    ViewData["imageLink"] = tempLink.Img.ToString();
                }
                ViewData["imageId"] = tempLink.Id;
            }
            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var getAll = await _repo.FindAll();
            string newFileName = "bg";
            var id = 0;
            if (getAll.Count > 0)
            {
                id = getAll.OrderByDescending(p => p.Id).First().Id;
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(id);
                return Page();
            }
            var existingInfo = await _repo.FindById(id);
            if (existingInfo != null)
            {
                var updatedInfo = _mapper.Map<WelcomePage>(existingInfo);
                if (Input.Img != null)
                {
                    string ext = System.IO.Path.GetExtension(Input.Img.FileName);
                    if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png" && ext.ToLower() != ".jpeg")
                    {
                        StatusMessage = "Only image files are allowed(.jpg, .jpeg and .png)";
                        return Page();
                    }
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    newFileName = "bg-image" + ext;
                    string filePath = Path.Combine(uploadsFolder, newFileName);
                    using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    Input.Img.CopyTo(fileStream);
                    var upload = _mapper.Map<WelcomePage>(Input);
                    upload.Img = newFileName;
                    if (newFileName != existingInfo.Img)
                    {
                        updatedInfo.Img = newFileName;
                        var setNewImageExt = await _repo.Update(updatedInfo);
                        if (!setNewImageExt)
                        {
                            throw new InvalidOperationException($"Unexpected error occurred while changing image.");
                        }
                    }
                }

                if (Input.WelcomeWorkingTitle != existingInfo.WelcomeWorkingTitle)
                {
                    updatedInfo.WelcomeWorkingTitle = Input.WelcomeWorkingTitle;
                    var setWelcomeWorkingTitle = await _repo.Update(updatedInfo);
                    if (!setWelcomeWorkingTitle)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting working title.");
                    }
                }
                if (Input.WelcomeText != existingInfo.WelcomeText)
                {
                    updatedInfo.WelcomeText = Input.WelcomeText;
                    var setWelcomeText = await _repo.Update(updatedInfo);
                    if (!setWelcomeText)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting welcome text.");
                    }
                }
            }
            else
            {
                if (Input.Img != null)
                {
                    string ext = System.IO.Path.GetExtension(Input.Img.FileName);
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    newFileName = "bg-image" + ext;
                    string filePath = Path.Combine(uploadsFolder, newFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Input.Img.CopyTo(fileStream);
                    }
                    var createInfo = _mapper.Map<WelcomePage>(Input);
                    createInfo.Img = newFileName;
                var CreatedSuccess = await _repo.Create(createInfo);
                if (!CreatedSuccess)
                {
                    throw new InvalidOperationException($"Unexpected error occurred while setting background image.");
                }
            } else
                {
                    var createInfo = _mapper.Map<WelcomePage>(Input);
                    var CreatedSuccess = await _repo.Create(createInfo);
                    if (!CreatedSuccess)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred while writing changes.");
                    }
                }
            }


            StatusMessage = "Your welcome page has been updated";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var image = await _repo.FindById(id);
            if (image == null)
            {
                return NotFound();
            }
            image.Img = null;
            await _repo.Update(image);
            return RedirectToPage();
        }
    }
}
