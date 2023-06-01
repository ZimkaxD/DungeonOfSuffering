using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Donate : MonoBehaviour
{
    [System.Serializable]
    class DonateItem
    {
        public Sprite Image;
        public int Price;
        public bool isPurchased = false;
        public GameObject DroppedItemPrefab; // Добавляем префаб выбрасываемого предмета
    }

    [SerializeField] List<DonateItem> DonateItemsList;

    public GameObject pauseMenuUI;
    public GameObject DonateWindow;
    public bool isPaused = false;
    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform DonateScrollView;
    Button buyBtn;
    PlayerStats playerStats;

    void Start()
    {

        itemTemplate = DonateScrollView.GetChild(0).gameObject;

        int len = DonateItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, DonateScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = DonateItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = DonateItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !DonateItemsList[i].isPurchased;

            int itemIndex = i;

            buyBtn.onClick.AddListener(() => OnDonateItemBtnClicked(itemIndex));
        }

        Destroy(itemTemplate);
    }

    void Update()
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();

    }

    public void OnDonateItemBtnClicked(int itemIndex)
    {
        if (ShopBuy.Instance.HasEnoughCoins(DonateItemsList[itemIndex].Price))
        {
            ShopBuy.Instance.UseCoins(DonateItemsList[itemIndex].Price);
            DonateItemsList[itemIndex].isPurchased = true;

            buyBtn = DonateScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Продано";

            // Создаем выбрасываемый предмет
            GameObject droppedItem = Instantiate(DonateItemsList[itemIndex].DroppedItemPrefab, GetRandomDropPosition(), Quaternion.identity);
            // Добавьте любую логику для размещения выбрасываемого предмета перед спрайтом торговца
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
