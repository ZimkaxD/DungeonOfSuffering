using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MeinMenu : MonoBehaviour
{
    public DefaultBullet defaultBullet;
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Уровень-1 Сцена");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
