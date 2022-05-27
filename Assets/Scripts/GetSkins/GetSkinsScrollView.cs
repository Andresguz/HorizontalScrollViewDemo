using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GetSkinsScrollView : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    [SerializeField] GameObject BtnPref;
    [SerializeField] Transform BtnParent;
    [SerializeField] Text Error;
    List<GameObject> availableSkinsBtnObjets = new List<GameObject>();

    [SerializeField] GameObject playerSkinsObject;
    private void Start()
    {
        if (GameManager.instance.playerData.id == -1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GetRequestSkins("http://localhost:8242/api/skins"));
        }
       
    }

    public void LoadSkinButtons()
    {
        Skin[] skins = GameManager.instance.availableSkins;
        for (int i = 0; i < skins.Length; i++)
        {
            GameObject availableSkinBtnObj = Instantiate(BtnPref, BtnParent) as GameObject;
            availableSkinBtnObj.GetComponent<SkinButtonItem>().skinName = skins[i].name;
            availableSkinBtnObj.GetComponent<SkinButtonItem>().id = skins[i].id;
            foreach (var item in GameManager.instance.playerData.playerSkins)
            {
                if (item.skinId == skins[i].id)
                {
                   
                    availableSkinBtnObj.GetComponent<SkinButtonItem>().button.interactable = false;
                }
            }
            availableSkinBtnObj.GetComponent<SkinButtonItem>().skinsScrollView = this;
            availableSkinsBtnObjets.Add(availableSkinBtnObj);
        }
    }

    public void ClearAvailableSkinsButtons()
    {
        foreach (var item in availableSkinsBtnObjets)
        {
            Destroy(item);
        }
        availableSkinsBtnObjets.Clear();
    }

    IEnumerator GetRequestSkins(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    print("error");
                    break;
                case UnityWebRequest.Result.Success:
                    Skins skins = JsonUtility.FromJson<Skins>("{ \"skins\":" + webrequest.downloadHandler.text + "}");
                    GameManager.instance.availableSkins = skins.skins;
                    LoadSkinButtons();
                    break;

            };
        }
    }



    public void OnAvailableSkinButtonClick(int id)
    {
        levelNumberText.text = "SkinId: " + (id);
        StartCoroutine(BuySkin("http://localhost:8242/api/playerSkins", id));
        GameManager.instance.skinstiene++;
    }

    public void OnButtonClickRefresh()
    {
        StartCoroutine(Refresh("http://localhost:8242/api/players/" + GameManager.instance.playerData.id));
    }

    IEnumerator BuySkin(string url, int id)
    {
        Random rd = new System.Random();
        WWWForm form = new WWWForm();
        form.AddField("Id", rd.Next());
        form.AddField("PlayerId", GameManager.instance.playerData.id);
        form.AddField("SkinId", id);
        form.AddField("Date", DateTime.Now.ToString());
        using (UnityWebRequest webrequest = UnityWebRequest.Post(url, form))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Error.text = "ya compraron personaje";
                    break;
                case UnityWebRequest.Result.Success:
                    OnButtonClickRefresh();

                    print("success");
                    break;

            };
        }
    }

    IEnumerator Refresh(string url)
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(url))
        {
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Error.text = "Error en el servidor";
                    break;
                case UnityWebRequest.Result.Success:
                    if (webrequest.downloadHandler.text == "")
                    {
                        Error.text = "El Player no existe";
                    }
                    else
                    {
                        GameManager.instance.playerData = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                        ClearAvailableSkinsButtons();
                        LoadSkinButtons();

                        playerSkinsObject.GetComponent<PlayerSkinsScrollView>().ClearPlayerSkinsButtons();
                        playerSkinsObject.GetComponent<PlayerSkinsScrollView>().LoadPlayerSkinsButtons();

                    }
                    break;

            };
        }
    }
}