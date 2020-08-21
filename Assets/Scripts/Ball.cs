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
    float speed;
    Vector2 gravity;

    private void Start()
    {
        gravity = Physics2D.gravity;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, transform.position + new Vector3(velocity.x, velocity.y, 0), 1 * Time.fixedDeltaTime);
        

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
