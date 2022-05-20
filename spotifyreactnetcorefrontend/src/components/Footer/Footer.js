import React from 'react'
import { FooterContainer, FooterLeft, FooterRight, FooterCenter } from './Styles'
import ShuffleIcon from '@mui/icons-material/Shuffle'
import SkipPreviousIcon from '@mui/icons-material/SkipPrevious'
import PlayCircleOutlineIcon from '@mui/icons-material/PlayCircleOutline'
import SkipNextIcon from '@mui/icons-material/SkipNext'
import RepeatIcon from '@mui/icons-material/Repeat'
import PlaylistPlay from '@mui/icons-material/PlaylistPlay'
import VolumeDownIcon from '@mui/icons-material/VolumeDown'
import { Grid, Slider } from '@mui/material'


const Footer = () => {
  return (
    <FooterContainer>
        <FooterLeft>
            <img src="https://m.media-amazon.com/images/I/81lrF8AR+tL._SL1500_.jpg" alt="cover" />
            <div>
              <h4>Psychosocial</h4>
              <p>Slipknot</p>
            </div>
        </FooterLeft>
        <FooterCenter>
            <ShuffleIcon/>
            <SkipPreviousIcon/>
            <PlayCircleOutlineIcon />
            <SkipNextIcon/>
            <RepeatIcon />
        </FooterCenter>
        <FooterRight>
            <Grid container spacing={2}>
                <Grid item><PlaylistPlay/></Grid>
                <Grid item><VolumeDownIcon/></Grid>
                <Grid item xs><Slider/></Grid>
            </Grid>
        </FooterRight>
    </FooterContainer>
  )
}

export default Footer