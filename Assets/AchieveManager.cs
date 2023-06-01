using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveManager : MonoBehaviour
{
    public GameObject achievementPanel;
    public Text achievementNameText;
    public Text achievementDescriptionText;
    public Image achievementIcon;

    public float displayTime = 3f; 

    private bool isDisplayingAchievement = false;
    private float displayTimer = 0f;

    private void Start()
    {
        achievementPanel.SetActive(false);
    }
    private void Update()
    {
        if (isDisplayingAchievement)
        {
            displayTimer += Time.deltaTime;

            if (displayTimer >= displayTime)
            {
                HideAchievement();
            }
        }
    }

    public void ShowAchievement()
    {
        achievementPanel.SetActive(true);
        isDisplayingAchievement = true;
        displayTimer = 0f;
    }

    public void HideAchievement()
    {
        achievementPanel.SetActive(false);
        isDisplayingAchievement = false;
    }
}
