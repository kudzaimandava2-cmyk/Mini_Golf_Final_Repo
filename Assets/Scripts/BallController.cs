using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public float changeAngleSpeed;
    public float lineLength;
    public Slider powerSlider;
    public TextMeshProUGUI puttCountLabel;
    public float minHoleTime;
    public Transform startTransform;

    private LineRenderer line;
    private Rigidbody ball;
    private float angle;
    private float powerUpTime;
    private float power;
    private int putts;
    private float holeTime;
    private Vector3 lastPosition;

    void Awake()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        
        if (ball.angularVelocity.magnitude < 0.01f)
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
                Putt();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                PowerUp();
            }
            UpdateLinePositions();
        }
        else
        {
            line.enabled = false;
        }
    }

    private void ChangeAngle(int direction)
    {
        angle += changeAngleSpeed * Time.deltaTime * direction;
    }


    private void UpdateLinePositions()
    {
        if (holeTime == 0) { line.enabled = true; }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * lineLength);
    }

    private void Putt()
    {
        lastPosition = transform.position;
        ball.AddForce(Quaternion.Euler(0, angle, 0) * Vector3.forward * maxPower * power, ForceMode.Impulse);
        power = 0;
        powerSlider.value = 0;
        powerUpTime = 0;
        putts++;
        puttCountLabel.text = putts.ToString();
    }

    private void PowerUp()
    {
        powerUpTime += Time.deltaTime;
        power = Mathf.PingPong(powerUpTime, 1);
        powerSlider.value = power;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hole")
        {
            CountHoleTime();
        }
    }
    private void CountHoleTime()
    {
        holeTime += Time.deltaTime;
        if (holeTime >= minHoleTime)
        {
            //Player has finished, move to next player
            Debug.Log("I'm in the hole and it took me " + putts + "putts to get in");
            holeTime = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hole")
        {
            LeftHole();
        }
    }

    private void LeftHole()
    {
        holeTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Out of Bounds")
        {
            transform.position = lastPosition;
            ball.linearVelocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }

    public void SetupBall(Color color)
    {
        transform.position = startTransform.position;
        angle = startTransform.rotation.eulerAngles.y;
        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        GetComponent<MeshRenderer>().material.SetColor("Color", color);
        line.material.SetColor("Color", color);
        line.enabled = true;
        putts = 0;
        puttCountLabel.text = "0";
    }
}
