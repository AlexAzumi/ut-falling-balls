using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody myRigidbody;
    [Header("Properties")]
    public float jumpForce = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            myRigidbody.velocity = Vector3.up * jumpForce;
        }
    }
}
