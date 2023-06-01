using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject Hero;
    public GameObject potionPrefab;  
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
        
        Instantiate(potionPrefab, transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
        isOpened = true;
        Destroy(gameObject);
        Instantiate(openChestPrefab, transform.position,Quaternion.identity);

    }
    public void Interact()
    {
        if (!isOpened)
        {
            OpenChest();
        }
    }
}

