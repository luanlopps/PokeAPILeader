import React from 'react';
import DataTable from 'react-data-table-component';

const PokemonTable = ({ pokemons, onPokemonClick }) => {
    const columns = [
        {
            name: 'Nome',
            selector: row => row.name,
            sortable: true,
        },
        {
            name: 'Tipo',
            selector: row => row.types.join(', '),
            sortable: true,
        }
    ];

    return (
        <DataTable
            title="Pokémons"
            columns={columns}
            data={pokemons}
            pagination
            highlightOnHover
            onRowClicked={onPokemonClick} // Define ação ao clicar na linha
            customStyles={{
                rows: {
                    style: {
                        cursor: 'pointer', // Aplica o cursor de "mãozinha" em todas as linhas
                    },
                },
            }}
        />
    );
};

export default PokemonTable;
