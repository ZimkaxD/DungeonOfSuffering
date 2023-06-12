using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject Hero;
    public GameObject[] weaponPrefabs;
    public GameObject[] potionPrefabs;
    public GameObject coinPrefab;
    public GameObject manaPrefab;
    public GameObject openChestPrefab;

    private bool isOpened = false;

    private void Start()
    {
        Hero = GameObject.Find("Hero");
    }

    private void Update()
    {
        PlayerStats playerStats = Hero.GetComponent<PlayerStats>();

        if (Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            float distance = Vector2.Distance(Hero.transform.position, transform.position);
            if (distance < 0.5f)
            {
                OpenChest();
                Interact();
            }
        }
    }

    private void OpenChest()
    {
        int dropType = Random.Range(0, 3); // Randomly select drop type: 0 for weapon, 1 for potion, 2 for coins with mana

        switch (dropType)
        {
            case 0: // Weapon
                int weaponIndex = Random.Range(0, weaponPrefabs.Length); // Randomly select weapon variation
                Instantiate(weaponPrefabs[weaponIndex], transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
                break;
            case 1: // Potion
                int potionIndex = Random.Range(0, potionPrefabs.Length); // Randomly select potion variation
                Instantiate(potionPrefabs[potionIndex], transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
                break;
            case 2: // Coins with mana
                int coinCount = Random.Range(3, 6); // Randomly determine coin count between 3 and 5
                int manaCount = Random.Range(10, 21); // Randomly determine mana count between 10 and 20

                for (int i = 0; i < coinCount; i++)
                {
                    Instantiate(coinPrefab, transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
                }

                for (int i = 0; i < manaCount; i++)
                {
                    Instantiate(manaPrefab, transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
                }

                break;
        }

        isOpened = true;
        Destroy(gameObject);
        Instantiate(openChestPrefab, transform.position, Quaternion.identity);
    }

    public void Interact()
    {
        if (!isOpened)
        {
            OpenChest();
        }
    }
}
