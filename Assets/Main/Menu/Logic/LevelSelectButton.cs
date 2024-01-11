using System;
using UnityEngine;

public class LevelSelectButton : ClickableElement
{
    public int myIndex;

    public void SetupOnClick(Action onClick, GameSetupHandler gameSetupHandler)
    {
        this.OnClick = () =>
        {
            onClick?.Invoke();
            gameSetupHandler.OpenLvl(myIndex);
        };
    }
}