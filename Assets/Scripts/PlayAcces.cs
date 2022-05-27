using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayAcces : MonoBehaviour
{
    bool puede;
   public PlayerSkinsScrollView numero;
    void Start()
    {
        puede=false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (numero.aux>3)
        //{
        //    puede = true;
        //}
        if (GameManager.instance.skinstiene>=3)
        {
            puede = true;
        }
        
    }
    public void Accedio()
    {
        if (puede)
        {
           
            StartCoroutine(GetRequestCharacter("http://localhost:8242/api/PlayerSkins/", GameManager.instance.playerData.id));
        }
       
    }
    IEnumerator GetRequestCharacter(string url, int id)
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

                    PlayerSkins playerSkin = JsonUtility.FromJson<PlayerSkins>("{ \"playerskinss\":" + webrequest.downloadHandler.text + "}");

   
                    break;

            };
        }
    }

}
