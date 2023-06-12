using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public int defaultLvl;
    public LayerMask solid;

    public int lvl;

    private void Awake()
    {
        // Сохраняем дефолтные значения
        defaultLvl = lvl;
    }

    private void OnDisable()
    {
        // Восстанавливаем дефолтные значения
        lvl = defaultLvl;
    }

    void Start()
    {
        damage=damage*lvl;
        Invoke("DestroyBullet", lifetime);
    }

    void Update()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, transform.up, distance, solid);
        if (info.collider != null)
        {
            if (info.collider.CompareTag("Enemy"))
            {
                info.collider.GetComponent<GreenSlime>().TakeDamage(damage);
                
            }
            else if (info.collider.CompareTag("SlimeBoss"))
            {
                info.collider.GetComponent<SlimeBoss>().TakeDamage(damage);
            }

            if (info.collider.CompareTag("NPC"))
            {
                info.collider.GetComponent<NPC>().TakeDamage(damage);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
