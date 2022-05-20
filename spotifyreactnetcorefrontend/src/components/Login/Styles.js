import styled from "styled-components"

const LoginContainer = styled.div`
display: grid;
background-color: #000;
place-items: center;
height: 100vh;
& img {
    width: 100%;
 }
`

const LoginButton = styled.a`
padding: 20px;
background-color: #1db954;
border-radius: 90px;
color: #fff;
text-decoration: none;
text-transform: uppercase;
font-weight: bolder


` 

export {LoginContainer, LoginButton }