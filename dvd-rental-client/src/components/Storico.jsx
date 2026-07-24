import { useState, useEffect } from 'react';

function Storico() 
{
    const [noleggi, setNoleggi] = useState([]);
    const [clienti, setClienti] = useState([]);

    const [caricamento, setCaricamento] = useState(true);

    const [filtroCliente, setFiltroCliente] = useState('');
    const [filtroTitolo, setFiltroTitolo] = useState('');

    
    function caricaStorico() 
    {
        const params = new URLSearchParams();
        if ( filtroCliente ) 
            params.append('clienteId', filtroCliente);
        if ( filtroTitolo ) 
            params.append('titolo', filtroTitolo);

        fetch(`http://localhost:5114/api/Noleggi?${params}`)
            .then(res => res.json())
            .then(data => {
                setNoleggi(data);
                setCaricamento(false);
            });
    }

    function caricaClienti() 
    {
        fetch('http://localhost:5114/api/Clienti').then(res => res.json()).then(setClienti);
    }

    useEffect(() => { caricaStorico(); caricaClienti(); }, []);


    function handleRestituisci(id) 
    {
        fetch(`http://localhost:5114/api/Noleggi/${id}/restituzione`, { method: 'PUT' })
        .then(res => {
            if (res.ok) 
                caricaStorico(); // ricarico la lista con lo stato aggiornato
        });
    }

    function handleFiltra(e) 
    {
        e.preventDefault();

        caricaStorico();
    }


    if ( caricamento ) 
        return <p>Caricamento storico...</p>;

    return (
        <div style={{ textAlign: 'left' }}>
            <h2>Storico noleggi</h2>

            <form onSubmit={handleFiltra} style={{ flexDirection: 'row', maxWidth: 'none' }}>
                <select value={filtroCliente} onChange={e => setFiltroCliente(e.target.value)}>
                    <option value="">Tutti i clienti</option>
                        { clienti.map( c => (<option key={c.id} value={c.id}>{c.nome} {c.cognome}</option>) ) }
                </select>

                <input
                    type="text"
                    placeholder="Filtra per titolo"
                    value={filtroTitolo}
                    onChange={e => setFiltroTitolo(e.target.value)}
                    />

                <button type="submit">Filtra</button>
            </form>

            <table>
                <thead>
                    <tr>
                        <th>Cliente</th>
                        <th>DVD</th>
                        <th>Data noleggio</th>
                        <th>Restituzione prevista</th>
                        <th>Stato</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>
                    {noleggi.map(n => (
                        <tr key={n.id}>
                            <td>{n.cliente}</td>
                            <td>{n.dvd}</td>
                            <td>{new Date(n.dataNoleggio).toLocaleDateString()}</td>
                            <td>{new Date(n.restituzionePrevista).toLocaleDateString()}</td>
                            <td>{n.restituzioneEffettiva ? 'Restituito' : 'In corso'}</td>
                            <td>
                                { !n.restituzioneEffettiva && (
                                <   button onClick={() => handleRestituisci(n.id)}>Segna come restituito</button>
                                ) }
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Storico;