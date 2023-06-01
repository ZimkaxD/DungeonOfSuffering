using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f,timer=5f;
    public int damage = 10;
    private GameObject Hero;


    private Rigidbody2D rb;

    private void Start()
    {
        Hero = GameObject.Find("Hero");
        rb = GetComponent<Rigidbody2D>();
        Vector2 snipe = Vector2.up * (Hero.transform.position.y - transform.position.y) + Vector2.right * (Hero.transform.position.x - transform.position.x) / (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Hero.transform.position.x, Hero.transform.position.y)) / 3);
        rb.AddForce(snipe * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            playerStats.armorRecoveryTimer2 = 0f;
            Destroy(gameObject);
        }
        
    }
}
