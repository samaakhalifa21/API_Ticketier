using API_Ticketier.DTO;
using API_Ticketier.Models.Entities;
using AutoMapper;

namespace API_Ticketier.Models.AutoMapper
{
    public class AutoMapperConfigProfile: Profile
    {
        public AutoMapperConfigProfile()
        {
            //Tickets
            CreateMap<CreateTicketDTO, Ticket>();
            CreateMap<Ticket, GetTicketDTO>();
            CreateMap<UpdateTicketDTO, Ticket>();
        }
    }
}
