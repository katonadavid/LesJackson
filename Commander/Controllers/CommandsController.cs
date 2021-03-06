using System.Collections.Generic;
using AutoMapper;
using Data;
using DTOs;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetCommandById" )]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repo.GetCommandById(id);

            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandDto)
        {
            // Creating a command from the commandDto ( source )
            var commandModel = _mapper.Map<Command>(commandDto);
            _repo.CreateCommand(commandModel);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDTO>(commandModel);

            // Returns a 201 Created response with a url in the Location header, generated by the first 2 params
            return CreatedAtRoute( nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto );
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO command)
        {
            var commandFromRepo = _repo.GetCommandById(id);

            if( commandFromRepo == null )
            {
                return NotFound();
            }
            //       map source,   destination
            _mapper.Map(command, commandFromRepo);
            _repo.UpdateCommand(commandFromRepo);
            _repo.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PatchCommand(int id, JsonPatchDocument<CommandUpdateDTO> patchedCommand)
        {
            var commandFromRepo = _repo.GetCommandById(id);

            if( commandFromRepo == null )
            {
                return NotFound();
            }
            //           Please give us a CommandUpdateDTO from the given commandFromRepo
            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandFromRepo);
            patchedCommand.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandFromRepo);
            _repo.UpdateCommand(commandFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandFromRepo = _repo.GetCommandById(id);

            if( commandFromRepo == null )
            {
                return NotFound();
            }

            _repo.DeleteCommand(commandFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}