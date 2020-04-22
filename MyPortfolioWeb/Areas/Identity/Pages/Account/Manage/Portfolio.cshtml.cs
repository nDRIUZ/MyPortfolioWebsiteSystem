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
    public class PortfolioModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPortfolioRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PortfolioModel(
            UserManager<IdentityUser> userManager,
            IPortfolioRepository repo,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _repo = repo;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public PortfolioVM Input { get; set; }

        public IOrderedEnumerable<Portfolio> Portfolios { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync()
        {
            var getAll = await _repo.FindAll();
            if (getAll.Count > 0)
            {
                var sendAll = getAll.OrderByDescending(p => p.Id);
                if (sendAll != null)
                {
                    Portfolios = sendAll;
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

            var createInfo = _mapper.Map<Portfolio>(Input);
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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/portfolio");
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


            StatusMessage = "Item added to portfolio!";
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
