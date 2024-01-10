using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Face Direction", fileName = "ActionFaceDirection")]
public class ActionFaceDirection : AIAction
{
    public override void Act(StateController controller)
    {
        FaceDirection(controller);
    }

    // Makes face the direction of our movement
    private void FaceDirection(StateController controller)
    {
        if (controller.Path != null)
        {
            if (controller.Path.Direction == FollowPath.MoveDirections.RIGHT 
                || controller.Path.Direction == FollowPath.MoveDirections.RIGHT_UP 
                || controller.Path.Direction == FollowPath.MoveDirections.RIGHT_DOWN)
            {
                controller.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                controller.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
