import HomeIcon from '@mui/icons-material/Home';
import SearchIcon from '@mui/icons-material/Search';
import LibraryMusic from '@mui/icons-material/LibraryMusic';
import { Playlist,SidebarContainer,DIV } from './Styles'
import SidebarChoice from './SidebarChoice'
import {useState, useEffect} from 'react'

const Sidebar = () => {
  //Usar playlist
  const [playlists, setPlaylists] = useState([])

    const showPlay = async () => {
        const response = await fetch('home/UserPlaylist')
        const data = await response.json()
        setPlaylists(data)
        console.log("jdjg",data)
    }
    
    useEffect(() => {
        showPlay()
    }, [])



  return (
    <SidebarContainer>
      
          <img src="https://1000logos.net/wp-content/uploads/2017/08/Spotify-symbol.jpg
  " alt="logo" />
        <SidebarChoice title="Home" Icon={HomeIcon}/>
        <SidebarChoice title="Search" Icon={SearchIcon}/>
        <SidebarChoice title="Your Library" Icon={LibraryMusic}/>
        <Playlist>PlayLists</Playlist>
        <hr />
        {playlists.map((title) => (
          <DIV><a href="/">{title.Name}</a></DIV>
        ))}
      
    </SidebarContainer>
      
  )
}

export default Sidebar