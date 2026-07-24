import { useState, useEffect } from 'react';

function Catalogo() 
{
    const [dvds, setDvds] = useState([]); // la lista dei dvd (all'inizio vuota)
    const [caricamento, setCaricamento] = useState(true); // lo stato dell'operazione (true perché non ho acora finito il caricamento)

    useEffect(() => 
    {
        fetch('http://localhost:5114/api/Dvd')
        .then(res => res.json())
        .then(data => {
            setDvds(data); // aggiorno i dati
            setCaricamento(false); // ho finito di aspettare il caricamento
        });
    }, []);

    if (caricamento) // flag sullo stato del caricamento 
        return <p>Caricamento catalogo...</p>;

    return (
        <div style={{ textAlign: 'left' }}>
            <h2>Catalogo DVD</h2>
            <table>
                <thead>
                    <tr>
                        <th>Titolo</th>
                        <th>Categoria</th>
                        <th>Disponibili</th>
                    </tr>
                </thead>
                <tbody>
                    {dvds.map(dvd => (
                        <tr key={dvd.id}>
                        <td>{dvd.titolo}</td>
                        <td>{dvd.categoria}</td>
                        <td>{dvd.disponibili} / {dvd.quantitaTotale}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );

}

export default Catalogo;
