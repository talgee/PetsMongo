using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsMongo.JWTWebAuthentication.Repository;
using PetsMongo.Models;
using PetsMongo.Services;
using System.Collections.Generic;

namespace PetsMongo.Controllers
{
    [Authorize]
    [Controller]
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private readonly MongoDBService _mongoDBService;
        private readonly IJWTManagerRepository _jWTManager;

        public PetsController(MongoDBService mongoDBService, IJWTManagerRepository jWTManager)
        {
            _mongoDBService = mongoDBService;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        [Route("GetPets")]
        public async Task<IActionResult> GetPets(int page = 0, int limit = 10) 
        {
            List<Pet> listOfPets = await _mongoDBService.GetAsync(page, limit);

            var countItems = listOfPets.Count;

            return Ok(new { results = listOfPets, countItems });
        }

        [HttpPost]
        [Route("CreateNewPet")]
        public async Task<IActionResult> Post([FromBody] Pet pet) 
        {
            await _mongoDBService.CreateAsync(pet);

            return CreatedAtAction(nameof(GetPets), new { id = pet.Id }, pet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) 
        {
            var pet = await _mongoDBService.DeleteAsync(id);

            return CreatedAtAction(nameof(Delete), pet); //NoContent();
        }

        [HttpGet]
        [Route("GetSumAges")]
        public async Task<int> GetSumAges()
        {
            var sum = await _mongoDBService.SumOfAgesAsync();
            return sum;
        }
    }
}
