using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairControl : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

    void start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }
}
