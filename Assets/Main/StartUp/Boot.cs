using Cysharp.Threading.Tasks;
using OneSignalSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    public int MainSceneIndex = 1;

    async void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        Application.targetFrameRate = 60;

        await LoadMain();
    }

    private async UniTask LoadMain()
    {
        OneSignal.ConsentRequired = true;
        OneSignal.Initialize("9a59fe3e-3d4e-4e54-a426-82ce949ad762");
        OneSignal.User.PushSubscription.OptIn();

        await UniTask.Delay(1000);

        var task = SceneManager.LoadSceneAsync(MainSceneIndex);
    }
}
