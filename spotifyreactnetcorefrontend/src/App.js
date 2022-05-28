import {useEffect , useState} from 'react'
import './App.css';
import Login from './components/Login/Login';
import Player from './components/Player/Player';
import Index from './components/Index/Index';
import {getCodeFromURL} from "./SpotifyLogic"
import {getTokenFromURL} from "./SpotifyLogic"
import Login2 from "./components/Login/Login2"

function App() {
    const [code, setCode] = useState()
    const [token, setToken] = useState()

    useEffect(() => {
        console.log("getTokenFromURL =>", getCodeFromURL().code)
        const search = getCodeFromURL();
        console.log("getCodeFromURL =>", getCodeFromURL().code)
        console.log("hash =>", search)
        const _code = search.code;
        setCode(_code)
    },[])

    return (
        <div>
            {
                code ? <Login2/> : <Login />
            }
        </div>
    ); 
}
export default App;