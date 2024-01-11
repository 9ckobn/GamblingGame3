using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainMenuScreen : UIScreen
{
    [SerializeField] private MoneyCountItem moneyCounter;


    public ClickableElement settingsButton, shopButton, daily;
    public UIScreen settingsScreen, shopScreen, dailyScreen;

    [SerializeField] private GameSetupHandler gsh;

    [SerializeField] private LevelSelectButton[] lvlSelectButton;

    [SerializeField] private GameObject barrierObj;

    public override void StartScreen()
    {
        moneyCounter.SetMoneyCount();

        settingsButton.OnClick += async () => await settingsButton.OpenScreenAsync(this, settingsScreen);
        shopButton.OnClick += async () => await shopButton.OpenScreenAsync(this, shopScreen);
        daily.OnClick += () => daily.OpenScreen(dailyScreen);

        for (int i = 0; i < lvlSelectButton.Length; i++)
        {
            if (i < 5)
            {
                lvlSelectButton[i].myIndex = i;

                Action buttonAction = async () =>
                {
                    await CloseScreenWithAnimation();
                };

                lvlSelectButton[i].SetupOnClick(buttonAction, gsh);
            }
            else
            {
                lvlSelectButton[i].OnClick = async () => await ShowBarrier();
            }
        }

        gameObject.SetActive(true);
    }

    private async UniTask ShowBarrier()
    {
        barrierObj.SetActive(true);
        await UniTask.Delay(2500);
        barrierObj.SetActive(false);
    }
}
