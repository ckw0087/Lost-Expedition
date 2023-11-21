using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private float recoilMultiplier;

    public Vector2 CurrentMovement { get; set; }
    public bool NormalMovement { get; set; }

    private Rigidbody2D myRigidbody2D;
    private Vector2 recoilMovement;

    // Start is called before the first frame update
    void Start()
    {
        NormalMovement = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (NormalMovement)
            MoveCharacter();

        Recoil();
    }

    private void MoveCharacter()
    {
        Vector2 currentMovePosition = myRigidbody2D.position + CurrentMovement * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(currentMovePosition);
    }

    private void Recoil()
    {
        if (recoilMovement.magnitude > 0.1f)
            myRigidbody2D.AddForce(recoilMovement * recoilMultiplier);
    }

    public void MovePosition(Vector2 newPosition)
    {
        myRigidbody2D.MovePosition(newPosition);
    }

    public void SetMovement(Vector2 newPosition)
    {
        CurrentMovement = newPosition;
    }

    public void ApplyRecoil(Vector2 recoilDirection, float recoilForce)
    {
        recoilMovement = recoilDirection * recoilForce;
    }

    public void Jump(float jumpForce)
    {
        myRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jumped.");
    }
}
