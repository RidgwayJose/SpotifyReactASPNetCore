import React from 'react'
import { FooterContainer, FooterLeft, FooterRight, FooterCenter,DIV10} from './Styles'
import ShuffleIcon from '@mui/icons-material/Shuffle'
import SkipPreviousIcon from '@mui/icons-material/SkipPrevious'
import PlayCircleOutlineIcon from '@mui/icons-material/PlayCircleOutline'
import SkipNextIcon from '@mui/icons-material/SkipNext'
import RepeatIcon from '@mui/icons-material/Repeat'
import PlaylistPlay from '@mui/icons-material/PlaylistPlay'
import VolumeDownIcon from '@mui/icons-material/VolumeDown'
import { Grid, Slider } from '@mui/material'
import {useState, useEffect} from 'react'
import Typography from '@mui/material/Typography';


const Footer = () => {
  const [tracks, setTracks] = useState([])
    //const [search, setSearch] = useState("")

    //funcion para traer los datos de la Api

    const showData2 = async () => {
        const response = await fetch('home/CurrentlyPlayingTrack')
        const data = await response.json()
        setTracks(data)
    }
    //showData() => {Bucle de datos}

    //Metodo de filtrado

    //funcion de busqueda
    useEffect(() => {
        showData2()
    }, [])

  return (
    <FooterContainer>
        <FooterLeft>
            <img src={tracks.ImageUrl} alt="cover" />
            <div>
              <Typography gutterBottom variant="h7" component="marquee">{tracks.Name}</Typography>
              {tracks.Artists}
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