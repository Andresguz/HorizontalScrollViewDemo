using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayAcces : MonoBehaviour
{
    bool puede;
    public GameObject playgame;
    public Text mensaje;
    void Start()
    {
        puede=false;
        playgame.SetActive(false);
    }

    
    void Update()
    {
        //if (numero.aux>3)
        //{
        //    puede = true;
        //}
        if (GameManager.instance.Habilitado>=3)
        {
            puede = true;
        }
        
    }
    public void Accedio()
    {
        if (puede)
        {
           playgame.SetActive(true);
            mensaje.text = "BIENVENIDO";

        }
        else
        {
            mensaje.text = "NO cumples los requisitos";
        }
       
    }
   

}
