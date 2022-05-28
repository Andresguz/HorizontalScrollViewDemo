using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuexamen : MonoBehaviour
{
    public GameObject shopCharacter;
    public GameObject menu;
    public GameObject playGame;

    private void Start()
    {
        menu.SetActive(true);
        shopCharacter.SetActive(false);
        playGame.SetActive(false);
    }
   public void shop()
    {
        menu.SetActive(false);
        shopCharacter.SetActive(true);
        playGame.SetActive(false);
    }
    public void playgame()
    {
        menu.SetActive(false);
        shopCharacter.SetActive(false);
        playGame.SetActive(true);
    }
    public void menuInicio()
    {
        menu.SetActive(true);
        shopCharacter.SetActive(false);
        playGame.SetActive(false);
    }
}
