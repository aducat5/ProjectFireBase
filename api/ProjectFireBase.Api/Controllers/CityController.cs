using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFireBase.Model;

namespace ProjectFireBase.Api.Controllers
{
    [Route("city")]
    public class CityController : ControllerBase
    {
        //This is the basic controller protected by authorization.
        //This controller uses base service injection which also uses dapper context to connect to database.
        private readonly ILogger<CityController> _logger;

        public CityController(ILogger<CityController> logger)
        {
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(string name, string state)
        {
            var firestore = new FirestoreDbBuilder
            {
                ProjectId = "fir-denemesi-7ffbe",
                EmulatorDetection = Google.Api.Gax.EmulatorDetection.EmulatorOrProduction
            }.Build();

            if (name is null || state is null) return BadRequest();

            var collection = firestore.Collection("cities");
            await collection.Document(Guid.NewGuid().ToString("N")).SetAsync(new City(name, state));
            return Ok();
        }
    }
}