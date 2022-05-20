import { HeaderContainer, HeaderLeft, HeaderRight } from "./Styles"
import SearchIcon from "@mui/icons-material/Search"
import { Avatar } from "@mui/material"

const Header = () => {
  return (
    <HeaderContainer>
        <HeaderLeft>
          <SearchIcon/>
          <input type="text" placeholder="Search for Artists, Songs or Album" />
        </HeaderLeft>
        <HeaderRight>
        <Avatar/>
          <h4>Usuario desde Api</h4>  
          </HeaderRight>
    </HeaderContainer>
  )
}

export default Header