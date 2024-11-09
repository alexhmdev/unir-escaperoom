using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [SerializeField] Transform orientation;
    [SerializeField] GameObject player;

    float xRotation;
    float yRotation;

    Rigidbody rb;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Move the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = orientation.transform.right * x + orientation.transform.forward * z;
        rb.AddForce(move.normalized * 10f, ForceMode.Impulse);
        
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > 10f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized * 10f;
        }

    }



}
