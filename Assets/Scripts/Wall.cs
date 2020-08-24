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
    Vector3 leftUpCorner;
    Vector3 rightUpCorner;
    Vector3 leftDownCorner;
    Vector3 rightDownCorner;
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
            //    if (!IsTouchWall(item))
            //        enteredBalls.Remove(item);
            //}
        }
    }

    private void Update()
    {
        topLimit = transform.position + transform.up * size.y / 2f;
        downLimit = transform.position - transform.up * size.y / 2f;

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
        enteredBalls.Add(ball);
        BounceOffWall(ball);
    }

    void BounceOffWall(Ball ball)
    {
        Debug.DrawLine(ball.transform.position, ball.transform.position + transform.up, Color.green, 100000);
        //ball.velocity = Vector2.Reflect(ball.velocity, transform.right) * bouncess;
        var newVelocity = ball.velocity - 2 * transform.up * ball.velocity * transform.up; //reflect
        ball.velocity = newVelocity * bouncess;
        if (ball.radius + size.y / 2f > ball.transform.position.y) ball.transform.position = new Vector2(ball.transform.position.x, transform.position.y + ball.radius + size.y / 2f);
    }

    bool IsTouchWall(Ball ball)
    {
        Transform ballTrans = ball.transform;

        //Debug.DrawLine(topLimit, ballTrans.position + (ball.radius + size.x / 2f) * transform.right);
        //Debug.DrawLine(downLimit, ballTrans.position + (ball.radius + size.x / 2f) * transform.right);

        if (ballTrans.position.y < transform.position.y + ball.radius + size.y /2f)
            return true;
        else
            return false;
    }
}
