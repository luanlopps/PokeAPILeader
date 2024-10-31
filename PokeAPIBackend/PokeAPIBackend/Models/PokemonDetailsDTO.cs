namespace PokeAPIBackend.Models
{
    public class PokemonDetailsDTO
    {
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public Dictionary<string, int> Stats { get; set; }
    }
}
