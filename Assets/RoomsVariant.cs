using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsVariant : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] boss;

    public GameObject key;
    private AddRoom lastRoom; 

    [HideInInspector] public List<GameObject> rooms;

    private void Start()
    {
        StartCoroutine(RandomSpawner());
    }

    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();

        int rand = Random.Range(0, rooms.Count - 2);
        Instantiate(key, rooms[rand].transform.position, Quaternion.identity);

        GameObject bossPrefab = GetBossPrefab();
        Instantiate(bossPrefab, lastRoom.transform.position, Quaternion.identity);

        lastRoom.door.SetActive(true);
        lastRoom.DestroyWalls();

        string lastRoomType = GetRoomTypeFromName(lastRoom.gameObject.name);
        if (lastRoomType == "Bot" || lastRoomType == "Top" || lastRoomType == "Left" || lastRoomType == "Right")
        {
            Debug.Log("Last room type: " + lastRoomType);
        }
        else
        {
            // Если последняя комната не является комнатой с требуемыми названиями, удалите ее и создайте новую комнату
            Destroy(lastRoom.gameObject);
            CreateSpecialRoom();
        }
    }

    private GameObject GetBossPrefab()
    {
        return boss[0];
    }

    private string GetRoomTypeFromName(string roomName)
    {
        roomName = roomName.ToLower();
        if (roomName.Contains("bot"))
        {
            return "Bot";
        }
        else if (roomName.Contains("top"))
        {
            return "Top";
        }
        else if (roomName.Contains("left"))
        {
            return "Left";
        }
        else if (roomName.Contains("right"))
        {
            return "Right";
        }
        else
        {
            return "Unknown";
        }
    }

    private void CreateSpecialRoom()
    {
        // Выберите случайное имя комнаты из доступных вариантов
        string[] specialRoomTypes = { "Bot", "Top", "Left", "Right" };
        string randomRoomType = specialRoomTypes[Random.Range(0, specialRoomTypes.Length)];

        GameObject[] roomArray = GetRoomArrayByType(randomRoomType);
        if (roomArray != null && roomArray.Length > 0)
        {
            GameObject specialRoomPrefab = roomArray[Random.Range(0, roomArray.Length)];
            Instantiate(specialRoomPrefab, lastRoom.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No room prefab available for type: " + randomRoomType);
        }
    }

    private GameObject[] GetRoomArrayByType(string roomType)
    {
        switch (roomType)
        {
            case "Bot":
                return bottomRooms;
            case "Top":
                return topRooms;
            case "Left":
                return leftRooms;
            case "Right":
                return rightRooms;
            default:
                return null;
        }
    }
}
