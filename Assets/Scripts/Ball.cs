using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool useGravity;
    public float gravityScale = 1;
    public Vector2 velocity;
    public float mouseImpulsePower;
    public float radius;
    public float mass;
    float speed;
    Vector2 gravity;

    private void Start()
    {
        gravity = Physics2D.gravity;

        radius = Random.Range(0.5f, 1.5f);
        mass = radius * 2;
        transform.localScale = Vector2.one * radius * 2;
    }

    private void Awake()
    {
        if (useGravity)
            velocity += gravity * gravityScale * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.fixedDeltaTime;

        if (useGravity)
            velocity += gravity * gravityScale * Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 moveVector = mousePos - transform.position;
            moveVector.Normalize();
            velocity = moveVector * mouseImpulsePower;
        }
    }
}
