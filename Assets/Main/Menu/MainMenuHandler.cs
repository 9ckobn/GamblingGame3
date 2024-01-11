using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public UIScreen mainMenuScreen;

    public static MainMenuHandler instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void OnEnable()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        OpenMenu();
    }

    public void OpenMenu()
    {
        mainMenuScreen.StartScreen();
    }
}