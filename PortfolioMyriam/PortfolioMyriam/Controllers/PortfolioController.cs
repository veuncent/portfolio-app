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
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDatabaseService _databaseService;

        public PortfolioController(ApplicationDbContext context, IDatabaseService databaseService)
        {
            _context = context;
            _databaseService = databaseService;
        }

        // GET: Portfolio
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioItem.Select(pi => pi.ToViewModel(_context)).OrderBy(pi => pi.Title).ToListAsync());
        }

        // GET: Portfolio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItem
                .Include(pi => pi.ExternalReference)
                .Include(pi => pi.Project)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem.ToViewModel(_context));
        }

        // GET: Portfolio/Create
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create()
        {
            var viewModel = new PortfolioItemViewModel();
            viewModel.ProjectOptions = await _databaseService.GetProjectBaseViewModelList();

            return View(viewModel);
        }

        // POST: Portfolio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ExternalReference,ProjectId")] PortfolioItemViewModel portfolioItemViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioItemViewModel.ToEntity(_context));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioItemViewModel);
        }

        // GET: Portfolio/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItem
                .Include(pi => pi.ExternalReference)
                .Include(pi => pi.Project)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (portfolioItem == null)
            {
                return NotFound();
            }

            var viewModel = portfolioItem.ToViewModel(_context);
            viewModel.ProjectOptions = await _databaseService.GetProjectBaseViewModelList();

            return View(viewModel);
        }

        // POST: Portfolio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ExternalReference,ProjectId")] PortfolioItemViewModel portfolioItemViewModel)
        {
            if (id != portfolioItemViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioItemViewModel.ToEntity(_context));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(portfolioItemViewModel.Id))
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
            return View(portfolioItemViewModel);
        }

        // GET: Portfolio/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = await _context.PortfolioItem
                .Include(pi => pi.ExternalReference)
                .Include(pi => pi.Project)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem.ToViewModel(_context));
        }

        // POST: Portfolio/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Roles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioItem = await _context.PortfolioItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.PortfolioItem.Remove(portfolioItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int id)
        {
            return _context.PortfolioItem.Any(e => e.Id == id);
        }
    }
}
