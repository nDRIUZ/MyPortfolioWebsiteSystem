using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using MyPortfolioWeb.Models;

namespace MyPortfolioWeb.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISettingRepository _repo;
        private readonly IMapper _mapper;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            ISettingRepository repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public SettingVM Input { get; set; }

        public class SettingsModel
        {
            public int Id { get; set; }
            [Display(Name = "Facebook account link")]
            public string FacebookLink { get; set; }
            [Display(Name = "Instagram account link")]
            public string InstaLink { get; set; }
            [Display(Name = "LinkedIn account link")]
            public string LinkedInLink { get; set; }
            [Display(Name = "Twitter account link")]
            public string TwitterLink { get; set; }
            [Display(Name = "Full name")]
            public string FullName { get; set; }
            [Display(Name = "Link to your resume")]
            public string ResumeLink { get; set; }
        }

        private async Task LoadAsync(int Id)
        {
            if (Id != 0)
            {
                var settings = await _repo.FindById(Id);
                if (settings != null)
                {
                    Input = new SettingVM
                    {
                        FullName = settings.FullName,
                        FacebookLink = settings.FacebookLink,
                        InstaLink = settings.InstaLink,
                        LinkedInLink = settings.LinkedInLink,
                        TwitterLink = settings.TwitterLink,
                        ResumeLink = settings.ResumeLink
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
                id = getAll.OrderByDescending(p => p.Id).First().Id;
            }
            await LoadAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var getAll = await _repo.FindAll();
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
                var updatedInfo = _mapper.Map<Setting>(existingInfo);
                if (Input.FullName != existingInfo.FullName)
                {
                    updatedInfo.FullName = Input.FullName;
                    var setFullNameResult = await _repo.Update(updatedInfo);
                    if (!setFullNameResult)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting full name.");
                    }
                }
                if (Input.ResumeLink != existingInfo.ResumeLink)
                {
                    updatedInfo.ResumeLink = Input.ResumeLink;
                    var setResumeLink = await _repo.Update(updatedInfo);
                    if (!setResumeLink)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting resume link.");
                    }
                }
                if (Input.FacebookLink != existingInfo.FacebookLink)
                {
                    updatedInfo.FacebookLink = Input.FacebookLink;
                    var setFacebookLink = await _repo.Update(updatedInfo);
                    if (!setFacebookLink)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting Facebook link.");
                    }
                }
                if (Input.InstaLink != existingInfo.InstaLink)
                {
                    updatedInfo.InstaLink = Input.InstaLink;
                    var setInstaLink = await _repo.Update(updatedInfo);
                    if (!setInstaLink)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting Instagram link.");
                    }
                }
                if (Input.LinkedInLink != existingInfo.LinkedInLink)
                {
                    updatedInfo.LinkedInLink = Input.LinkedInLink;
                    var setLinkedInLink = await _repo.Update(updatedInfo);
                    if (!setLinkedInLink)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting LinkedIn link.");
                    }
                }
                if (Input.TwitterLink != existingInfo.TwitterLink)
                {
                    updatedInfo.TwitterLink = Input.TwitterLink;
                    var setTwitterLink = await _repo.Update(updatedInfo);
                    if (!setTwitterLink)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred setting Twitter link.");
                    }
                }
            } else
            {
                var createInfo = _mapper.Map<Setting>(Input);
                var CreatedSuccess = await _repo.Create(createInfo);
                if (!CreatedSuccess)
                {
                    throw new InvalidOperationException($"Unexpected error occurred while writing changes.");
                }
            }


            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
