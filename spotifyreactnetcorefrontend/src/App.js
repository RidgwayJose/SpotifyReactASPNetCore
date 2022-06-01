import {useEffect , useState} from 'react'
import './App.css';
import Login from './components/Login/Login';
import Player from './components/Player/Player';
import Index from './components/Index/Index';
import {getCodeFromURL} from "./SpotifyLogic"
import {getTokenFromURL} from "./SpotifyLogic"
import {Route, Routes, Navigate} from 'react-router-dom'
import RecienteEsc from "./components/extrasp/RecienteEsc"


const LoginRoute = () => {return(<Login/>)}
const AuthRoute = () => {
    const [tokens, setTokens] = useState([])
   
    const showData = async () => {
        const response = await fetch('home/getToken')
        const data = await response.json()
        setTokens(data)
    }
        useEffect(() => {
        showData()
    }, [])
        if(tokens.length != 0 ) {
            return(
                <div>
                        <Navigate to="/Player"/>
                </div>
            )
        } 
    }
const PlayerRoute = () => {return(<Player/>)}
const IndexRoute = () => {return(<Index/>)}

const RecienteRoute = () => {return(<RecienteEsc/>)}

function App() {
    return (
        <div>
            <Routes>
                <Route path = '/' element={<LoginRoute />} />
                <Route path = '/callback' element={<AuthRoute/>}/>
                <Route path = '/player' element={<PlayerRoute />}/>
                <Route path = '/index' element={<IndexRoute />} />
                <Route path = '/playlist' element={<RecienteRoute/>}/>
            </Routes>
        </div>
    ); 
}
export default App;