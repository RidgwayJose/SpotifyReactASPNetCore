import Header from './Header'
import { BodyContainer } from './Styles'
import {useState, useEffect} from 'react'
import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';

const Body = () => {
  const [users, setUsers] = useState([])
    //const [search, setSearch] = useState("")

    //funcion para traer los datos de la Api

    const showData = async () => {
        const response = await fetch('home/index3')
        const data = await response.json()
        setUsers(data)
        console.log(fetch('home/index3'))
        console.log(data)
    }
    //showData() => {Bucle de datos}
    
    //Metodo de filtrado

    //funcion de busqueda
    useEffect(() => {
        showData()
    }, [])

    //renderizamos la vista

  return (
    <BodyContainer>
        <Header/>
        <div>
            <table className='table table-striped table-hover mt-5 shadow-lg'>
                <thead>
                    <tr className='bg-curso text-white'>
                        <th>Nombre</th>
                        <th>Artista</th>
                        <th>Album</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((user) => (
                        <tr key={user.id}>
                            <td>{user.Name}</td>
                            <td>{user.Artists}</td>
                            <td>{user.Album}</td>
                            <td><img src={user.ImageUrl} /></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    </BodyContainer>
  )
}

export default Body