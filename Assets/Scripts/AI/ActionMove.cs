using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Action/Action Move", fileName = "ActionMove")]
public class ActionMove : AIAction
{
    public override void Act(StateController controller)
    {
        controller.transform.Translate(Vector3.right * 4f * Time.deltaTime);
    }
}
