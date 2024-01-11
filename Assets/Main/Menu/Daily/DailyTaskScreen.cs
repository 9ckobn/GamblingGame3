using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskScreen : UIScreen
{
    [SerializeField] private ClickableElement close;

    public override void StartScreen()
    {
        close.OnClick = async () => await CloseScreenWithAnimation();

        gameObject.SetActive(true);
    }
}
