using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    public GameObject[] point = new GameObject[12];
    private GameObject Hero;
    public GameObject Bolet;
    public GameObject floatingDamage;
    private AddRoom room;

    public int damage;
    public int hp = 5;
    private int action, rand = 0;
    public float speed = 1f;
    private float timer = 2f;

    public static int EnemyCount = 1;

    void Start()
    {
        Hero = GameObject.Find("Hero");
        room = GetComponentInParent<AddRoom>();
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Hero.transform.position.x, Hero.transform.position.y)) < 7f)
        {
            action = 1;
        }
        else
        {
            action = 0;
        }
        if (action == 0)
        {
            if (point[rand].transform.parent != null)
            {
                for (int i = 0; i < point.Length; i++)
                {
                    point[i].transform.parent = null;
                }
            }
            if (transform.position != point[rand].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, point[rand].transform.position, speed * Time.deltaTime);
            }
            else
            {
                rand = Random.Range(0, 12);
            }
        }
        else if (action == 1)
        {
            if (point[rand].transform.parent == null)
            {
                for (int i = 0; i < point.Length; i++)
                {
                    point[i].transform.parent = transform;
                }
            }
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Hero.transform.position.x, Hero.transform.position.y)) < 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, Hero.transform.position, speed * Time.deltaTime);

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Hero.transform.position, speed * Time.deltaTime);
            }
            if (timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {

                    timer = 2;   
                    Instantiate(Bolet, transform.position, Quaternion.identity);
                    
                
            }

        }
        if(hp<=0)
        {
            Destroy(gameObject);
            room.enemies.Remove(gameObject);
        }
        if(Hero.transform.position.x>transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (action == 0)
        {
            rand = Random.Range(0, 12);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
   
        if(other.name == "Hero")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            playerStats.armorRecoveryTimer2 = 0f;
        }
    }
    public void TakeDamage(int damage)
    {
        hp-=damage;
        Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 0.75f);
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage=hp;
    }
}

