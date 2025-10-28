using System;
using UnityEngine;

[Serializable]
public class PlayerProfile : Character
{
    public PlayerProfile(int health, string name) : base(health, name)
    {
    }
}
