import { useState, useEffect } from 'react';

function Noleggio() 
{
    const [clienti, setClienti] = useState([]); // devo caricare sia i clienti che i dvd
    const [dvds, setDvds] = useState([]);

    const [clienteId, setClienteId] = useState('');
    const [dvdId, setDvdId] = useState('');
    const [restituzionePrevista, setRestituzionePrevista] = useState('');
    const [messaggio, setMessaggio] = useState(null);
    const [errore, setErrore] = useState(null);


    function caricaDati() 
    {
        fetch('http://localhost:5114/api/Clienti').then(res => res.json()).then(setClienti);

        fetch('http://localhost:5114/api/Dvd').then(res => res.json()).then(setDvds);
    }


    useEffect(() => { caricaDati(); }, []);



    function handleSubmit(e) 
    {
        e.preventDefault();

        setMessaggio(null);
        setErrore(null);

        fetch('http://localhost:5114/api/Noleggi', 
            {
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({ 
                    clienteId: Number(clienteId),
                    dvdId: Number(dvdId),
                    restituzionePrevista })
            }).then(async res => {
                if (!res.ok) {
                    const testoErrore = await res.text();
                    setErrore(testoErrore);
                    return;
                }

        setMessaggio('Noleggio creato con successo!');

        setClienteId('');
        setDvdId('');
        setRestituzionePrevista('');
        caricaDati(); // ricarico il catalogo con disponibilità aggiornata
        });
  }

  return (
    <div style={{ textAlign: 'left' }}>
        <h2>Nuovo noleggio</h2>

        <form onSubmit={handleSubmit}>
            <select value = { clienteId } onChange={e => setClienteId(e.target.value)} required>
                <option value="">Seleziona cliente</option>
                    { clienti.map( c => ( <option key={ c.id } value={ c.id }> { c.nome } { c.cognome }</option> ) ) }
            </select>

            <select value = { dvdId } onChange={e => setDvdId(e.target.value)} required>
                <option value="">Seleziona DVD</option>
                    { dvds.map ( d => ( <option key={ d.id } value={ d.id } disabled={ d.disponibili === 0 }>
                        {d.titolo} ( { d.disponibili }/{ d.quantitaTotale } disponibili ) </option>))}
            </select>

            <input
                type="date"
                value={restituzionePrevista}
                onChange={e => setRestituzionePrevista(e.target.value)}
                required
            />

            <button type="submit">Crea noleggio</button>
        </form>

      {messaggio && <p style={{ color: 'green' }}>{messaggio}</p>}
      {errore && <p style={{ color: 'red' }}>{errore}</p>}
    </div>
  );
}

export default Noleggio;