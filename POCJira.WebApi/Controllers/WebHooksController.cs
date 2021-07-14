using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POCJira.Domain.Interfaces;
using POCJira.Shared.Models;
using POCJira.WebApi.DTOs.Requests;
using POCJira.WebApi.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POCJira.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WebHooksController : ControllerBase
    {
        private readonly IGetAllCardsService getAllCardsService;
        private readonly IUpdateSqlService updateSqlService;
        private readonly ICreateJiraCardService createJiraCardService;
        private readonly IMapper mapper;

        public WebHooksController(IGetAllCardsService getAllCardsService, IUpdateSqlService updateSqlService, ICreateJiraCardService createJiraCardService,  IMapper mapper)
        {
            this.getAllCardsService = getAllCardsService;
            this.updateSqlService = updateSqlService;
            this.createJiraCardService = createJiraCardService;
            this.mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCards()
        {
            var result = await getAllCardsService.GetAllCards();

            var response = mapper.Map<IEnumerable<ChamadoModel>, IEnumerable<ChamadoDto>>(result);

            return Ok(response);
        }

        [HttpPost("sql")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSqlCard([FromBody] UpdateSqlRequest input)
        {
            var inputModel = mapper.Map<UpdateSqlRequest, UpdateSqlModel>(input);

            await updateSqlService.UpdateSqlCard(inputModel);

            return Ok();
        }

        [HttpPost("jira")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJiraCard([FromBody] CreateJiraCardRequest input)
        {
            var inputModel = mapper.Map<CreateJiraCardRequest, CreateJiraCardModel>(input);

            var result = await createJiraCardService.CreateJiraCard(inputModel);

            return Ok(result.Id);
        }
    }
}
