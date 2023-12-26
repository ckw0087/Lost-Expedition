using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct PlayerNetworkData : INetworkInput
{
    public float HorizontalInput;
    public bool JumpPressed;
}
