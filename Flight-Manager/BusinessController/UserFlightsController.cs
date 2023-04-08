using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Policy;
using BusinessController;

namespace MVC.Controllers
{
    public class UserFlightsController : Controller
    {
        private readonly FmDbContext _context;
        private readonly UserManager<dbUser> _userManager;

        public UserFlightsController(FmDbContext context, UserManager<dbUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult FlightReserved()
        {
            return View();
        }
        public IActionResult FullPlaneError()
        {
            return View();
        }

        // GET: UserFlights
        public async Task<IActionResult> Index()
        {
            var fmDbContext = _context.UsersFlights.Include(u => u.Flight);
            return View(await fmDbContext.ToListAsync());
        }
    

        // GET: UserFlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFlight = await _context.UsersFlights
                .Include(u => u.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFlight == null)
            {
                return NotFound();
            }

            return View(userFlight);
        }

        // GET: UserFlights/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id");
            return View();
        }

        // POST: UserFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,dbUserId,FlightId")] UserFlight userFlight)
        {
            if (ModelState.IsValid)
            {
                userFlight.dbUserId = _context.Users. Where(x => x.UserName == LoginAutherification.userName).FirstOrDefault().Id;
                int flieths = _context.UsersFlights.Select(x => x.FlightId == userFlight.FlightId).Count();
                int reservationFlieths = _context.Reservations.Select(x => x.FlightId == userFlight.FlightId).Count();
                if (flieths + reservationFlieths >= _context.Flights.Where(x => x.Id == userFlight.FlightId).FirstOrDefault().AllPassengersCount)
                {
                    return View("FullPlaneError");
                }
                _context.Add(userFlight);
                await _context.SaveChangesAsync();
                return RedirectToAction("FlightReserved");
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", userFlight.FlightId);
            return View(userFlight);
        }

        // GET: UserFlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFlight = await _context.UsersFlights.FindAsync(id);
            if (userFlight == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", userFlight.FlightId);
            return View(userFlight);
        }

        // POST: UserFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,dbUserId,FlightId")] UserFlight userFlight)
        {
            if (id != userFlight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFlight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFlightExists(userFlight.Id))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", userFlight.FlightId);
            return View(userFlight);
        }

        // GET: UserFlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFlight = await _context.UsersFlights
                .Include(u => u.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFlight == null)
            {
                return NotFound();
            }

            return View(userFlight);
        }

        // POST: UserFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFlight = await _context.UsersFlights.FindAsync(id);
            _context.UsersFlights.Remove(userFlight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFlightExists(int id)
        {
            return _context.UsersFlights.Any(e => e.Id == id);
        }
    }
}
