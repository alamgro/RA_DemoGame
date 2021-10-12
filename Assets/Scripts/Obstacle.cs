using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    private float initialY;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        initialY = transform.localPosition.y;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, initialY, 0f);
    }

    void FixedUpdate()
    {
        rb.velocity = -transform.root.right * Speed;
    }

    public float Speed {
        get { return speed; } 
        set {
            speed = value; 
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
