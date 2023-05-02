using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntranceScript : MonoBehaviour
{
    public string lastExitName;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            ScenePlayerScript.Instance.transform.position = transform.position;
            ScenePlayerScript.Instance.transform.eulerAngles = transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
