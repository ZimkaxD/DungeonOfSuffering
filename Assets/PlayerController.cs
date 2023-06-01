using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        // Поворот героя в направлении движения
        if (movement != Vector2.zero)
        {
            if (movement.x >= 0f)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movement.x < 0f)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (animator.GetInteger("Animation") == 0)
            {
                animator.SetInteger("Animation", 1);
            }
        }
        else
        {
            animator.SetInteger("Animation", 0);
        }
    }

}
