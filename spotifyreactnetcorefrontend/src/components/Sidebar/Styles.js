import styled from "styled-components"
import SidebarChoice from './SidebarChoice';

const SidebarContainer = styled.div`
flex: 0.2;
height: 100vh;
background-color: #000;
min-width: 260px;
color: #fff;
top:0;
position:sticky;
& img{
  height: 70px;
  padding: 10px;
  margin-right: auto;
}
& hr{
  border-bottom: 1px solid smokegray;
  width: 90%;
  margin: 10px auto;
  }
`
const Playlist = styled.div`
margin: 5px 10px;

`
const Choices = styled.div`
display: flex;
align-items: center;
color: gray;
height: 40px;
cursor: pointer;
transition: 300ms color ease-in;
&:hover{
  color: #fff;
}
& h5{
  margin: 10px 0 0 20px;
}

`

export {SidebarContainer, Playlist, Choices}