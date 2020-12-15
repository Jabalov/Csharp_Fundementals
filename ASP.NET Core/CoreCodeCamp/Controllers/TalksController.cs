using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [ApiController]
    [Route("api/camps/{moniker}/talks")]
    public class TalksController : ControllerBase
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public TalksController(ICampRepository campRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker)
        {
            try
            {
                var talks = await campRepository.GetTalksByMonikerAsync(moniker, true);

                return mapper.Map<TalkModel[]>(talks);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id)
        {
            try
            {
                var talk = await campRepository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null)
                    return NotFound("this talk is not found");

                return mapper.Map<TalkModel>(talk);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {
            try
            {
                var camp = await campRepository.GetCampAsync(moniker);
                if (camp == null)
                    return BadRequest("Camp Doesn't exist");

                var talk = mapper.Map<Talk>(model);
                talk.Camp = camp;

                if (model.Speaker == null)
                    return BadRequest("Speaker Id is required");
                var speaker = await campRepository.GetSpeakerAsync(model.Speaker.SpeakerId);
                if (speaker == null)
                    return BadRequest("Speaker Couldn't be found");

                talk.Speaker = speaker;

                campRepository.Add(talk);

                if (await campRepository.SaveChangesAsync())
                {
                    var url = linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { moniker, id = talk.TalkId });

                    return Created(url, mapper.Map<TalkModel>(talk));
                }
                else
                {
                    return BadRequest("Failed to save a new talk");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
        {
            try
            {
                var talk = await campRepository.GetTalkByMonikerAsync(moniker, id, true);
                if (talk == null)
                    return NotFound("Couldn't find the specified talk");

                mapper.Map(model, talk);
                if (model.Speaker != null)
                {
                    var speaker = await campRepository.GetSpeakerAsync(model.Speaker.SpeakerId);
                    if (speaker != null)
                        talk.Speaker = speaker;
                }

                if (await campRepository.SaveChangesAsync())
                {
                    return mapper.Map<TalkModel>(talk);
                }
                else
                {
                    return BadRequest("Failed to update the database");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string moniker, int id)
        {
            try
            {
                var talk = await campRepository.GetTalkByMonikerAsync(moniker, id);
                if (talk == null)
                    return NotFound();
                campRepository.Delete(talk);

                if (await campRepository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to Delete The talk");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }
    }
}

