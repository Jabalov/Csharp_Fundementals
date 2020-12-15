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
    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public CampsController(ICampRepository campRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await campRepository.GetAllCampsAsync(includeTalks);
                return mapper.Map<CampModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var results = await campRepository.GetCampAsync(moniker);

                return results == null ? NotFound() : mapper.Map<CampModel>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<CampModel[]>> SearchByDate(DateTime theDate, bool includeTalks = false)
        {
            try
            {
                var results = await campRepository.GetAllCampsByEventDate(theDate, includeTalks);

                return !results.Any() ? NotFound() : mapper.Map<CampModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CampModel>> Post(CampModel model)
        {
            try
            {
                var campExist = await campRepository.GetCampAsync(model.Moniker);
                if(campExist != null)
                {
                    return BadRequest("Moniker is in use");
                }

                var location = linkGenerator.GetPathByAction("Get", "Camps", new { moniker = model.Moniker });

                if(string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Couldn't use current moniker");
                }

                var camp = mapper.Map<Camp>(model);
                campRepository.Add(camp);

                if(await campRepository.SaveChangesAsync())
                {
                    return Created($"/api/camps/{camp.Moniker}", mapper.Map<CampModel>(camp));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
            return BadRequest();
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, CampModel model)
        {
            try
            {
                var oldCamp = await campRepository.GetCampAsync(moniker);
                if (oldCamp == null)
                    return NotFound($"Couldn't find a camp with moniker: {moniker}");

                 mapper.Map(model, oldCamp);
                
                if (await campRepository.SaveChangesAsync())
                {
                    return mapper.Map<CampModel>(oldCamp);
                }
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
            return BadRequest();
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var oldCamp = await campRepository.GetCampAsync(moniker);
                if (oldCamp == null) 
                    return NotFound();

                campRepository.Delete(oldCamp);
                if(await campRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure!");
            }
            return BadRequest("Failed to delete corresponding camp");
        }
    }
}
