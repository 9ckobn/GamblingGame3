using UnityEngine;

public class PauseScreen : UIScreen
{
    GameSetupHandler gsh;

    [SerializeField] private ClickableElement restart, toMenu, close;

    public override void StartScreen()
    {
        gameObject.SetActive(true);
    }

    public void SetupScreen(GameSetupHandler gameSetupHandler)
    {
        gsh = gameSetupHandler;

        restart.OnClick += async () =>
        {
            await CloseScreenWithAnimation();
            gsh.RestartLevel();
        };

        toMenu.OnClick += async () =>
        {
            await CloseScreenWithAnimation();
            gsh.CloseLevel();
            MainMenuHandler.instance.OpenMenu();
        };

        close.OnClick += async () =>
        {
            await CloseScreenWithAnimation();
        };

        StartScreen();
    }
}