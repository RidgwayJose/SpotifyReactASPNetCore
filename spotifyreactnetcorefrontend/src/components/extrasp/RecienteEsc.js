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
        const response = await fetch('home/index2')
        const data = await response.json()
        setUsers(data)
        console.log(fetch('home/index2'))
        console.log(data)
    }
    
    useEffect(() => {
        showData()
    }, [])

    

    return (
        <div>
        <table className='table table-striped table-hover mt-5 shadow-lg'>
            <thead>
                <tr className='bg-curso text-white'>
                    <th>NAME</th>
                    <th>ARTISTS</th>
                    <th>DATE</th>
                    <th>IMAGE</th>
                </tr>
            </thead>
            <tbody>
                {users.map((user) => (
                    <tr key={user.id}>
                        <td>{user.Name}</td>
                        <td>{user.Descrition}</td>
                        <td><img src={user.ImageUrl} /></td>
                    </tr>
                ))}
            </tbody>
        </table>
    </div>
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