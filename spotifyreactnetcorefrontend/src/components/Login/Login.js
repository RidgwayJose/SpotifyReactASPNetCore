/*import React, { useState, useEffect } from "react"

const SearchComponent = () => {
    //setear los hooks useState
    const [users, setUsers] = useState([])
    //const [search, setSearch] = useState("")

    //funcion para traer los datos de la Api


    const showData = async () => {
        const response = await fetch('home/index')
        const data = await response.json()
        setUsers(data)
        console.log(fetch('home/index'))
        console.log(data)
    }
    showData()

    //Metodo de filtrado

    //funcion de busqueda
    useEffect(() => {
        showData()
    }, [])

    //renderizamos la vista
    
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
                            <td>{user.Artists}</td>
                            <td>{user.Date}</td>
                            <td><img src={user.ImageUrl} /></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default SearchComponent
*/

/*
import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.id}>
                            <td>{forecast.Name}</td>
                            <td>{forecast.Artists}</td>
                            <td>{forecast.Date}</td>
                            <td><img src={forecast.ImageUrl} /></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderForecastsTable(this.state.forecasts);

    }

    async populateWeatherData() {
        const response = await fetch('home/index');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
        console.log(fetch('home/index'))
        console.log('home/index')
        console.log(data)
    }
}*/


/*function Login() {
    return (
        <div className="login">
            <img
                src="https://storage.googleapis.com/pr-newsroom-wp/1/2018/11/Spotify_Logo_CMYK_Black.png"
                alt=""
            />
            <button>LOGIN WITH SPOTIFY</button>
        </div>
    );
}
export default Login;
*/

// PRUEBAS DE CONEXIÓN

  /*const endpoint = "https://accounts.spotify.com/authorize"
    const clientID = "07154d0264744fed89a3c008f1292ef9"
    const redirectUri = "https://localhost:3000/"
    const scopes = ["user-read-currently-playing",
        "user-read-recently-played",
        "user-read-playback-state",
        "user-top-read",
        "user-modify-playback-state"]

    const loginUR = `${endpoint}?client_id=${clientID}&response_type=token&redirect_uri=${redirectUri}&scope=${scopes.join("%20")}&show_dialog=true`
    */
    //const loginU = loginURL

import React, { useState, useEffect } from "react"
import { LoginButton, LoginContainer } from "./Styles"
import { Buffer} from "buffer"


const LoginPassURL = () => {
        const [loginURL, setLoginURL] = useState([]);
    
        const showData = async () => {
            const response = await fetch('home/login')
            const encode = await response.json()
            const data = Buffer.from(encode, "base64").toString()
            setLoginURL(data)
        }
       //showData()
        
        useEffect(() => {
        showData()
        },[])
        return (
            <LoginContainer>
            <img src="https://1000logos.net/wp-content/uploads/2017/08/Spotify-symbol.jpg" alt="logo" />
            <LoginButton href={loginURL}>Login with Spotify</LoginButton>
        </LoginContainer>
            )
    }
        export default LoginPassURL