    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    public GameObject[] Panels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickPanel(int i) {
        foreach (var item in Panels)
        {
            item.SetActive(false);
        }
        Panels[i].SetActive(true);
    }
}
