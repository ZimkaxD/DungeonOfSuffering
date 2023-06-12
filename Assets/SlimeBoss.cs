using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 10;
    public float shootCooldown = 10f;
    public float bulletSpeed = 5f;
    public int bulletDamage = 5;
    public int maxHealth = 100;
    public float immuneThreshold = 0.5f;
    public GameObject bulletPrefab;
    public GameObject smallSlimePrefab;
    public Transform shootPoint;
    public Transform healthBar;
    public bool isHalf=false;
    public GameObject floatingDamage;
    public GameObject portalPrefab;
    public Transform portalSpawnPoint;

    public int currentHealth;
    private bool isImmune = false;
    private bool isShooting = false;
    private float lastShootTime = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // ����������� � ������
        MoveTowardsPlayer();

        // �������� ����� �������� ��������� �������
        Shoot();

        // ��������� ���������� ��������
        UpdateHealthBar();
    }

    private void MoveTowardsPlayer()
    {
        // �������� ������� ������
        GameObject player = GameObject.FindGameObjectWithTag("Hero");
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y)) < 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void Shoot()
    {
        if (!isShooting && Time.time - lastShootTime >= shootCooldown)
        {
            isShooting = true;
            StartCoroutine(ShootBullets());
        }
    }

    private IEnumerator ShootBullets()
    {
        // ��������������� �� �����
        moveSpeed = 0f;
        GameObject player = GameObject.FindGameObjectWithTag("Hero");
        // �������� � ������ �������
        for (int i = 0; i < 5; i++)
        {
            // ������� ����
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            // ���������� ���� � ��������� �����������
            Vector3 direction = (player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            // ������������� ���� ����
            bullet.GetComponent<Projectile>().damage = bulletDamage;

            yield return new WaitForSeconds(0.5f);
        }

        // ������������ ��������
        moveSpeed = 0.6f;

        // ���������� ����� ��������� ��������
        lastShootTime = Time.time;
        isShooting = false;
    }

    private void UpdateHealthBar()
    {
        // ��������� ������� ���������� ��������
        healthBar.localScale = new Vector3(currentHealth / maxHealth, 1f);

        // ���������, ���������� �� ������ �������� ��������
        if (currentHealth < maxHealth * immuneThreshold && !isImmune && !isHalf)
        {
            StartCoroutine(ImmunePhase());
            isHalf = true;
        }
    }

    private IEnumerator ImmunePhase()
    {
        // ���� ���������� ��������������� � �����
        isImmune = true;
        moveSpeed = 0f;

        // ��������� ��������� �������
        for (int i = 0; i < 5; i++)
        {
            Instantiate(smallSlimePrefab, transform.position, Quaternion.identity);
        }

        // ������� 3 �������
        yield return new WaitForSeconds(3f);

        // ���� ���������� ����� �������� � ����������
        isImmune = false;
        moveSpeed = 1.5f;
    }

    public void TakeDamage(int damage)
    {
        // ��������� ����, ���� ���� �� ������������� � �����
        if (!isImmune)
        {
            currentHealth -= damage;
            Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 1.5f);
            Instantiate(floatingDamage, damagePos, Quaternion.identity);
            floatingDamage.GetComponentInChildren<FloatingDamage>().damage = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            currentHealth = currentHealth;
        }
    }

    private void Die()
    {
        // ���������� ���� � ��������� �������������� ��������, ���� ����������
        Destroy(gameObject);

        SpawnPortal();
    }

    private void SpawnPortal()
    {
        Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "Hero")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            playerStats.armorRecoveryTimer2 = 0f;
        }
    }
}
