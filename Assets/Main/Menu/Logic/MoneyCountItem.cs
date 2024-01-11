using TMPro;
using UnityEngine;

public class MoneyCountItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;

    public int coinSpriteSize = 30;

    void OnEnable()
    {
        SetMoneyCount();
    }

    public void SetMoneyCount()
    {
        counter.text = $"{PlayerStats.MoneyCount} <size={coinSpriteSize}><sprite=0>";
    }
}