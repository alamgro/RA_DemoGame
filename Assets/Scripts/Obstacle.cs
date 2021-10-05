using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    private float initialY;
    private Transform positionPlayer;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        initialY = transform.localPosition.y;
    }

    private void Update()
    {
        //rb.MovePosition(-transform.right * speed * Time.fixedDeltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, initialY, 0f);

    }

    void FixedUpdate()
    {
        rb.velocity = -transform.root.right * speed * transform.parent.transform.localScale.x;
        //print(-transform.root.right);
    }
}
