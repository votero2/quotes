using QuotesApi.Data;
using QuotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuotesApi.Controllers
{
    public class QuotesController : ApiController
    {
        QuotesDbContext db = new QuotesDbContext();
        // GET: api/Quotes
        [HttpGet]
        public IHttpActionResult LoadQuotes(string sort)
        {
            IQueryable<Quote> quotes;
            switch(sort)
            {
                case "desc":
                   quotes =  db.Quotes.OrderByDescending(temp => temp.CreatedAt);
                break;

                case "asc":
                    quotes = db.Quotes.OrderBy(temp => temp.CreatedAt);
                break;
                default:
                    quotes = db.Quotes;
                    break;

            }
           
            return Ok(quotes);
            
        }


        public IHttpActionResult PagingQuote()
        {

        }

        //[HttpGet]
        //[Route("api/Quotes/Test/{id}")]
        //public int Test(int id)
        //{
        //    return id;
        //}

        // GET: api/Quotes/5
        [HttpGet]
        public IHttpActionResult LoadQuotes(int id)
        {
            var quote = db.Quotes.Find(id);
            if(quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

        // POST: api/Quotes
        public IHttpActionResult Post([FromBody]Quote quote)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Quotes.Add(quote);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        // PUT: api/Quotes/5
        public IHttpActionResult Put(int id, [FromBody]Quote quote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = db.Quotes.FirstOrDefault(temp => temp.Id == id);
            if(entity == null)
            {
                return BadRequest("No record found agaist this id");
            }
            entity.Title = quote.Title;
            entity.Author = quote.Author;
            entity.Description = quote.Description;
            db.SaveChanges();
            return Ok("Record updated successfully");
        }

        // DELETE: api/Quotes/5
        public IHttpActionResult Delete(int id)
        {
            var quote = db.Quotes.Find(id);
            if(quote == null)
            {
                return BadRequest("No record found agaist this id");
            }
            db.Quotes.Remove(quote);
            db.SaveChanges();
            return Ok("Quote deleted");
        }
    }
}
