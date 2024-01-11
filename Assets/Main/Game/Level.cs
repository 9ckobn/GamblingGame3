using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Floor> allFloors;

    public GameSetupHandler gameSetupHandler;

    public int myIndex;

    public void Start()
    {
        foreach (var item in allFloors)
        {
            item.myLevel = this;
        }

        gameObject.SetActive(true);
    }

    public async void CheckIfLevelPassed()
    {
        var result = allFloors.TrueForAll(c => c.floorCleared);
        if (result)
        {
            await gameSetupHandler.WinLevel();
            // gameObject.SetActive(false);
        }
    }
}