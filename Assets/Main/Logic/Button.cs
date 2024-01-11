public class Button : ClickableElement
{
    void OnDisable()
    {
        OnClick = null;
    }
}
