import {useState, useEffect} from 'react'
import {Flex,DIV,TABLE,Sep} from "./styled"
import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';

const RecienteEsc = () => {
    
    
    const [recs, setRecs] = useState([])
    const showRec = async () => {
        const response = await fetch('home/PlaylistTracks/5IpiouPQ6p0odjK4JUG6X1')
        const data = await response.json()
        console.log("uwyur",data)
        setRecs(data)
    }
    
    useEffect(() => {
        showRec()
    }, [])

    //funcion para traer los datos de la Api

    //Usar playlist
  const [playlists, setPlaylists] = useState([])

  const showPlay = async () => {
      const response = await fetch('home/UserPlaylist')
      const data = await response.json()
      setPlaylists(data)
      console.log("jdjg",data)
  }
  
  useEffect(() => {
      showPlay()
  }, [])



  
    

    return (
        
    <DIV>
        {playlists.map((title) => (
        <Sep>
            <img src={title.ImageUrl} align="left"></img>
            <h1>{title.Name}</h1>
        </Sep>
        ))}
        
        <TABLE>
        <table  class='table'>
            <thead>
                <tr className='bg-curso text-white'>
                    <th>NAME</th>
                    <th>ARTISTS</th>
                    <th>IMAGE</th>
                </tr>
            </thead>
            <tbody>
            {recs.map((user) => (
                <tr key={user.id}>
                    <td>{user.NameTrack}</td>
                    <td>{user.ArtistTrack}</td>
                    <td><img src={user.ImageUrl} /></td>
                </tr>
                 ))}
            </tbody>
        </table>
        </TABLE>
    </DIV>
    
    )
   
}

/*
    */
   /*{users.map((user) => (
            
        <Card sx={{ maxWidth: 345 }}>
        <CardActionArea>
            <CardMedia
            component="img"
            height="140"
            image={user.ImageUrl}
            alt="green iguana"
            />
            <CardContent>
            <Typography gutterBottom variant="h5" component="div">
                {user.Name}
            </Typography>
            <Typography variant="body2" color="text.secondary">
                {user.Artists}
            </Typography>
            </CardContent>
        </CardActionArea>
        </Card>))}*/

export default RecienteEsc