using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BuyCharcteer : MonoBehaviour
{
    public Text ch1;
    public Text ch2;
    public Text ch3;
    public Text ch4;
    public Text ch5;

    private int numCh;
    private int idCh;
    public bool buyNow;

    public bool ch1bool;
    public bool ch2bool;
    public bool ch3bool;
    public bool ch4bool;
    public bool ch5bool;

    public GameObject bt1;
    public GameObject bt2;
    public GameObject bt3;
    public GameObject bt4;
    public GameObject bt5;
    public Text comprado;
    public bool iscomprado=true;
    public int accedenum;

    public Text sk1;

    public Text a1;
    public Text a2;
    public Text a3;
    public Text a4;
    public Text a5;
    void Start()
    {
        StartCoroutine(GetRequestSkins("http://localhost:8242/api/Skins"));
        accedenum = 0;
        sumaP();
        for (int i = 0; i < GameManager.instance.playerData.playerSkins.Length; i++)
        {
            a1.text = GameManager.instance.playerData.playerSkins[0].skin.name.ToString();
            // Debug.Log(GameManager.instance.playerData.playerSkins[0].skin.name);
            a2.text = GameManager.instance.playerData.playerSkins[1].skin.name.ToString();
            a3.text = GameManager.instance.playerData.playerSkins[2].skin.name.ToString();
        }
    }

    void Update()
    {
     

        if (ch1bool == true)
        {
            bt1.SetActive(false);
        }
        if (ch2bool == true)
        {            
            bt2.SetActive(false);
        }

        if (ch3bool == true)
        {
            bt3.SetActive(false);
        }
        if (ch4bool == true)
        {
            bt4.SetActive(false);
        }
        if (ch5bool == true)
        {
            bt5.SetActive(false);
        }

        if (buyNow)
        {
         //   StartCoroutine(GetRequestSkins("http://localhost:8242/api/Skins"));
          StartCoroutine(Postskin1("http://localhost:8242/api/PlayerSkins/"));
        }
        //  sk1.text=  GameManager.instance.playerData.playerSkins[1].skin.name;
        // Debug.Log(GameManager.instance.playerData.playerSkins[1].skin.name);

       

    }
    void sumaP()
    {

        accedenum += 1;
    }
  public void btn1()
    {
        numCh = 1;
        idCh = Random.Range(1, 6000);
        buyNow = true;
        ch1bool = true;
        string name = "Infinito";
         StartCoroutine(PutRequest("http://localhost:8242/api/skins",numCh, name));
    }
 public void btn2()
    {
        numCh = 2;
        idCh = Random.Range(1, 6000);
        buyNow = true;
        ch2bool = true;
        string name = "Omega";
        StartCoroutine(PutRequest("http://localhost:8242/api/skins", numCh, name));
    }
   public void btn3()
    {
        numCh = 3;
        idCh = Random.Range(1, 6000);
        buyNow = true;
        ch3bool = true;
        string name = "penta";
        StartCoroutine(PutRequest("http://localhost:8242/api/skins", numCh, name));
    }
   public void btn4()
    {
        numCh = 4;
        idCh = Random.Range(1, 6000);
        buyNow = true;
        ch4bool = true;
        string name = "Delta";
        StartCoroutine(PutRequest("http://localhost:8242/api/skins", numCh, name));
    }
   public void btn5()
    {
        numCh = 5;
        idCh = Random.Range(1, 6000);
        buyNow = true;
        ch5bool = true;
        string name = "Sexta";
        StartCoroutine(PutRequest("http://localhost:8242/api/skins", numCh, name));
    }
    IEnumerator PutRequest(string url,int id,string nameS)
    {
        string json = "{\"Id\":\"" + id + "\", \"Code\":\"" + 90 + "\", \"Name\":\""+nameS + "\", \"IsActive\":\"" + true + "\"}";
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
                    comprado.text = "El Usuario ya existe o es el valor es erroneo";
                    break;
                case UnityWebRequest.Result.Success:
                    print(webrequest.downloadHandler.text);
                    comprado.text = "compra exitosa";
                    // Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    //  GameManager.instance.playerData = player;
                    // PanelsManager.onClickPanel(1);
                    break;
            };
        }
    }
    IEnumerator Postskin1(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerId", GameManager.instance.playerData.id);
        form.AddField("skinId", numCh);
        form.AddField("date", "2022-03-11T10:01:00");
        form.AddField("id", idCh);
        using (UnityWebRequest webrequest = UnityWebRequest.Post(url, form))
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
                    print(webrequest.downloadHandler.text);
                    //Player player = JsonUtility.FromJson<Player>(webrequest.downloadHandler.text);
                    PlayerSkin playerSkin = JsonUtility.FromJson<PlayerSkin>(webrequest.downloadHandler.text);
                    // print(playerSkin.skinId);
                    buyNow = false;
                    break;

            };
        }
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
                    // print(webrequest.downloadHandler.text);
                    Skins skins = JsonUtility.FromJson<Skins>("{\"skins\":" + webrequest.downloadHandler.text + "}");
                    for (int i = 0; i < skins.skins.Length; i++)
                    {
                        ch1.text = skins.skins[0].name.ToString();
                        ch2.text = skins.skins[1].name.ToString();
                        ch3.text = skins.skins[2].name.ToString();
                        ch4.text = skins.skins[3].name.ToString();
                        ch5.text = skins.skins[4].name.ToString();
                       // print(skins.skins[0].isActive);
                        ch1bool = skins.skins[0].isActive;
                        ch2bool = skins.skins[1].isActive;
                        ch3bool = skins.skins[2].isActive;
                        ch4bool = skins.skins[3].isActive;
                        ch5bool = skins.skins[4].isActive;
                    }
                    break;

            };
        }
    }
}
