import { HeaderContainer, HeaderLeft, HeaderRight } from "./Styles"
import SearchIcon from "@mui/icons-material/Search"
import { Avatar } from "@mui/material"
import {useState, useEffect} from 'react'

const Header = () => {
  const [users, setUsers] = useState([])
    //const [search, setSearch] = useState("")

    //funcion para traer los datos de la Api

    const showData = async () => {
        const response = await fetch('home/GetUserID')
        const data = await response.json()
        setUsers(data)
        console.log("Header fetch success =>",fetch('home/GetUserID'))
        console.log("Header data =>",data)
    }
    useEffect(() => {
      showData()
  }, [])

  return (
    <HeaderContainer>
        <HeaderLeft>
          <SearchIcon/>
          <input type="text" placeholder="Search for Artists, Songs or Album" />
        </HeaderLeft>
        <HeaderRight>
        <Avatar/>
          <h4>{users}</h4>  
          </HeaderRight>
    </HeaderContainer>
  )
}

export default Header