using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POCJira.Domain.Interfaces;
using POCJira.Repository.Interfaces;
using POCJira.Shared.Models;

namespace POCJira.Domain.Services
{
    public class GetAllCardsService : IGetAllCardsService
    {
        private readonly ICardsRepository cardsRepository;

        public GetAllCardsService(ICardsRepository cardsRepository)
        {
            this.cardsRepository = cardsRepository;
        }
        public async Task<IEnumerable<ChamadoModel>> GetAllCards()
        {
            return await cardsRepository.GetAllCards();
        }
    }
}
