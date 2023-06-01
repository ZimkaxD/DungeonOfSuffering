using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public Dialog dialog;
    public Animator DialogOpen;

    public void DialogTrigger()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
    

    public void EndDialog()
    {
        DialogOpen.SetBool("Dialog", false);
    }
}
