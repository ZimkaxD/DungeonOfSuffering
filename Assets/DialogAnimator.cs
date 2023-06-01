using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    public GameObject Hero;
    public GameObject Shop;
    public GameObject Donate;
    public Animator DialogOpen;
    public DialogManager dialogManager;
    public bool isOpened = false;

    public void Start()
    {
        Hero = GameObject.Find("Hero");
    }

    public void Update()
    {
        PlayerStats playerStats = Hero.GetComponent<PlayerStats>();
        TriggerDialog triggerDialog = GetComponent<TriggerDialog>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector2.Distance(Hero.transform.position, transform.position);


            if (distance < 1.5f)
            {
                if(isOpened)
                {
                    DialogOpen.SetBool("DialogOpen", false);
                    isOpened=false;
                }
                else
                {
                    DialogOpen.SetBool("DialogOpen", true);   
                    triggerDialog.DialogTrigger();
                    isOpened=true;
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            DialogOpen.SetBool("DialogOpen", false);
            Donate.SetActive(false);
            Shop.SetActive(false);
            dialogManager.EndDialog();
        }
    }
}
