using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundWidget : MonoBehaviour
{
    [SerializeField] private List<MoveBackground> backgroundList = new();
    [SerializeField] private float speedMultiplier;

    private void Start()
    {
        foreach (var item in backgroundList)
        {
            item.MultSpeed(speedMultiplier);
        }
    }
}
