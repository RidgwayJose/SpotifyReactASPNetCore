import styled from "styled-components"

const Flex = styled.div`
display: flex;
flex-direction: row;
flex-wrap: wrap;
justify-content:space-around;
background-color:#121212;
text-align:center;
margin-bottom:2%;
& img{
    width:80%;
    margin:auto;
    margin-top:20px;
}
`
const DIV2= styled.div`

display: flex;
width:18%;
border:1px solid black;
margin-top:10px;
margin-bottom:10px;
background-color:#181818;
color: white;
& div{
    max-height:150px;
}
`

export {Flex,DIV2}