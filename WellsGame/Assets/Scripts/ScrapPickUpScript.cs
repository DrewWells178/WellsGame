using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPickUpScript : MonoBehaviour
{
    public int value;
    public ScrapManager theSM;    

    // Start is called before the first frame update
    void Start()
    {
        theSM = FindObjectOfType<ScrapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            value = Random.Range(0, value);
            theSM.AddScrap(value);
            Destroy(gameObject);
        }
    }
}
