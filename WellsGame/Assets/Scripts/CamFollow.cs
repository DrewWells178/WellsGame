using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamFollow : MonoBehaviour
{   
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;

    
    void Start()
    {
       cam = Camera.main;
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;
        
        targetPos.x = Helper.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Helper.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);
        this.transform.position = targetPos;
    }
}
