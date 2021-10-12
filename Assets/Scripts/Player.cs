using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerPlayer;
    private Rigidbody rb;
    private Vector3 initialPos;
    private Collider colliderPlayer;
    Animator anim;
    private const float GRAVITY = 60.0f;
    private float sizePlayer;
    private float scaleAdjustment;
    private bool canJump = false;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderPlayer = GetComponent<Collider>();
        sizePlayer = colliderPlayer.bounds.extents.y;
        anim = GetComponent<Animator>();


        initialPos = transform.localPosition;

        scaleAdjustment = transform.parent.transform.localScale.x;
        jumpForce *= 10f; //Just to avoid entering large values in the inspector

    }

    void Update()
    {
        Physics.gravity = -transform.up * GRAVITY * scaleAdjustment;

        transform.localPosition = new Vector3(initialPos.x, transform.localPosition.y, initialPos.z);
        anim.SetInteger("Walk", 1);

        //Checar si está tocando el suelo, sino, sale del update
        Debug.DrawRay(colliderPlayer.bounds.center, -transform.up * 0.5f * scaleAdjustment, Color.cyan);
        if (!Physics.Raycast(colliderPlayer.bounds.center, -transform.up, sizePlayer + (0.01f * scaleAdjustment), layerPlayer))
            return;

        if (canJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ))
        {
            rb.velocity = rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.up * jumpForce * rb.mass * scaleAdjustment);

            anim.SetInteger("Walk", 0);
            anim.SetTrigger("jump");
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
