import { useState } from 'react';
import Catalogo from './components/Catalogo';
import Clienti from './components/Clienti';
import Noleggio from './components/Noleggio';
import Storico from './components/Storico';

function App() {
  const [pagina, setPagina] = useState('home'); // pagina = stato della sessione attiva

  return (
    <div style={{ padding: '30px' }}>
      <h1>Gestione noleggio DVD</h1>
      
      <nav style={{ display: 'flex', gap: '20px', marginBottom: '42px' }}>
        <button className="btn-home" onClick={() => setPagina('home')}>Home</button>
        <button className="btn-catalogo" onClick={() => setPagina('catalogo')}>Vedi il catalogo</button>
        <button className="btn-clienti" onClick={() => setPagina('clienti')}>Vedi i clienti</button>
        <button className="btn-noleggio" onClick={() => setPagina('noleggio')}>Nuovo noleggio</button>
        <button className="btn-storico" onClick={() => setPagina('storico')}>Storico noleggi</button>
      </nav>

      {pagina === 'home' && <p>Home page! Scegli una sezione dal menu sopra.</p>}
      {pagina === 'catalogo' && <Catalogo />}
      {pagina === 'clienti' && <Clienti />}
      {pagina === 'noleggio' && <Noleggio />}
      {pagina === 'storico' && <Storico />}
    </div>
  );
}

export default App;