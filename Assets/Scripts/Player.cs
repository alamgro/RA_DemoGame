using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerPlayer;
    private const float GRAVITY = 9.81f;
    private Rigidbody rb;
    private float sizePlayer;
    private float scaleAdjustment;
    private bool canJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sizePlayer = GetComponent<Collider>().bounds.extents.y;
        scaleAdjustment = transform.parent.transform.localScale.x;
    }

    void Update()
    {
        Physics.gravity = -transform.up * GRAVITY * scaleAdjustment;

        transform.localPosition = new Vector3(0f, transform.localPosition.y, 0f);

        //Checar si está tocando el suelo, sino, sale del update
        if (!Physics.Raycast(transform.position, -transform.up, sizePlayer + (0.01f * scaleAdjustment), layerPlayer))
            return;

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce / scaleAdjustment);
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
