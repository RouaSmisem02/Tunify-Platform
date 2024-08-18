// File: ArtistsController.cs
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Repositories.Interfaces; // Make sure this is included
using Tunify_Platform.Models;

namespace Tunify_Platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtists _artistRepository;

        public ArtistsController(IArtists artistRepository)
        {
            _artistRepository = artistRepository;
        }

        // Controller actions
    }
}
