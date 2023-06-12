using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Weapons")]
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public Image weaponIcon;

    [Header("Key")]
    public GameObject keyIcon;

    


    public int maxHealth = 100;
    public int maxMana = 100;
    public int maxArmor = 50;
    public int armorRecoveryRate = 1;
    public int armorRecoveryDelay = 0;

    public int currentHealth;
    public int currentMana;
    public int currentArmor;
    public int currentMoney;
    private bool isTakingDamage;
    private bool keyButtonPushed;
    private float armorRecoveryTimer;
    public float armorRecoveryTimer2;
    public Image[] state_image = new Image[4];
    public DefaultBullet defaultBullet;
    private Collider2D currentCollider;

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentArmor = maxArmor;

        // ��������� ���������� ����� ����� ������������ ���������
        InvokeRepeating("RecoverArmor", armorRecoveryDelay, 2f);
    }

    private void Update()
    {
        armorRecoveryTimer += Time.deltaTime;
        armorRecoveryTimer2 += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentCollider.CompareTag("Weapon"))
            {
                for (int i = 0; i < allWeapons.Length; i++)
                {
                    if (currentCollider.name == allWeapons[i].name+"(Clone)")
                    {
                        unlockedWeapons.Add(allWeapons[i]);
                    }
                }
                SwitchWeapon();
                Destroy(currentCollider.gameObject);
            }
            else if(currentCollider.CompareTag("Key"))
            {
                keyIcon.SetActive(true);
                Destroy(currentCollider.gameObject);
                keyButtonPushed = !keyButtonPushed;
            }
        }
    }
  
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy)
        {
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentCollider = other; 
    }

    public void TakeDamage(int damage)
    {
        float damagee=(float)damage;
        if (currentArmor > 0)
        {
            int remainingArmorDamage = Mathf.Max(damage - currentArmor, 0);
            
            currentArmor = Mathf.Max(currentArmor - damage, 0);
            damage = remainingArmorDamage;
            if (currentArmor>=maxArmor/5)
            {
                state_image[0].fillAmount -= damagee / maxArmor;
            }
            else
            {
                state_image[0].fillAmount -= (damagee/2-(float)0.7) / maxArmor;
            }
        }

        if (damage > 0)
        {
            currentHealth -= damage;
            if (currentHealth >= maxHealth/20)
            {
                if (currentHealth >= maxHealth / 5)
                {
                    state_image[1].fillAmount -= damagee / maxHealth;
                }
                else
                {
                    state_image[1].fillAmount -= (damagee / 5 + 4) / maxHealth;
                }
            }
            if (currentHealth <= 0)
            {
                
                UnityEngine.SceneManagement.SceneManager.LoadScene("Уровень-1 Сцена");
            }

        }
    }

    public void UseMana(int manaCost)
    {
        float manaa=(float)manaCost;
        if (currentMana > 0)
        {
            currentMana -= manaCost;
            if (currentMana >= maxMana/20)
            {
                if (currentMana >= maxMana / 5)
                {
                    state_image[2].fillAmount -=  manaa / maxMana;
                }
                else
                {
                    state_image[2].fillAmount -= (manaa / 5 + 4) / maxMana;
                }
            }
            if (currentMana <= 0)
            {
                currentMana = 0;
            }
        }
    }

    private void RecoverArmor()
    {
        float armorheal = (float)armorRecoveryRate;
        if (armorRecoveryTimer2 >= 5f)
        {
            if (armorRecoveryTimer >= 2f)
            {
                currentArmor += armorRecoveryRate;
                state_image[0].fillAmount += armorheal / maxArmor;
                currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
                armorRecoveryTimer = 0f;
            }
        }
    }

    public void TakeMoney(int moneyCount)
    {
        currentMoney += moneyCount;

    }

    public void SwitchWeapon()
    {
        for(int i = 0;i<unlockedWeapons.Count;i++)
        {
            if(unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);
                int nextIndex = (i + 1) % unlockedWeapons.Count;
                unlockedWeapons[nextIndex].SetActive(true);
                weaponIcon.sprite = unlockedWeapons[nextIndex].GetComponent<SpriteRenderer>().sprite;
                weaponIcon.SetNativeSize();
                break;
            }
        }
    }
}
