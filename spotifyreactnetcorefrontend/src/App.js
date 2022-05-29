import {useEffect , useState} from 'react'
import './App.css';
import Login from './components/Login/Login';
import Player from './components/Player/Player';
import Index from './components/Index/Index';
import {getCodeFromURL} from "./SpotifyLogic"
import {getTokenFromURL} from "./SpotifyLogic"
import {renderMatches, Route, Routes} from 'react-router-dom'


const LoginRoute = () => {return(<Login/>)}
const PlayerRoute = () => {
    const [tokens, setTokens] = useState([])
   
    const showData = async () => {
        const response = await fetch('home/gettoken')
        const data = await response.json()
        setTokens(data)
    }
        useEffect(() => {
        showData()
    }, [])
        if(tokens.length != 0 ) {
            return(
                <div>
                        <Player/>       
                </div>
            )
        } 
    }
const IndexRoute = () => {return(<Index/>)}
function App() {
    return (
        <div>
            <Routes>
                <Route path = '/' element={<LoginRoute />} />
                <Route path = '/callback' element={<PlayerRoute/>}/>
                <Route path = '/index' element={<IndexRoute />} />
            </Routes>
        </div>
    ); 
}
export default App;