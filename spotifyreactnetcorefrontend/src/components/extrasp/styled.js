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
const DIV = styled.div`
background-color: #121212;
text-aling:center;
`


const TABLE = styled.div`
text-aling:center;
background-color: ;
& img {
    width:100px;
    
}
& td {
    border:1px solid #121212;
    border-bottom:1px solid #2D2D2D;
    color:white;
    
}
`
const Sep = styled.div`
background-color: #0E0E0E;
padding:2%;
height:350px;
color: white;
& h1 {
    margin-left: 5%;
    padding:7%;
}
& img{
    width:20%;
}
`

export {Flex,DIV2,DIV,TABLE,Sep}