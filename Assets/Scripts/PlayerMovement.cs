using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public bool isGrounded;
    public Rigidbody rb;
    public float jumpForce;
    public float mouseSensitivity;
    public float moveSpeed;
    private float moveHorizontal;
    private float moveVertical;
    private float moveX;
    private float moveY;
    private float rotationY = 0.0f;

    public GameObject target;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        jumpForce = 4f;
        mouseSensitivity = 4f;
        moveSpeed = 4f;

    }

    // Update is called once per frame
    void Update()
    {
        //movement inputs
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Mouse X");
        moveY = Input.GetAxis("Mouse Y");
        rotationY -= moveY * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        //movement
        target.transform.Rotate(0, moveX * mouseSensitivity, 0);
        cam.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        target.transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime, Space.Self);
        target.transform.Translate(Vector3.forward * moveVertical * moveSpeed * Time.deltaTime, Space.Self);
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 2.0f, 0) * jumpForce, ForceMode.Impulse);
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        { 
            isGrounded = false;
        }
    }
}