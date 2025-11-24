using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public float changeAngleSpeed;

    private Rigidbody ball;
    private float angle;

    void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
    }

    void Update()
    {
        //Aim Left
        if (Input.GetKey(KeyCode.A))
        {
            ChangeAngle(-1);
        }
        //Aim Right
        if (Input.GetKey(KeyCode.D))
        {
            ChangeAngle(1);
        }
        //Power Up
        if (Input.GetKeyUp(KeyCode.Space))
        {

        }
    }

    private void ChangeAngle(int direction)
    {
        angle += changeAngleSpeed * Time.deltaTime * direction;
    }

}
