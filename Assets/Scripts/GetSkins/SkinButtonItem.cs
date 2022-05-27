using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButtonItem : MonoBehaviour
{
    public int id;
    public string skinName;
    public GetSkinsScrollView skinsScrollView;
    public Button button;
    [SerializeField] Text levelButtonText;


    private void Start()
    {
        levelButtonText.text = (skinName).ToString();
    }

    // click event of level button
    public void OnAvailableSkinButtonClick()
    {
        skinsScrollView.OnAvailableSkinButtonClick(id);
    }
}
