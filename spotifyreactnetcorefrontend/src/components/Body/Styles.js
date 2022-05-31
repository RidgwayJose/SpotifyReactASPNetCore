import styled from "styled-components"

const BodyContainer = styled.div`
flex: 0.8;
background-color: #121212;
margin-top:5%;
margin-bottom:5%;
& img {
  border:1px solid black;
  border-radius:10px;
`

const HeaderContainer = styled.div`
display: flex;
height:7%;
width:80%;
top:0;
position:fixed;
z-index:1;
justify-content: space-between;
background-color: #0C0C0C;
margin-bottom:35px;

`
const HeaderLeft = styled.div`
display: flex;
width:35%;
align-items: center;
min-width: 75px;
background-color: #fff;
color: #181818;
padding-top: 2px;

& input{
border:none;
width: 93%;
}

`
const HeaderRight = styled.div`
display: flex;
align-items: center;
& h4 {
  margin-left: 15px;
}

`
const Tittle = styled.h4`
    color:white;
    margin-left:1%;

`


export {BodyContainer, HeaderContainer, HeaderLeft, HeaderRight,Tittle}