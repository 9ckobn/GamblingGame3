using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSetupHandler : MonoBehaviour
{
    [SerializeField] private Level[] lvlPrefabs;

    [SerializeField] private UIScreen WinScreen;
    [SerializeField] private LoseScreen loseScreen;
    [SerializeField] private PauseScreen pauseScreen;

    [SerializeField] private ClickableElement pauseButton;

    Level openedLevel;

    int currentLevelIndex;

    public void OpenLvl(int index)
    {
        pauseButton.gameObject.SetActive(true);

        currentLevelIndex = index;

        openedLevel = Instantiate(lvlPrefabs[index], this.transform);

        openedLevel.myIndex = index;

        openedLevel.gameSetupHandler = this;
        openedLevel.gameObject.SetActive(true);

        pauseButton.OnClick = () => pauseScreen.SetupScreen(this);
    }

    internal async UniTask WinLevel()
    {
        await UniTask.Delay(500);
        WinScreen.StartScreen();
        CloseLevel();
    }

    internal async UniTask LoseLevel()
    {
        await UniTask.Delay(300);
        loseScreen.SetupScreen(this);
        CloseLevel();
    }

    internal void PauseGame()
    {
        pauseScreen.SetupScreen(this);
        // openedLevel.gameObject.SetActive(false);
    }

    internal void RestartLevel()
    {
        CloseLevel();

        openedLevel = Instantiate(lvlPrefabs[currentLevelIndex], this.transform);

        openedLevel.gameSetupHandler = this;
        openedLevel.gameObject.SetActive(true);

        pauseButton.gameObject.SetActive(true);
        pauseButton.OnClick = () => pauseScreen.SetupScreen(this);
    }

    internal void CloseLevel()
    {
        pauseButton.gameObject.SetActive(false);
        openedLevel.gameObject.SetActive(false);
    }
}
