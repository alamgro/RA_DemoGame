using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerPlayer;
    private Rigidbody rb;
    private float sizePlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sizePlayer = GetComponent<Collider>().bounds.extents.y;
        print(sizePlayer);
    }

    void Update()
    {
        Physics.gravity = -transform.up * 4f;

        transform.localPosition = new Vector3(0f, transform.localPosition.y, 0f);

        if (!Physics.Raycast(transform.position, -transform.up, sizePlayer + 0.005f, layerPlayer))
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
