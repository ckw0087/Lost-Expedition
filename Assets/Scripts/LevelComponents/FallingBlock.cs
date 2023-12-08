using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : LevelComponent
{
    public override void Damage(PlayerMotor player)
    {
        base.Damage(player);
        Debug.Log("Rock");
    }
}
