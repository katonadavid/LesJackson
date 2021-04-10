using System.Collections.Generic;
using AutoMapper;
using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repo.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repo.GetCommandById(id);

            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }
            return NotFound();
        }
    }
}