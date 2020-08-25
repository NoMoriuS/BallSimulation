using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Wall : MonoBehaviour
{
    public Vector2 size;
    public float bouncess;
    public List<Ball> enteredBalls;
    Vector3 topLimit;
    Vector3 downLimit;
    Vector3 rightLimit;
    Vector3 leftLimit;
    Vector3 leftUpCorner;
    Vector3 rightUpCorner;
    Vector3 leftDownCorner;
    Vector3 rightDownCorner;
    float distance;
    Ball[] balls;

    private void Start()
    {
        balls = FindObjectsOfType<Ball>();
    }

    private void FixedUpdate()
    {
        foreach (var item in balls)
        {
            //if (!enteredBalls.Contains(item))
            //{
                if (IsTouchWall(item))
                    OnBallEnter(item);
            //}
            //else
            //{
            //   if (!IsTouchWall(item))
            //        enteredBalls.Remove(item);
            //}
        }
    }

    private void Update()
    {
        topLimit = transform.position + transform.up * size.y / 2f;
        downLimit = transform.position - transform.up * size.y / 2f;
        rightLimit = transform.position + transform.right * size.x / 2f;
        leftLimit = transform.position - transform.right * size.x / 2f;


        leftUpCorner = topLimit - transform.right * size.x / 2f;
        rightUpCorner = topLimit + transform.right * size.x / 2f;
        leftDownCorner = downLimit - transform.right * size.x / 2f;
        rightDownCorner = downLimit + transform.right * size.x / 2f;

        Debug.DrawLine(leftUpCorner, rightUpCorner, Color.green);
        Debug.DrawLine(rightUpCorner, rightDownCorner, Color.green);
        Debug.DrawLine(rightDownCorner, leftDownCorner, Color.green);
        Debug.DrawLine(leftDownCorner, leftUpCorner, Color.green);
    }

    void OnBallEnter(Ball ball)
    {
        //enteredBalls.Add(ball);
        BounceOffWall(ball);
    }

    void BounceOffWall(Ball ball)
    {
        //ball.velocity = Vector2.Reflect(ball.velocity, transform.up) * bouncess;
        var newVelocity = ball.velocity - 2 * Vector2.Dot(transform.up, ball.velocity) * (Vector2)transform.up; //reflect
        ball.velocity = newVelocity * bouncess;

        if (ball.radius + size.y / 2f > distance)
            ball.transform.position += transform.up * (ball.radius + size.y / 2f - distance);

        Debug.Log("Touch");
    }

    bool IsTouchWall(Ball ball)
    {
        Transform ballTrans = ball.transform;

        Debug.DrawLine(ballTrans.position, leftLimit, Color.red);
        Debug.DrawLine(leftLimit, leftUpCorner, Color.red);

        var toBall = ballTrans.position - leftLimit;
        var main = rightLimit - leftLimit;
        var angle = Vector2.Angle(toBall.normalized, main.normalized);
        var c = (ballTrans.position - leftUpCorner).magnitude;
        distance = Mathf.Sin(angle * Mathf.Deg2Rad) * c;

        if (distance < ball.radius + size.y / 2f && ball.velocity != Vector2.zero)  
            return true;
        else
            return false;
    }
}
