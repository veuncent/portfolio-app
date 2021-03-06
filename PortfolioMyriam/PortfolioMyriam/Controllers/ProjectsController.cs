﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioMyriam.Data;
using PortfolioMyriam.Models;
using PortfolioMyriam.Models.HelperClasses;
using PortfolioMyriam.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMyriam.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDatabaseService _databaseService;

        public ProjectsController(ApplicationDbContext context, IDatabaseService databaseService)
        {
            _context = context;
            _databaseService = databaseService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.Select(p => p.ToViewModel(_context)).OrderBy(p => p.Title).ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _databaseService.GetProjectDetails(id.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project.ToViewModel(_context));
        }

        // GET: Projects/Create
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View(new ProjectViewModel());
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProjectType,ImageUpload,PortfolioItems")] ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                await projectViewModel.SaveModelAsync(_context);
                return RedirectToAction(nameof(Index));
            }
            return View(projectViewModel);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(pi => pi.PortfolioItems)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            return View(project.ToViewModel(_context));
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProjectType,ImageUpload,PortfolioItems")] ProjectViewModel projectViewModel)
        {
            if (id != projectViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await projectViewModel.SaveModelAsync(_context);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(projectViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(projectViewModel);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project.ToViewModel(_context));
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Roles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(m => m.Id == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _context.PortfolioItem.Any(e => e.Id == id);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
