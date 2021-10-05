using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerPlayer;
    private Rigidbody rb;
    private Vector3 initialPos;
    private const float GRAVITY = 60.0f;
    private float sizePlayer;
    private float scaleAdjustment;
    private bool canJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sizePlayer = GetComponent<Collider>().bounds.extents.y;

        initialPos = transform.localPosition;
        print(initialPos);
        scaleAdjustment = transform.parent.transform.localScale.x;
        jumpForce *= 10f; //Just to avoid entering large values in the inspector
    }

    void Update()
    {
        Physics.gravity = -transform.up * GRAVITY * scaleAdjustment;

        transform.localPosition = new Vector3(initialPos.x, transform.localPosition.y, initialPos.z);

        //Checar si está tocando el suelo, sino, sale del update
        if (!Physics.Raycast(transform.position, -transform.up, sizePlayer + (0.01f * scaleAdjustment), layerPlayer))
            return;

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce * rb.mass * scaleAdjustment);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canJump = false;
    }
}
