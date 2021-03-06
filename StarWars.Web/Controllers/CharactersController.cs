﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using StarWars.Application.Contract;
using StarWars.Core.Exceptions;
using StarWars.Web.Contract;

namespace StarWars.Web.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterApplicationService _characterService;

        public CharactersController(ILogger<CharactersController> logger, ICharacterApplicationService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> Get(uint page, uint pageSize)
        {
            return Ok(await _characterService.GetAsync(page, pageSize).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("{characterName}")]
        public async Task<ActionResult<CharacterDTO>>GetByName(string characterName)
        {
            var result = await _characterService.GetByNameAsync(characterName).ConfigureAwait(false);
            if (null == result)
                return NotFound(new { error = $"Character with name: {characterName} was not found." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CharacterDTO character)
        {
            CharacterDTO result;
            try
            {
                result = await _characterService.CreateAsync(character).ConfigureAwait(false);
            }
            catch(BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            var path = $"{Request.Scheme}://{Request.Host}{Request.Path}/{result.Name}";
            return Created(path, result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]CharacterDTO character)
        {
            CharacterDTO result;
            try
            {
                result = await _characterService.UpdateAsync(character);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{characterName}")]
        public async Task<ActionResult<CharacterDTO>> Delete(string characterName)
        {
            CharacterDTO result;
            try
            {
                result = await _characterService.DeleteByNameAsync(characterName);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            if(result == null)
                return NotFound(new { error = $"Character with name: {characterName} was not found." });

            return Ok(result);
        }   
    }
}
