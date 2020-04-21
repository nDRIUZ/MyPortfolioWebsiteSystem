using System;
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
    public class ExperiencesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExperienceRepository _repo;
        private readonly IMapper _mapper;

        public ExperiencesModel(
            UserManager<IdentityUser> userManager,
            IExperienceRepository repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
        }

        [BindProperty]
        public ExperienceVM Input { get; set; }

        public IOrderedEnumerable<Experience> Experiences { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync()
        {
            var getAll = await _repo.FindAll();
            if (getAll.Count > 0)
            {
                var sendAll = getAll.OrderByDescending(p => p.Current).ThenByDescending(p => p.EndDate);
                if (sendAll != null)
                {
                    Experiences = sendAll;
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
                    if (Input.Current == true)
            {
                Input.EndDate = DateTime.Now;
            }
                    var createInfo = _mapper.Map<Experience>(Input);
                    var CreatedSuccess = await _repo.Create(createInfo);
                    if (!CreatedSuccess)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred while adding changes.");
                    }


            StatusMessage = "Your work experience added!";
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
