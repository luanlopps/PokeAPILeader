import React, { useState, useEffect } from 'react';
import axios from 'axios';
import TypeSelect from './components/TypeSelect';
import PokemonTable from './components/PokemonTable';
import PokemonModal from './components/PokemonModal';

const App = () => {
    const [selectedType, setSelectedType] = useState('');
    const [pokemons, setPokemons] = useState([]);
    const [selectedPokemon, setSelectedPokemon] = useState(null);
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
        if (selectedType === 'all') {
            axios.get('https://localhost:7091/api/pokemon/types')
                .then(response => {
                    const promises = response.data.map(type =>
                        axios.get(`https://localhost:7091/api/pokemon/pokemons?type=${type.name}`)
                    );
                    Promise.all(promises)
                        .then(results => {
                            const allPokemons = results.flatMap(result => result.data);
                            setPokemons(allPokemons);
                        });
                })
                .catch(error => console.error('Erro ao obter todos os Pokémon:', error));
        } else if (selectedType) {
            axios.get(`https://localhost:7091/api/pokemon/pokemons?type=${selectedType}`)
                .then(response => setPokemons(response.data))
                .catch(error => console.error('Erro ao obter Pokémons:', error));
        }
    }, [selectedType]);

    const handlePokemonClick = (pokemon) => {
        axios.get(`https://localhost:7091/api/pokemon/pokemon/${pokemon.name}`)
            .then(response => {
                setSelectedPokemon(response.data);
                setShowModal(true);
            })
            .catch(error => console.error('Erro ao obter detalhes do Pokémon:', error));
    };

    return (
        <div className="container mt-4">
            <h1 className="text-center">Pokédex</h1>
            <TypeSelect onTypeSelect={setSelectedType} />
            <PokemonTable pokemons={pokemons} onPokemonClick={handlePokemonClick} />
            <PokemonModal show={showModal} onClose={() => setShowModal(false)} pokemon={selectedPokemon} />
        </div>
    );
};

export default App;
