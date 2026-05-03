using Group4Flight.Models.DataLayer.Repositories;
using Group4Flight.Models.DomainModels;
using Group4Flight.Models.Utilities;
using Group4Flight.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group4Flight.Controllers
{
    public class HomeController : Controller
    {
private readonly IRepository<Flight> _flightRepository;
    private readonly IRepository<Airline> _airlineRepository;
        private readonly IRepository<Reservation> _reservationRepository;

    public HomeController(IRepository<Flight> flightRepository,
                          IRepository<Airline> airlineRepository,
                          IRepository<Reservation> reservationRepository)
    {
        _flightRepository = flightRepository;
        _airlineRepository = airlineRepository;
            _reservationRepository = reservationRepository;
        }

        // GET: /Home/Index — load filter from session, query DB, display results
        [HttpGet]
        public IActionResult Index()
        {
            var session = new FlightSession(HttpContext.Session);
            var savedFilter = session.GetFilter() ?? new FlightViewModel();

            var flights = ApplyFilter(_flightRepository.GetAll().Include(f => f.Airline), savedFilter)
                          .ToList()
                          .OrderBy(f => f.Date).ThenBy(f => f.DepartureTime).ToList();

            var vm = savedFilter;
            vm.Flights = flights;
            vm.Airlines = _airlineRepository.GetAll().ToList();
            vm.FromOptions = _flightRepository.GetAll().Select(f => f.From).Distinct().OrderBy(x => x).ToList();
            vm.ToOptions = _flightRepository.GetAll().Select(f => f.To).Distinct().OrderBy(x => x).ToList();

            ViewBag.SelectionCount = session.GetSelections().Count;
            return View(vm);
        }

        // POST: /Home/Index — save filter to session, redirect (PRG)
        [HttpPost]
        public IActionResult Index(FlightViewModel filter)
        {
            var session = new FlightSession(HttpContext.Session);
            session.SetFilter(filter);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Home/Detail/5
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var flight = _flightRepository.GetAll().Include(f => f.Airline)
                                         .FirstOrDefault(f => f.FlightId == id);
            if (flight == null) return NotFound();

            ViewBag.SelectionCount = new FlightSession(HttpContext.Session).GetSelections().Count;
            return View(flight);
        }

        // POST: /Home/Select — add flight to session selections (PRG)
        [HttpPost]
        public IActionResult Select(int id)
        {
            var flight = _flightRepository.GetById(id);
            if (flight != null)
            {
                var session = new FlightSession(HttpContext.Session);
                session.AddSelection(id);
                TempData["Message"] = $"Flight {flight.FlightCode} added to your selections!";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Home/Selections
        [HttpGet]
        public IActionResult Selections()
        {
            var session = new FlightSession(HttpContext.Session);

            var selectedIds = session.GetSelections();

            var selectedFlights = _flightRepository.GetAll().Include(f => f.Airline)
                                                   .Where(f => selectedIds.Contains(f.FlightId))
                                                   .ToList();

            // Get reservations from database (excluding expired ones)
            var now = DateTime.Now;
            var reservedFlights = _reservationRepository.GetAll()
                .Where(r => r.ExpiryDate > now)
                .Include(r => r.Flight)
                .ThenInclude(f => f.Airline)
                .Select(r => r.Flight)
                .Where(f => f != null)
                .Cast<Flight>()
                .ToList();

            ViewBag.SelectedFlights = selectedFlights;
            ViewBag.ReservedFlights = reservedFlights;
            ViewBag.SelectionCount = selectedIds.Count;
            return View();
        }

        // POST: /Home/Reserve — move flight from session to database reservation (PRG)
        [HttpPost]
        public IActionResult Reserve(int id)
        {
            var flight = _flightRepository.GetById(id);
            if (flight != null)
            {
                var session = new FlightSession(HttpContext.Session);

                // Check if already reserved in database
                var existingReservation = _reservationRepository.GetAll()
                    .FirstOrDefault(r => r.FlightId == id);

                if (existingReservation == null)
                {
                    // Create new reservation in database (expires in 14 days)
                    var reservation = new Reservation
                    {
                        FlightId = id,
                        ReservedDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddDays(14)
                    };

                    _reservationRepository.Add(reservation);
                    _reservationRepository.SaveChanges();
                }

                // Remove from session selections
                session.RemoveSelection(id);
                TempData["Message"] = $"Flight {flight.FlightCode} has been reserved for 2 weeks!";
            }
            return RedirectToAction(nameof(Selections));
        }

        // POST: /Home/ClearSelections — clear all session selections (PRG)
        [HttpPost]
        public IActionResult ClearSelections()
        {
            var session = new FlightSession(HttpContext.Session);
            session.ClearSelections();
            TempData["Message"] = "All selections cleared.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Home/Back — clear filter from session, redirect to Index (PRG)
        [HttpPost]
        public IActionResult Back()
        {
            var session = new FlightSession(HttpContext.Session);
            session.ClearFilter();
            return RedirectToAction(nameof(Index));
        }

        private static IQueryable<Flight> ApplyFilter(IQueryable<Flight> query, FlightViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.From))
                query = query.Where(f => f.From.Contains(filter.From));

            if (!string.IsNullOrWhiteSpace(filter.To))
                query = query.Where(f => f.To.Contains(filter.To));

            if (!string.IsNullOrWhiteSpace(filter.CabinType))
                query = query.Where(f => f.CabinType == filter.CabinType);

            if (DateTime.TryParse(filter.StartDate, out var start))
                query = query.Where(f => f.Date >= start);

            if (DateTime.TryParse(filter.EndDate, out var end))
                query = query.Where(f => f.Date <= end);

            return query.OrderBy(f => f.Date);
        }
    }
}
