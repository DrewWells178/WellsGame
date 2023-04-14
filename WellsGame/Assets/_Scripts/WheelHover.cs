using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WheelHover : MonoBehaviour
{
    public UnityEvent OnHover = new UnityEvent();
    
    void Start()
    {
        OnHover.AddListener(SetChoice);
    }

    void Update()
    {
        if(!Input.GetKey("r"))
        {
            OnHover.Invoke();
        }
    }

    void OnMouseEnter()
    {

    }

    void OnMouseExit()
    {

    }

    public void SetChoice()
    {
        //Player.WheelWeapon();
    }
}
