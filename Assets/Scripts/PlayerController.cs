using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public SimpleTouchPad touchPad;

    private Rigidbody rb;
    private int count;

    private Quaternion calibrationQuaternion;

    private void Start()
    {
        CalibrateAccelerometer();
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Vector3 rawAcceleration = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(rawAcceleration);
        //Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);

        //Vector2 direction = touchPad.GetDirection();
        //Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 11)
        {
            winText.text = "You win!";
        }
    }

    void CalibrateAccelerometer ()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAcceleration(Vector3 acceleration)
    {
        return calibrationQuaternion * acceleration;
    }
}
