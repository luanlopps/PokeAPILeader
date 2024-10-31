import React, { useEffect, useState } from 'react';
import axios from 'axios';

const TypeSelect = ({ onTypeSelect }) => {
    const [types, setTypes] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:7091/api/pokemon/types')
            .then(response => {
                const filteredTypes = response.data.filter(
                    type => type.name !== 'unknown' && type.name !== 'stellar'
                );
                setTypes(filteredTypes);
            })
            .catch(error => console.error('Erro ao obter tipos:', error));
    }, []);

    const handleSelect = (e) => {
        onTypeSelect(e.target.value);
    };

    return (
        <div className="mb-3">
            <label htmlFor="typeSelect" className="form-label">Selecione um Tipo de Pokémon:</label>
            <select
                id="typeSelect"
                className="form-select"
                onChange={handleSelect}
                style={{ cursor: 'pointer' }}
                defaultValue="" 
            >
                <option value="" disabled hidden>Escolha um tipo...</option> {/* Padrão escondido após seleção */}
                <option value="all" style={{ fontWeight: 'bold' }}>Todos os tipos</option> {/* Em negrito */}
                {types.map(type => (
                    <option key={type.name} value={type.name}>{type.name}</option>
                ))}
            </select>
        </div>
    );
};

export default TypeSelect;
