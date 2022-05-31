import Header from './Header'
import { BodyContainer,Tittle } from './Styles'
import {useState, useEffect} from 'react'
import * as React from 'react';
import {Flex,DIV2} from "../extrasp/styled"
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';
import { useTabPanel } from '@mui/base';
//canciones recientemente escuchadas
const Body = () => {
  const [rect, setRect] = useState([])
    //const [search, setSearch] = useState("")

    //funcion para traer los datos de la Api
    const showData = async () => {
        const response = await fetch('home/RecentlyPlayedTracks')
        const data = await response.json()
        const data2= data.slice(0,5)
        setRect(data2)
        
        console.log("holu",data2)
        console.log(fetch('home/RecentlyPlayedTracks'))
        console.log("1",data)
    }
    
    useEffect(() => {
        showData()
    }, [])

//Ultimos Lanzamientos
const [ults, setUlts] = useState([])

    const showUlt = async () => {
        const response = await fetch('home/NewReleases')
        const data = await response.json()
        setUlts(data)
        console.log(fetch('home/NewReleases'))
        console.log("p",data)
    }
    
    useEffect(() => {
        showUlt()
    }, [])

//Artistas que sigues
const [FollowArts, setFollowArts] = useState([])

    const showFollow = async () => {
        const response = await fetch('home/FollowedArtists')
        const data = await response.json()
        setFollowArts(data)
        console.log("p",data)
    }
    
    useEffect(() => {
        showFollow()
    }, [])



    
  return (
    <BodyContainer>
        <Header/>
        <Tittle>Escuchado Recientemente</Tittle>
        <Flex>
        {rect.map((track) => (
    
            <DIV2 key={track.id}>
            
                <CardActionArea>
                    <CardMedia
                    component="img"
                    image={track.ImageUrl}
                    />
                    <CardContent>
                    <div>
                        <Typography gutterBottom variant="h10" component="div" scrollamount="0" text-aling="center">{track.Name.toString().substring(0,20)}...</Typography>
                        <Typography variant="h8" color="#878787">{track.Artists.toString().substring(0,20)}...</Typography>
                    </div>
                    </CardContent>
                </CardActionArea>
            
            </DIV2>
            ))}
        </Flex>
        
        <Tittle>Ultimos Lanzamientos</Tittle>
        <Flex>
        {ults.map((ult) => (
            <DIV2 key={ult.id}>
                <CardActionArea>
                    <CardMedia
                        component="img"
                        image={ult.ImageUrl}
                        />
                    <CardContent>
                        <div>
                            <Typography gutterBottom variant="h10" component="div" scrollamount="0" text-aling="center">{ult.Name.toString().substring(0,20)}...</Typography>
                            <Typography variant="h8" color="#878787">{ult.Artists.toString().substring(0,20)}</Typography>
                        </div>
                    </CardContent>
                </CardActionArea>
            </DIV2>
            ))}
        </Flex>

        <Tittle>Artistas Que Sigues</Tittle>
            <Flex>
            {FollowArts.map((Follow) => (
                <DIV2 key={Follow.id}>
                    <CardActionArea>
                        <CardMedia
                            component="img"
                            image={Follow.ImageUrl}
                            />
                        <CardContent>
                            <div>
                                <Typography gutterBottom variant="h10" component="div" scrollamount="0" text-aling="center">{Follow.Name}</Typography>
                                <Typography variant="h8" color="#878787">{Follow.Followers}</Typography>
                            </div>
                        </CardContent>
                    </CardActionArea>
                </DIV2>
                ))}
            </Flex>
    </BodyContainer>
  )
}

export default Body