using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject Primary;
    public GameObject Secondary;
    public int currentweapon;
    // Start is called before the first frame update
    void Start()
    {
        currentweapon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentweapon !=1){
            EquipPrimary();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentweapon != 2)
        {
            EquipSecondary();
        }
    }

    public void EquipPrimary()
    {
        Secondary.SetActive(false);
        Primary.SetActive(true);
        currentweapon = 1;
        Primary.GetComponent<Gun>().Draw();
    }

    public void EquipSecondary()
    {
        Primary.SetActive(false);
        Secondary.SetActive(true);
        currentweapon = 2;
        Secondary.GetComponent<Gun>().Draw();
    }

}
