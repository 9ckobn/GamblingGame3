using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : UIScreen
{
    [SerializeField] private ShopConfig shopConfig;
    [SerializeField] private SlotItem[] slotItem;

    [Space(1)]
    [SerializeField] private Image cardImage, cardBackGround;
    [SerializeField] private TextMeshProUGUI h_1, h_2, price;

    [SerializeField] private GameObject defaultText, mainText;

    [Space(1)]
    public Sprite defaultSprite, selectedSprite;

    public ClickableElement closeButton;

    public override void StartScreen()
    {
        gameObject.SetActive(true);

        SetupShop();

        closeButton.OnClick += async () =>
        {
            await CloseScreenWithAnimation();

            CloseCards();

            MainMenuHandler.instance.OpenMenu();
        };
    }



    public void CloseCards()
    {
        mainText.SetActive(false);
        defaultText.SetActive(true);

        cardBackGround.sprite = defaultSprite;
        cardImage.enabled = false;
        cardImage.sprite = null;
        h_1.text = "";
        h_2.text = "";
        price.text = "";

        foreach (var item in slotItem)
        {
            item.backGroundImage.sprite = defaultSprite;
        }
    }

    private void SetupShop()
    {
        cardImage.enabled = false;
        for (int i = 0; i < slotItem.Length; i++)
        {
            slotItem[i].SetupOnClick(this, i);
        }
    }

    public void OpenCard(SlotItem slot)
    {
        CloseCards();

        h_1.text = shopConfig.slots[slot.myIndex].name;
        h_2.text = shopConfig.slots[slot.myIndex].description;
        price.text = shopConfig.slots[slot.myIndex].Price + "<size=30><sprite=0>";

        defaultText.SetActive(false);
        mainText.SetActive(true);

        cardBackGround.sprite = selectedSprite;

        cardImage.sprite = slot.mainImage.sprite;
        cardImage.enabled = true;
        cardImage.SetNativeSize();
    }
}
