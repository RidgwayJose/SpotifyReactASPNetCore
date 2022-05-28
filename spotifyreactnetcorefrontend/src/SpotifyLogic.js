import React, { useState, useEffect } from "react"

const getTokenFromURL = () => {

  return window.location.hash  //retona la parte hash (#) de una URL
  .substring(1)   //extrae el primer caracter e imprime el resto de la cadena
  .split("&")    //divide un string en un array usando el argumento como separador [a=b,c=d]
  .reduce((initial, item) => {
    //#accessToken=sadfsad&name=dasdsadsa
    //ejecuta una funcion reductora sobre cada elemnto del array y da como resultado un solo valor.
    let parts = item.split("=") //con split se divide los elemntos de cada array(string) en arrays [[a,b], [c,d]]
    initial[parts[0]] = decodeURIComponent(parts[1])    //accesa al elemento en posicion 0 // decodifica el elemento en posicion 1 (token)
    return initial
  }, {}) 
}

const getCodeFromURL = () => {

  return window.location.search  //retona la parte hash (#) de una URL
  .substring(1)   //extrae el primer caracter e imprime el resto de la cadena
  .split("&")    //divide un string en un array usando el argumento como separador [a=b,c=d]
  .reduce((initial, item) => {
    //#accessToken=sadfsad&name=dasdsadsa
    //ejecuta una funcion reductora sobre cada elemnto del array y da como resultado un solo valor.
    let parts = item.split("=") //con split se divide los elemntos de cada array(string) en arrays [[a,b], [c,d]]
    initial[parts[0]] = decodeURIComponent(parts[1])    //accesa al elemento en posicion 0 // decodifica el elemento en posicion 1 (token)
    return initial
  }, {}) 
}

export {getTokenFromURL, getCodeFromURL}