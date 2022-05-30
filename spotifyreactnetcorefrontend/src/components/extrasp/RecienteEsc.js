import {useState, useEffect} from 'react'
import {Flex,DIV2} from "./styled"
import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';

const RecienteEsc = () => {
    
    const [users, setUsers] = useState([])
    

    const showData = async () => {
        const response = await fetch('home/index3')
        const data = await response.json()
        setUsers(data)
        console.log(fetch('home/index3'))
        console.log(data)
    }
    
    useEffect(() => {
        showData()
    }, [])

    

    return (
    <Flex>
        {users.map((user) => (
    
    <DIV2 key={user.id}>
            
                <CardActionArea>
                    <CardMedia
                    component="img"
                    image={user.ImageUrl}
                    alt="green iguana"
                    />
                    <CardContent>
                    <Typography gutterBottom variant="h6" component="div">
                        {user.Name}
                    </Typography>
                    <Typography variant="h6" color="#878787">
                        {user.Artists}
                    </Typography>
                    </CardContent>
                </CardActionArea>
            
    </DIV2>
    ))}
    </Flex>
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