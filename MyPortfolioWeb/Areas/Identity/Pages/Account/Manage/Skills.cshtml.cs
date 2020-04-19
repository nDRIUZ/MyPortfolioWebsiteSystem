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
    public class SkillsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISkillRepository _repo;
        private readonly IMapper _mapper;

        public SkillsModel(
            UserManager<IdentityUser> userManager,
            ISkillRepository repo,
            IMapper mapper)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
        }

        [BindProperty]
        public SkillVM Input { get; set; }

        public IOrderedEnumerable<Skill> Skills { get; set; }

        [TempData]
        public string StatusMessage { get; set; }



        public class InputModel
        {
            public int Id { get; set; }
            [Required]
            [Display(Name = "Name of the skill")]
            public string SkillName { get; set; }
            [Required]
            [Display(Name = "How well do you know it? In percentage")]
            public int SkillPercentage { get; set; }
        }

        private async Task LoadAsync()
        {
            var getAll = await _repo.FindAll();
            if (getAll.Count > 0)
            {
                var sendAll = getAll.OrderByDescending(p => p.SkillPercentage);
                if (sendAll != null)
                {
                    Skills = sendAll;
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

                    var createInfo = _mapper.Map<Skill>(Input);
                    var CreatedSuccess = await _repo.Create(createInfo);
                    if (!CreatedSuccess)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred while adding changes.");
                    }


            StatusMessage = "Great! New skill!";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var skill = await _repo.FindById(id);
            if (skill == null)
            {
                return NotFound();
            }
            await _repo.Delete(skill);
            return RedirectToPage();
        }
    }
}
