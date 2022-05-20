import {useEffect , useState} from 'react'
import './App.css';
import Login from './components/Login/Login';
import Player from './components/Player/Player';
import Index from './components/Index/Index';
import {getTokenFromURL} from "./SpotifyLogic"

function App() {
    const [token, setToken] = useState()

    useEffect(() => {
        const hash = getTokenFromURL();
        window.location.hash = ""
        const _token = hash.access_token;
        setToken(_token)
        console.log("token => ", token);
    },[])
    return (
        <div>
            {
                token ? <Index /> : <Player />
            }
        </div>
    ); 
}
export default App;