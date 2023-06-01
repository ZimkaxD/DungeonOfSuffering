using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool isPurchased = false;
        public GameObject DroppedItemPrefab; // Добавляем префаб выбрасываемого предмета
    }

    [SerializeField] List<ShopItem> ShopItemsList;

   
    public GameObject pauseMenuUI;
    public bool isPaused = false;
    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    PlayerStats playerStats;

    void Start()
    {
        Time.timeScale=0f;
        itemTemplate = ShopScrollView.GetChild(0).gameObject;

        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].isPurchased;

            int itemIndex = i;

            buyBtn.onClick.AddListener(() => OnShopItemBtnClicked(itemIndex));
        }

        Destroy(itemTemplate);
    }

    void Update()
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();

    }

    public void OnShopItemBtnClicked(int itemIndex)
    {
        if (ShopBuy.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            ShopBuy.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            ShopItemsList[itemIndex].isPurchased = true;

            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Продано";

            // Создаем выбрасываемый предмет
            GameObject droppedItem = Instantiate(ShopItemsList[itemIndex].DroppedItemPrefab, GetRandomDropPosition(), Quaternion.identity);

        }
        else
        {
            Debug.Log("Нет денег");
        }
    }
    private Vector3 GetRandomDropPosition()
    {
        GameObject npc=GameObject.Find("NPCSeller");
        float randomX = Random.Range(-1f, 1f); 
        float randomY = Random.Range(-1f, 1f); 
        float randomZ = Random.Range(-1f, 1f); 

        Vector3 randomPosition = npc.transform.position - new Vector3(randomX, randomY, randomZ); // Получаем случайную позицию относительно текущей позиции торговца

        return randomPosition;
    }

    

}
