using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MyHp;

    [SerializeField] private TextMeshProUGUI hpText;

    void Start()
    {
        hpText.text = $"{MyHp}";
    }

    public async UniTask ChangeMyHpToZero()
    {
        for (int i = MyHp; i >= 0; i--)
        {
            hpText.text = $"{i}";
            await UniTask.Delay(50);
        }
    }
}
