﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GofromaniaWebApp.Data;
using GofromaniaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GofromaniaWebApp.Controllers
{
    public class OpiniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpiniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Opinies
        
        public async Task<IActionResult> Index()
        {
              return View(await _context.Opinie.ToListAsync());
        }

        // POST: Active Opinies
        public async Task<IActionResult> ActiveOpinies()
        {
            return View(await _context.Opinie.Where(j => j.Aktwyne.Equals(true)).ToListAsync());
        }

        // GET: Opinies/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Opinie == null)
            {
                return NotFound();
            }

            var opinie = await _context.Opinie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opinie == null)
            {
                return NotFound();
            }

            return View(opinie);
        }

        // GET: Opinies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opinies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocena,Opinia,Autor,Ip,Aktwyne,Admin")] Opinie opinie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opinie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ActiveOpinies));
            }
            return View(opinie);
        }

        // GET: Opinies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.userid = userId;


            if (id == null || _context.Opinie == null)
            {
                return NotFound();
            }

            var opinie = await _context.Opinie.FindAsync(id);
            if (opinie == null)
            {
                return NotFound();
            }
            return View(opinie);
        }

        // POST: Opinies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ocena,Opinia,Autor,Ip,Aktwyne,Admin")] Opinie opinie)
        {
            if (id != opinie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpinieExists(opinie.Id))
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
            return View(opinie);
        }

        // GET: Opinies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Opinie == null)
            {
                return NotFound();
            }

            var opinie = await _context.Opinie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opinie == null)
            {
                return NotFound();
            }

            return View(opinie);
        }

        // POST: Opinies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Opinie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Opinie'  is null.");
            }
            var opinie = await _context.Opinie.FindAsync(id);
            if (opinie != null)
            {
                _context.Opinie.Remove(opinie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpinieExists(int id)
        {
          return _context.Opinie.Any(e => e.Id == id);
        }
    }
}
