using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : UIScreen
{
    [SerializeField] private Image roulette;

    private int[] winSize = new int[8] { 10, 50, 100, 150, 200, 300, 250, 90 };

    [SerializeField] private ClickableElement claimButton;

    private int currentWin;

    public override void StartScreen()
    {
        gameObject.SetActive(true);

        GenerateWin();
    }

    private void GenerateWin()
    {
        var randomIndex = Random.Range(1, 8);
        currentWin = winSize[randomIndex];
        roulette.rectTransform.DOLocalRotate(new Vector3(0, 0, randomIndex * 45), 5, RotateMode.FastBeyond360).SetEase(Ease.OutQuint).OnComplete(SetupWin);
    }

    private void SetupWin()
    {
        claimButton.gameObject.SetActive(true);

        claimButton.OnClick += async () =>
        {
            await CloseScreenWithAnimation();
            PlayerStats.MoneyCount += currentWin;
            MainMenuHandler.instance.OpenMenu();
            roulette.transform.localEulerAngles = Vector3.zero;
            claimButton.gameObject.SetActive(false);
        };
    }
}
