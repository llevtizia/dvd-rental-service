import { useState, useEffect } from 'react';

function Clienti() 
{

    const [clienti, setClienti] = useState([]);
    const [caricamento, setCaricamento] = useState(true);
    // stati del form
    const [nome, setNome] = useState('');
    const [cognome, setCognome] = useState('');
    const [email, setEmail] = useState('');

    function caricaClienti() 
    {
        fetch('http://localhost:5114/api/Clienti')
        .then(res => res.json())
        .then(data => {
            setClienti(data);
            setCaricamento(false);
        })
    }

    useEffect(() => { caricaClienti(); }, []);

    function handleSubmit(e) 
    {
        e.preventDefault(); // per bloccare il refresh della pagina

        fetch('http://localhost:5114/api/Clienti', 
            {
                method: 'POST',
                headers: { 'content-type': 'application/json'},
                body: JSON.stringify({ nome, cognome, email })
            }).then( res => res.json() )
                .then( () => {
                    setNome('');
                    setCognome('');
                    setEmail('');
                    caricaClienti(); // ricarico la lista aggiornata
                })

            
    }

    if (caricamento) // flag sullo stato del caricamento 
        return <p>Caricamento catalogo...</p>;

    return (
        <div style={{ textAlign: 'left' }}>
            <h2>Clienti</h2>

            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Nome"
                    value={nome}
                    onChange={e => setNome(e.target.value)}
                    required
                />
                <input
                    type="text"
                    placeholder="Cognome"
                    value={cognome}
                    onChange={e => setCognome(e.target.value)}
                    required
                />
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                    required
                />
                <button type="submit">Aggiungi cliente</button>
            </form>

            <table>
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Cognome</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {clienti.map(c => (
                        <tr key={c.id}>
                        <td>{c.nome}</td>
                        <td>{c.cognome}</td>
                        <td>{c.email}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Clienti