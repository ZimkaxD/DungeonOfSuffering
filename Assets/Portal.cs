using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex;
    private GameController gameController;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ChangeLevel();
        }
    }
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameControll").GetComponent<GameController>();
    }

    private void ChangeLevel()
    {
        StartCoroutine(gameController.SelectMap(nextLevelIndex));
    }
}
