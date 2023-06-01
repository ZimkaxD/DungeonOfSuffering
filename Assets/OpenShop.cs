using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public GameObject Shop;
    public Animator CloseDialog;

    public void Start()
    {
        
    }
    public void OpeningShop()
    {
        Shop.SetActive(true);
        CloseDialog.SetBool("Dialog",false);
    }
    public void CloseShop()
    {
        Time.timeScale=1f;
        Shop.SetActive(false);
    }

}
