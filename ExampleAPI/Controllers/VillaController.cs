using ExampleAPI.Models.Dtos;
using ExampleAPI.Store;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [Route("api/villas")]
    [ApiController]
    public class VillaController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.GetVillas());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            VillaDto villa = VillaStore.GetVillas().FirstOrDefault(u => u.Id == id);

            if(villa != null)
                return Ok();

            return NotFound();
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            if (villaDto == null)
                return BadRequest();

            int lastId = VillaStore.villaList.OrderByDescending(v => v.Id).First().Id;

            villaDto.Id = lastId + 1;

            VillaStore.villaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDto> DeleteVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            VillaStore.villaList.Remove(villa);

            return Ok(villa);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<VillaDto> UpdateVilla(int id, [FromBody]VillaDto villaDto)
        {
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            villa.Name = villaDto.Name;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<VillaDto> UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            //https://jsonpatch.com/
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            patchDto.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}
