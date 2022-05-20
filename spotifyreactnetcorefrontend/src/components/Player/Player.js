import Body from '../Body/Body'
import Footer from '../Footer/Footer'
import Sidebar  from '../Sidebar/Sidebar'
import {SpotifyBody}  from './Styles'

const Player = () => {
    return (
        <>
        <SpotifyBody>
            <Sidebar/>
            <Body/>
        </SpotifyBody>
            <Footer/>
        </>
            )
}
export default Player