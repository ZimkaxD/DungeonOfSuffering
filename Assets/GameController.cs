using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> maps;
    private GameObject currentMap;
    private Vector3 playerStartPosition;

    void Start()
    {
        currentMap = GameObject.FindGameObjectWithTag("Map");

        playerStartPosition = GameObject.FindGameObjectWithTag("Hero").transform.position;
    }

    public IEnumerator SelectMap(int levelIndex)
    {
        yield return new WaitForSeconds(2);
        if (currentMap != null)
        {
            
            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
            foreach (GameObject room in rooms)
            {
                Destroy(room);
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag("PointEnemy");
            foreach (GameObject enemypoint in enemyPoints)
            {
                Destroy(enemypoint);
            }
            GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
            foreach (GameObject chest in chests)
            {
                Destroy(chest);
            }
            GameObject[] potions = GameObject.FindGameObjectsWithTag("Potion");
            foreach (GameObject potion in potions)
            {
                Destroy(potion);
            }
            GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
            foreach (GameObject weapon in weapons)
            {
                Destroy(weapon);
            }
            GameObject portal = GameObject.FindGameObjectWithTag("Portal");
            Destroy(portal);
            GameObject key = GameObject.FindGameObjectWithTag("Key");
            Destroy(key);

            // ”ничтожаем текущий префаб карты
            Destroy(currentMap);
        }

        currentMap = Instantiate(maps[levelIndex], transform.position, Quaternion.identity);

        GameObject player = GameObject.FindGameObjectWithTag("Hero");
        player.transform.position = playerStartPosition;
    }


    void Update()
    {
        
    }
}
