import styled from "styled-components"

const Flex = styled.div`
display: flex;
flex-direction: row;
flex-wrap: wrap;
justify-content:space-around;
background-color:#121212;
text-align:center;
& img{
    border-radius:100%;
    width:80%;
    margin:auto;
    margin-top:10px;
}
`
const DIV2= styled.div`

display: flex;
width:23%;
border:1px solid black;
margin-top:10px;
background-color:#181818;
color: white;
`

export {Flex,DIV2}