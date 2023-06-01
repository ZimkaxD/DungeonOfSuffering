using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : MonoBehaviour
{
    private GameObject Hero;
    public GameObject bullet;
    public Transform pointToShot;

    private float reload;
    public float startReload;


    void Start()
    {
        Hero = GameObject.Find("Hero");
    }


    void Update()
    {
        PlayerStats playerStats = Hero.GetComponent<PlayerStats>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 

     
        Vector3 direction = mousePosition - Hero.transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (reload <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                playerStats.UseMana(1);
                if (playerStats.currentMana > 0)
                {
                    Instantiate(bullet, pointToShot.position, transform.rotation);
                    reload = startReload;
                }
            }
        }
        else
        {
            reload -= Time.deltaTime;
        }
       
    }
}
