using Microsoft.AspNetCore.Mvc;
using PokeAPIBackend.Models;
using PokeAPIBackend.Services;

namespace PokeAPIBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokeApiService _pokeApiService;

        public PokemonController(PokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<TypeDTO>>> GetTypes()
        {
            var types = await _pokeApiService.GetTypesAsync();
            return Ok(types);
        }

        [HttpGet("pokemons")]
        public async Task<ActionResult<List<PokemonDTO>>> GetPokemonsByType([FromQuery] string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Type parameter is required.");

            var pokemons = await _pokeApiService.GetPokemonsByTypeAsync(type);
            return Ok(pokemons);
        }

        [HttpGet("pokemon/{name}")]
        public async Task<ActionResult<PokemonDetailsDTO>> GetPokemonDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Pokemon name is required.");

            var pokemonDetails = await _pokeApiService.GetPokemonDetailsAsync(name);
            return Ok(pokemonDetails);
        }
    }
}
