using System;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;


public class SettingsScreen : UIScreen
{
    [SerializeField] private EmailSettings emailSettings;
    [SerializeField] private ReportScreen reportScreen;
    [SerializeField] private Sprite notifToggleEnable, notifToggleDisable;
    [SerializeField] private UnityEngine.UI.Button notification, privacy, terms, report, support, share, rate, close;
    [SerializeField] private string privacyUrl, termsUrl;

    private NativeShare nativeShareInstance;
    private WebViewObject webViewObject;

    private const string notificationKey = "notifs";
    private bool notificationsEnabled
    {
        get => PlayerPrefs.GetInt(notificationKey, 1) == 1;
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt(notificationKey, 1);
                notification.targetGraphic.GetComponent<Image>().sprite = notifToggleEnable;
            }
            else
            {
                PlayerPrefs.SetInt(notificationKey, 0);
                notification.targetGraphic.GetComponent<Image>().sprite = notifToggleDisable;
            }
        }
    }

    public override void StartScreen()
    {
        gameObject.SetActive(true);

        SetupNotification();

        SetupSendEmail();

        SetupButtons();
    }

    private void SetupSendEmail()
    {
        reportScreen.onSend = emailSettings.SendEmail;

        report.onClick.AddListener(OpenReport);
    }

    private void OpenReport()
    {
        reportScreen.StartScreen();
    }

    private void SetupNotification()
    {
        //todo Add Notification Service here
        notification.targetGraphic.GetComponent<Image>().sprite = notificationsEnabled ? notifToggleEnable : notifToggleDisable;
    }

    private void SetupButtons()
    {
        close.onClick.AddListener(async () =>
        {
            await CloseScreenWithAnimation();
            MainMenuHandler.instance.OpenMenu();

            notification.onClick.RemoveAllListeners();
            privacy.onClick.RemoveAllListeners();
            terms.onClick.RemoveAllListeners();
            report.onClick.RemoveAllListeners();
            support.onClick.RemoveAllListeners();
            share.onClick.RemoveAllListeners();
            rate.onClick.RemoveAllListeners();
            close.onClick.RemoveAllListeners();
        });

        notification.onClick.AddListener(SwitchNotifications);
        privacy.onClick.AddListener(() => OpenWebView(privacyUrl));
        terms.onClick.AddListener(() => OpenWebView(termsUrl));
        report.onClick.AddListener(() => reportScreen.StartScreen());
        support.onClick.AddListener(() => reportScreen.StartScreen());
        share.onClick.AddListener(ShareApp);
        rate.onClick.AddListener(() => Device.RequestStoreReview());
    }

    private void ShareApp()
    {
        if (nativeShareInstance == null)
        {
            nativeShareInstance = new NativeShare().SetTitle("Title").SetText("SubTitle and url to game at appstore");
        }
        else
        {
            nativeShareInstance.Share();
        }
    }

    private void OpenWebView(string URL)
    {
        webViewObject = (new GameObject("WebView")).AddComponent<WebViewObject>();
        webViewObject.Init(
            err: (msg) =>
            {
                Debug.Log($"Error: {msg}");
                Disable();
            },
            httpErr: (msg) =>
            {
                Debug.Log($"HttpError: {msg}");
                Disable();
            });

        webViewObject.LoadURL(URL);
        webViewObject.SetVisibility(true);
    }

    private void Disable()
    {
        Destroy(webViewObject.gameObject);
    }

    private void SwitchNotifications() => notificationsEnabled = !notificationsEnabled;
}


[Serializable]
public class EmailSettings
{
    public string mailToAddress;
    private string Subject = "Report from App";

    public void SendEmail(string body)
    {
        string emailUri = "mailto:" + mailToAddress + "?subject=" + Subject + "&body=" + body;

        Application.OpenURL(emailUri);
    }
}

