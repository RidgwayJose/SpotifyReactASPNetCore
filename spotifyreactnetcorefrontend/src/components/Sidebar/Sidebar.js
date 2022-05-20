import HomeIcon from '@mui/icons-material/Home';
import SearchIcon from '@mui/icons-material/Search';
import LibraryMusic from '@mui/icons-material/LibraryMusic';
import { Playlist,SidebarContainer } from './Styles'
import SidebarChoice from './SidebarChoice'

const Sidebar = () => {
  return (
    <SidebarContainer>
      <img src="https://1000logos.net/wp-content/uploads/2017/08/Spotify-symbol.jpg
" alt="logo" />
      <SidebarChoice title="Home" Icon={HomeIcon}/>
      <SidebarChoice title="Search" Icon={SearchIcon}/>
      <SidebarChoice title="Your Library" Icon={LibraryMusic}/>
      <Playlist>PLAYLIST</Playlist>
      <hr />
      <SidebarChoice title="2021 Chillout Music"/>
      <SidebarChoice title="Dark Metal"/>

    </SidebarContainer>
      
  )
}

export default Sidebar