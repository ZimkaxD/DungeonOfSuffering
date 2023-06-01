using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameDialog;

    public Animator Dialog;
    public Animator DialogOpen;

    private Queue<string> dialogTexts;

    private void Start()
    {
        dialogTexts = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        Dialog.SetBool("Dialog", true);
        DialogOpen.SetBool("DialogOpen", false);

        nameDialog.text = dialog.name;
        dialogTexts.Clear();

        foreach(string dialogText in dialog.textDialog)
        {
            dialogTexts.Enqueue(dialogText);
        }
        DisplayNextDialogText();
    }

    public void DisplayNextDialogText()
    {
       
        string dialogText=dialogTexts.Dequeue();
        StopAllCoroutines();
        StartCoroutine(PrintDialogText(dialogText));
        if (dialogTexts.Count == 0)
        {
            dialogTexts.Enqueue("Спасибо за покупки и удачи в приключениях!");
            PrintDialogText(dialogText);
            return;
        }
    }

    IEnumerator PrintDialogText(string message)
    {
        dialogText.text = "";  
        foreach(char letter in message.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    public void EndDialog()
    {
        Dialog.SetBool("Dialog", false);
    }


}
