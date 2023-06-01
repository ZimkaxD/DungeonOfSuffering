using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Text healthText;
    public Text armorText;
    public Text manaText;
    public Text moneyText;

    private void Start()
    {
        healthText.text = playerStats.currentHealth.ToString();
        armorText.text =  playerStats.currentArmor.ToString();
        manaText.text =  playerStats.currentMana.ToString();
        moneyText.text=playerStats.currentMoney.ToString();
    }
    private void Update()
    {
        // ���������� ��������� �������� ��������, ����� � ����
        healthText.text =  playerStats.currentHealth.ToString();
        armorText.text =  playerStats.currentArmor.ToString();
        manaText.text =  playerStats.currentMana.ToString();
        moneyText.text=playerStats.currentMoney.ToString();
    }
}
