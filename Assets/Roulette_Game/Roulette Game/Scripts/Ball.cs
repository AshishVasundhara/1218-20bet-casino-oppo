using UnityEngine;

public class Ball : MonoBehaviour
{
    // Variable definition
    private Vector3 initPosition;
    private Quaternion initRotation;
    private Rigidbody rb;

    public Transform aaaa;

    void Start()
    {
        // Variable initialization
        initPosition = transform.position;
        initRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();

        // Holding the ball still
        rb.isKinematic = true;
    }

    void Update()
    {
        aaaa.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // Playing audio according to the ball's movement
        if (rb.velocity.magnitude < 2)
        {
            AudioManager.getInstance().BallStopped();
        }
        else
        {
            AudioManager.getInstance().BallRolling();
        }
    }

    // This function is responsible for the ball's launch in the roulette
    public void SpinBall(float velocity)
    {
        // Apply gravity
        rb.isKinematic = false;

        // Apply direction
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initPosition;
        transform.rotation = initRotation;

        // Launch
        rb.AddForce(transform.forward * velocity * 20f);
    }
}
