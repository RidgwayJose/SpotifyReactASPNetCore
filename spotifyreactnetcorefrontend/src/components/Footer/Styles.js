import styled from "styled-components";

const FooterContainer = styled.div`
position: fixed;
bottom: 0;
height: 70px;
width: 100%;
background-color: #181818;
color: #fff;
display: flex;
justify-content: space-between;


`
const FooterLeft = styled.div`
flex: 0.3;
display: flex;
align-items: center;
max-width: 300px;
& img {
  height: 60px;
  width: 60px;
  margin-right: 20px;
  object-fit: contain;
}


`

const FooterCenter = styled.div`
flex: 0.4;
display: flex;
align-items: center;
justify-content: space-between;
max-width: 300px;


`

const FooterRight = styled.div`
flex: 0.3;
display: flex;
align-items: center;
justify-content: center;
margin-right: 35px;
& .MuiSlider-root {
  color: #00ff00;
}


`

export {FooterContainer, FooterLeft, FooterRight, FooterCenter}