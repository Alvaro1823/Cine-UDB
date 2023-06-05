using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarteleraCine.Models;

namespace CarteleraCine.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly CarteleraCineContext _context;

        public PeliculasController(CarteleraCineContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
            var carteleraCineContext = _context.Peliculas.Include(p => p.IdDirectorNavigation).Include(p => p.IdGeneroNavigation);
            ViewData["Generos"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View(await carteleraCineContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int? idGenero)
        {
            var carteleraCineContext = _context.Peliculas.Include(p => p.IdDirectorNavigation).Include(p => p.IdGeneroNavigation).Where(p => p.IdGeneroNavigation.Id == idGenero);
            ViewData["Generos"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View(await carteleraCineContext.ToListAsync());
        }

        // GET: Peliculas
        public async Task<IActionResult> Ranking()
        {
            var carteleraCineContext = _context.Peliculas.Include(p => p.IdDirectorNavigation).Include(p => p.IdGeneroNavigation);
            return View(await carteleraCineContext.ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.IdDirectorNavigation)
                .Include(p => p.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["IdDirector"] = new SelectList(_context.Directors, "Id", "Nombre");
            ViewData["IdGenero"] = new SelectList(_context.Generos, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,IdGenero,IdDirector,Puntos,Imagen")] Pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                pelicula.Puntos = 0;
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDirector"] = new SelectList(_context.Directors, "Id", "Id", pelicula.IdDirector);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "Id", "Id", pelicula.IdGenero);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["IdDirector"] = new SelectList(_context.Directors, "Id", "Id", pelicula.IdDirector);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "Id", "Id", pelicula.IdGenero);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,IdGenero,IdDirector,Puntos,Imagen")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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
            ViewData["IdDirector"] = new SelectList(_context.Directors, "Id", "Id", pelicula.IdDirector);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "Id", "Id", pelicula.IdGenero);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.IdDirectorNavigation)
                .Include(p => p.IdGeneroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'CarteleraCineContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Peliculas/CountLike/${id}
        // POST: Generoes/Delete/5
        [HttpPost, ActionName("like")]
        public async Task<IActionResult> CountLike(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'CarteleraCineContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            
            if (pelicula != null)
            {
                pelicula.Puntos++;
                _context.Peliculas.Update(pelicula);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Peliculas/RankingChart
        public async Task<ActionResult> RankingChartAsync()
        {

            return _context.Peliculas != null ?
                          StatusCode(StatusCodes.Status200OK, await _context.Peliculas.Where(p => p.Puntos>0).OrderByDescending(p => p.Puntos).Take(5).ToListAsync()) :
                          Problem("Entity set 'CarteleraCineContext.Generos'  is null.");
        }

        public async Task<IActionResult> List()
        {
            var carteleraCineContext = _context.Peliculas.Include(p => p.IdDirectorNavigation).Include(p => p.IdGeneroNavigation);            
            return View(await carteleraCineContext.ToListAsync());
        }


    }
}
