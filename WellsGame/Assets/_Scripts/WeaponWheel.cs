using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponWheel : MonoBehaviour
{
    [SerializeField] private KeyCode wheelKey = KeyCode.Tab; 
    [SerializeField] private GameObject wheelParent;
    [SerializeField] private GameObject centerDot;
    [SerializeField] public GameObject player;
    private float cursorAngle;
    private int option = -1;
    private int currentWeapon = 0;

    private bool weaponsFull = false;
    private bool dropping = false;

    private Weapon[] weapons = new Weapon[3];
    private Sprite[] images = new Sprite[3];

    private Weapon tempWeapon = null;

    [System.Serializable]
    public class Wheel
    {
        public Sprite highlightSprite;
        private Sprite m_NormalSprite;
        public Image wheel;

        public Sprite NormalSprite
        {
            get => m_NormalSprite;
            set => m_NormalSprite = value;
        }
    }

    [SerializeField] private Wheel[] wheels = new Wheel[3];
    
    void Start()
    {
        Weapon.OnCollideWithWeapon += PickUpWeapon;
        DisableWheel();
        UpdateWheelPics();

        for(int i = 0; i < wheels.Length; i++)
        {
            if(wheels[i].wheel != null)
            {
                wheels[i].NormalSprite = wheels[i].wheel.sprite;
            }
        }
    }

    void Update()
    {
        if(Input.GetKey(wheelKey))
        {
            EnableWheel();
            SelectWeapon();
        }
        else if(Input.GetKeyUp(wheelKey))
        {
            DisableWheel();
            WheelWeapon(option);
        } 
        else if(dropping)
        {
            DroppingWeapon(tempWeapon);
        }
    }

    private void UpdateWheelPics()
    {
        for(int i = 0; i < 3; i++)
        {
            if(images[i] != null) 
            {
                Debug.Log(i);
                wheelParent.transform.GetChild(i).GetChild(0).GetComponent<Image>().overrideSprite = images[i];
            }
        }
    }

    private void EnableWheel()
    {
        if(wheelParent != null) wheelParent.SetActive(true);
    }

    private void DisableWheel()
    {
        if(wheelParent != null) wheelParent.SetActive(false);
    }

    private void EnableHighlight(int index)
    {
        for(int i = 0; i < wheels.Length; i++)
        {
            if(wheels[i].wheel != null && wheels[i].highlightSprite != null)
            {
                if(i == index) wheels[i].wheel.sprite = wheels[i].highlightSprite;
                else wheels[i].wheel.sprite = wheels[i].NormalSprite;
            }
        }
    }

    private void DisableAllHighlight()
    {
        for(int i = 0; i < wheels.Length; i++)
        {
            if(wheels[i].wheel != null) wheels[i].wheel.sprite = wheels[i].NormalSprite;
        }
    }

    private void GetAngle()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - centerDot.transform.position;
        if(v.x < 0)
        {
            cursorAngle = 270f - Mathf.Atan2(v.y, -v.x) * (180 / Mathf.PI);
        }
        else
        {
            cursorAngle = 90f + Mathf.Atan2(v.y, v.x) * (180 / Mathf.PI);
        }
    }

    private void SelectWeapon()
    {
        GetAngle();

        if(Helper.Close(Camera.main.ScreenToWorldPoint(Input.mousePosition), centerDot.transform.position, 1f))
        {
            DisableAllHighlight();
            option = -1;
        }
        else
        {
            if(cursorAngle > 120f && cursorAngle < 240f)
            {
                EnableHighlight(0);
                option = 0; 
            }
            else if(cursorAngle > 0f && cursorAngle < 120f)
            {
                EnableHighlight(1);
                option = 1;
            }
            else if(cursorAngle > 240f && cursorAngle < 360f)
            {
                EnableHighlight(2);
                option = 2;
            }
        }
    }

    private void ChangeWheelSprite(Sprite image, int index)
    {

    }

    public void PickUpWeapon(Weapon weaponToPickUp)
    {
        tempWeapon = weaponToPickUp;
        if(weaponsFull)
        {
            EnableWheel();
            dropping = true;
            DroppingWeapon(weaponToPickUp);
        }
        else
        {
            for(int i = 0; i < weapons.Length; i++)
            {
                if(weapons[i] == null)
                {
                    weapons[i] = weaponToPickUp;
                    images[i] = weaponToPickUp.weaponSprite;
                    if(weapons[currentWeapon] != null) weapons[currentWeapon].isEquipped = false;
                    weapons[i].EquipFromGround(player.transform);
                    currentWeapon = i;
                    if(i == 1) weaponsFull = true; 
                    UpdateWheelPics();
                    return;
                }
            }
            //update Wheel
        }

    }

    public void WheelWeapon(int option)
    {
        if(option > -1)
        {
            if(weapons[option] != null)
            {
                weapons[currentWeapon].isEquipped = false;
                weapons[option].ActivateWeapon();
                weapons[option].isEquipped = true;
            } 
        }
        currentWeapon = option;
    }

    private void DroppingWeapon(Weapon weapon)
    {
        bool selected = false;
        Time.timeScale = 0f;
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - centerDot.transform.position;

        if(v.x < 0)
        {
            cursorAngle = 270f - Mathf.Atan2(v.y, -v.x) * (180 / Mathf.PI);
        }
        else
        {
            cursorAngle = 90f + Mathf.Atan2(v.y, v.x) * (180 / Mathf.PI);
        }

        if(Helper.Close(Camera.main.ScreenToWorldPoint(Input.mousePosition), centerDot.transform.position, 1f))
        {
            DisableAllHighlight();
            option = -1;
        }
        else
        {
            if(cursorAngle > 120f && cursorAngle < 240f)
            {
                EnableHighlight(0);
                option = 0; 
            }
            else if(cursorAngle > 0f && cursorAngle < 120f)
            {
                EnableHighlight(1);
                option = 1;
            }
            else if(cursorAngle > 240f && cursorAngle < 360f)
            {
                EnableHighlight(2);
                option = 2;
            }
        }

        if(Input.GetMouseButton(0))
        {
                selected = true;
        }
        
        if(selected)
        {
            ReplaceWeapon(option, weapon);
            WheelWeapon(option);
            Time.timeScale = 1f;
            DisableWheel();
        }
        
    }

    private void ReplaceWeapon(int option, Weapon weapon)
    {
        if(option != -1)
        {
            if(option == currentWeapon)
            {
                weapons[currentWeapon].Drop(weapon.transform.position);
                weapons[currentWeapon] = weapon;

                weapons[currentWeapon].EquipFromGround(player.transform);
                images[option] = weapon.weaponSprite;
            }
            else
            {
                // Drop weapon
                weapons[option].Drop(weapon.transform.position);
                // move ground item to the slot
                weapons[option] = weapon;
                weapons[option].EquipFromGround(player.transform);
                // unequip current weapon
                weapons[currentWeapon].isEquipped = false;
                // equip weapon that was picked up
                currentWeapon = option;
                images[option] = weapon.weaponSprite;
                weapons[currentWeapon].isEquipped = true;
            }
            UpdateWheelPics();
        }
        dropping = false;
    }
}
