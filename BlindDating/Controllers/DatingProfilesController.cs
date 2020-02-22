using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using BlindDating.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BlindDating.Controllers
{
    public class DatingProfilesController : Controller
    {
        private readonly BlindDatingContext _context;
        private UserManager<IdentityUser> _userManager;
        private IHostingEnvironment _webroot;

        public DatingProfilesController(BlindDatingContext context, UserManager<IdentityUser> userManager, IHostingEnvironment webroot )
        {
            _context = context;
            _userManager = userManager;
            _webroot = webroot;
        }

        // GET: DatingProfiles
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DatingProfile.ToListAsync());
        }

        // GET: DatingProfiles
        [Authorize]
        public async Task<IActionResult> Browse()
        {
            return View(await _context.DatingProfile.ToListAsync());
        }

        // GET: DatingProfiles/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datingProfile = await _context.DatingProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datingProfile == null)
            {
                return NotFound();
            }

            return View(datingProfile);
        }

        [Authorize]
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datingProfile = await _context.DatingProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datingProfile == null)
            {
                return NotFound();
            }

            return View(datingProfile);
        }

        // GET: DatingProfiles/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public IActionResult ProfileInfo()
        {
            string userID = _userManager.GetUserId(User);
            DatingProfile profile = _context.DatingProfile.FirstOrDefault(p => p.UserAccountId == userID);

            if (profile == null)
            {
                return RedirectToAction("Create");

            }


            return View(profile);

        }

        // POST: DatingProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,Gender,Bio,UserAccountId, DisplayName")] DatingProfile datingProfile,
            IFormFile FilePhoto)
        { 

            if (FilePhoto.Length > 0)
            {

                string photoPath = _webroot.WebRootPath + "\\userPhotos\\";
                var fileName = Path.GetFileName(FilePhoto.FileName);

            using (var stream = System.IO.File.Create(photoPath + fileName))
            {
                await FilePhoto.CopyToAsync(stream);
                    datingProfile.PhotoPath = fileName;
}
            }


        
            if (ModelState.IsValid)
            {
                _context.Add(datingProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProfileInfo");
            }
            return View(datingProfile);
        }

        // GET: DatingProfiles/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datingProfile = await _context.DatingProfile.FindAsync(id);
            if (datingProfile == null)
            {
                return NotFound();
            }
            return View(datingProfile);
        }

        // POST: DatingProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,Gender,Bio,UserAccountId, DisplayName")] DatingProfile datingProfile)
        {
            if (id != datingProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datingProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatingProfileExists(datingProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(datingProfile);
        }

        // GET: DatingProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datingProfile = await _context.DatingProfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datingProfile == null)
            {
                return NotFound();
            }

            return View(datingProfile);
        }

        // POST: DatingProfiles/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datingProfile = await _context.DatingProfile.FindAsync(id);
            _context.DatingProfile.Remove(datingProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatingProfileExists(int id)
        {
            return _context.DatingProfile.Any(e => e.Id == id);
        }
    }
}
