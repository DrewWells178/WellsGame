using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrapManager : MonoBehaviour
{
    public Text scrapText;
    public int currentScrap;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentScrap"))
        {
            currentScrap = PlayerPrefs.GetInt("CurrentScrap");
        }
        else
        {
            currentScrap = 0;
            PlayerPrefs.SetInt("CurrentScrap", 0);
        }
        scrapText.text = "Scrap: " + currentScrap;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adding Game Currency To Update UI
    public void AddScrap(int scrapToAdd)
    {
        currentScrap+= scrapToAdd;
        PlayerPrefs.SetInt("CurrentScrap", currentScrap);
        scrapText.text = "Scrap: " + currentScrap;
    }
}
