using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    [SerializeField] private Image myImage;
    [SerializeField] private Sprite defaultSprite, selectedSprite;
    [SerializeField] private RectTransform mainLayout;
    [SerializeField] private List<Enemy> allEnemies;

    [SerializeField] private BonusType withBonus = BonusType.None;
    [SerializeField] private Image bonusImage;

    public Level myLevel;

    private bool alreadyWithPlayer = false;

    private Player myPlayer;

    private Vector3 lastPosition;

    public bool floorCleared = false;

    void Start()
    {
        if (allEnemies.Count < 1)
            floorCleared = true;

        if (withBonus != BonusType.None)
        {
            bonusImage.enabled = true;
        }
    }

    public void SwitchFloorSprite(bool enable)
    {
        myImage.sprite = enable ? selectedSprite : defaultSprite;
    }

    public void EnterLayout(Player player)
    {
        if (alreadyWithPlayer)
        {
            player.transform.localPosition = lastPosition;
            return;
        }

        player.transform.SetParent(mainLayout);
        player.transform.localScale *= 0.8f;
        alreadyWithPlayer = true;

        myPlayer = player;

        if (!floorCleared) StartFight();
    }

    public void ExitLayout()
    {
        alreadyWithPlayer = false;
    }

    async UniTask StartFight()
    {
        myPlayer.AtFight = true;

        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (allEnemies[i].MyHp < myPlayer.MyHp)
            {
                lastPosition = myPlayer.transform.localPosition;
                await allEnemies[i].ChangeMyHpToZero();

                myPlayer.MyHp += allEnemies[i].MyHp;
                myPlayer.UpdateHp();

                allEnemies[i].gameObject.SetActive(false);

                // allEnemies.RemoveAt(i);
            }
            else
            {
                myLevel.gameSetupHandler.LoseLevel();
            }
        }

        if (withBonus != BonusType.None)
        {
            GainBonus();
        }

        floorCleared = true;
        myPlayer.AtFight = false;
        myLevel.CheckIfLevelPassed();
    }

    private void GainBonus()
    {
        switch (withBonus)
        {
            case BonusType.DoubleHealth:

                myPlayer.MyHp *= 2;
                myPlayer.UpdateHp();

                bonusImage.enabled = false;

                break;
            default:
                break;
        }
    }
}

public enum BonusType
{
    None,
    DoubleHealth
}
