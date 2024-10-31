using System.Net.Http;
using System.Text.Json;
using PokeAPIBackend.Models;

namespace PokeAPIBackend.Services
{
    public class PokeApiService
    {
        private readonly HttpClient _httpClient;

        public PokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<List<TypeDTO>> GetTypesAsync()
        {
            var response = await _httpClient.GetAsync("type");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(content);

            return jsonDoc.RootElement
                .GetProperty("results")
                .EnumerateArray()
                .Select(type => new TypeDTO
                {
                    Name = type.GetProperty("name").GetString(),
                    Url = type.GetProperty("url").GetString()
                })
                .ToList();
        }

        public async Task<List<PokemonDTO>> GetPokemonsByTypeAsync(string typeName)
        {
            var response = await _httpClient.GetAsync($"type/{typeName}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(content);

            return jsonDoc.RootElement
                .GetProperty("pokemon")
                .EnumerateArray()
                .Select(p => new PokemonDTO
                {
                    Name = p.GetProperty("pokemon").GetProperty("name").GetString(),
                    Types = new List<string> { typeName }
                })
                .ToList();
        }

        public async Task<PokemonDetailsDTO> GetPokemonDetailsAsync(string pokemonName)
        {
            var response = await _httpClient.GetAsync($"pokemon/{pokemonName}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(content);

            var types = jsonDoc.RootElement
                .GetProperty("types")
                .EnumerateArray()
                .Select(type => type.GetProperty("type").GetProperty("name").GetString())
                .ToList();

            var stats = jsonDoc.RootElement
                .GetProperty("stats")
                .EnumerateArray()
                .ToDictionary(
                    stat => stat.GetProperty("stat").GetProperty("name").GetString(),
                    stat => stat.GetProperty("base_stat").GetInt32()
                );

            return new PokemonDetailsDTO
            {
                Name = pokemonName,
                Types = types,
                Stats = stats
            };
        }
    }
}
