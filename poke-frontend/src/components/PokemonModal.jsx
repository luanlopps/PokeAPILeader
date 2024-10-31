
import React from 'react';
import { Modal, Button } from 'react-bootstrap';

const PokemonModal = ({ show, onClose, pokemon }) => {
  if (!pokemon) return null;

  return (
    <Modal show={show} onHide={onClose}>
      <Modal.Header closeButton>
        <Modal.Title>{pokemon.name}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <p><strong>Tipos:</strong> {pokemon.types.join(', ')}</p>
        <h5>Status</h5>
        <ul>
          {Object.entries(pokemon.stats).map(([stat, value]) => (
            <li key={stat}><strong>{stat}:</strong> {value}</li>
          ))}
        </ul>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>
          Fechar
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default PokemonModal;
