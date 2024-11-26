using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Ticketier.Models;
using API_Ticketier.DTO;
using API_Ticketier.Models.Entities;
using AutoMapper;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;


namespace API_Ticketier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase


    {

        private readonly ApplicationDbContext context;
        //inject AutoMapper

        public readonly IMapper mapper;

        public TicketsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        [HttpPost]
        [Route("create")]

        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDTO createTicketDTO)
        {
            var newTicket = new Ticket();
            mapper.Map(createTicketDTO, newTicket);
            await context.Tickets.AddAsync(newTicket);
            await context.SaveChangesAsync();
            return Ok("Ticket saved successfully");

        }

        //read all
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GetTicketDTO>>> GetTickets()
        {
            //get ticket from context

            var tickets = await context.Tickets.ToListAsync();
            var convertedTickets = mapper.Map<IEnumerable<GetTicketDTO>>(tickets);
            return Ok(tickets);

        }
        //read ticket by id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetTicketDTO>> GetTicketById([FromRoute] long id)
        {
            var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound("ticket not found");
            }
            var convertedTickets = mapper.Map<GetTicketDTO>(ticket);
            return Ok(convertedTickets);
        }
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTicket([FromRoute] long id,[FromBody] UpdateTicketDTO updateTicketDTO)
        {
            var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound("ticket not found");
            }
            mapper.Map(updateTicketDTO, ticket);
            ticket.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
            return Ok("ticket updated");
        }
        //Delete
        [HttpDelete]
        [Route("deleted/{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] long id)
        {
            var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound("ticket not found");
            }
            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
            return Ok("ticket is deleted");
        }
    }
}
