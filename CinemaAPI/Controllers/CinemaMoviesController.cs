using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaMoviesController : ControllerBase
    {
        private readonly AppDbContext entities;

        public CinemaMoviesController(AppDbContext db) {
        entities = db;
        }

        //Get: api/all Movies
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Movies>>> getAllMovies() {

            if(entities.Movies == null) {

            return NotFound();
            }

            var getMovies = await entities.Movies.ToListAsync();

            return Ok(getMovies);
        }

        //Get: api/ a movie by id

        [HttpGet("(Id)")]
        public async Task<ActionResult<Movies>> getMovies(int id) { 
        
        
        
        if(entities.Movies is  null)
            {
                return NotFound();
            }

        var getMovie = await entities.Movies.FindAsync(id);

            if(getMovie is null)
            {
                return NotFound();
            }

            return getMovie;
        }


        //Post: api/Movie
        [HttpPost]
        public async Task<ActionResult<Movies>> createMovie(Movies movie) { 
        
        
        entities.Movies.Add(movie);
            await entities.SaveChangesAsync();
            return CreatedAtAction(nameof(getMovies), new { Id=movie.Id}, movie);
        
        }

        // Put(Update): api/a Movie
        [HttpPut]
        public async Task<ActionResult<Movies>> updateMovie(int id, Movies movie) { 
        
        if(id!=movie.Id)
            {
                return BadRequest();
            }
        
        
        entities.Entry(movie).State = EntityState.Modified;

            try {
                await entities.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {

                if (!movieExist(id))
                {

                    return NotFound();

                }
                else { throw; }

                
            }
            return NoContent();
        }

        private bool movieExist(long id)
        {


            return (entities.Movies?.Any(m => m.Id == id)).GetValueOrDefault();
        }

        //Delete:api/ a movie

        [HttpDelete("{id}")]

        public async Task<ActionResult<Movies>> deleteMovie(int id) {


            if (entities.Movies is null) {
                return NotFound();
            }

            var movie = await entities.Movies.FindAsync(id);

            if(movie is null) {

                return NotFound();
            }

            entities.Movies.Remove(movie);
            await entities.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
