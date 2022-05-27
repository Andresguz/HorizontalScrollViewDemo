using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public Text NickName;
    public Text Money;
    public Text comprado;
    public Text Errors;

    private int buyAmount;
    void Start()
    {
        buyAmount = GameManager.instance.playerData.money;
    }

  
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.playerData.idNavigation != null)
        {
          //  NickName.text = GameManager.instance.playerData.nickName;
            Money.text = buyAmount.ToString();
        }

    }
  public  void Diamont1()
    {
      
        buyAmount = GameManager.instance.playerData.money +20;
        StartCoroutine(DiamontRequest("http://localhost:8242/api/players", GameManager.instance.playerData.id));
    }

    IEnumerator DiamontRequest(string url, int id)
    {
        string json = "{\"Id\":\"" + id + "\", \"Nickname\":\"" + GameManager.instance.playerData.nickName +"\", \"Money\":\"" +buyAmount + "\" }";
        byte[] body = Encoding.UTF8.GetBytes(json);
        using (UnityWebRequest webrequest = UnityWebRequest.Put(url + "/" + id, body))
        {
            webrequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
            webrequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webrequest.SetRequestHeader("Content-Type", "application/json");
            yield return webrequest.SendWebRequest();

            switch (webrequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Errors.text = "El Usuario ya existe o es el valor es erroneo";
                    break;
                case UnityWebRequest.Result.Success:
                    print(webrequest.downloadHandler.text);
                    Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                   // GameManager.instance.playerData = player;
                  
                    break;
            };
        }
    }

}
