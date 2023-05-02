using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScriptOnLoad : MonoBehaviour
{
    public string objectID;
    private void Awake()
    {
        objectID = name + transform.position.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < Object.FindObjectsOfType<DontDestroyScriptOnLoad>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroyScriptOnLoad>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroyScriptOnLoad>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        
            DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
