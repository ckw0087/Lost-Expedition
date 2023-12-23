using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxPendulum : LevelComponent
{
    public override void Damage(PlayerMotor player)
    {
        base.Damage(player);
        Debug.Log("Ax Pendulum");
    }
}
