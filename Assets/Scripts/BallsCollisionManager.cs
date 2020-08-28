using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsCollisionManager : MonoBehaviour
{
    Ball[] balls;
    List<Ball> touchedBalls = new List<Ball>();

    private void Start()
    {
        balls = FindObjectsOfType<Ball>();
    }

    private void FixedUpdate()
    {
        foreach (var ball0 in balls)
        {
            foreach (var ball1 in balls)
            {
                var ballsLayering = Vector2.Distance(ball0.transform.position, ball1.transform.position) - (ball0.radius + ball1.radius);
                if (ballsLayering <= 0 && ball0 != ball1 && !touchedBalls.Contains(ball0))
                {
                    touchedBalls.Add(ball1);

                    var m1 = ball0.mass;
                    var m2 = ball1.mass;
                    var v1 = ball0.velocity;
                    var v2 = ball1.velocity;
                    var x1 = ball0.transform.position;
                    var x2 = ball1.transform.position;
                    var touchToBall0Norm = Mathf.Pow(Mathf.Sqrt(Vector2.Dot(x1 - x2, x1 - x2)), 2);
                    var touchToBall1Norm = Mathf.Pow(Mathf.Sqrt(Vector2.Dot(x2 - x1, x2 - x1)), 2);

                    ball0.transform.position -= (x1 - x2).normalized * (ballsLayering / 2f);
                    ball1.transform.position -= (x2 - x1).normalized * (ballsLayering / 2f);

                    ball0.velocity -= ((2 * m2) / (m1 + m2)) * (Vector2.Dot(v1 - v2, x1 - x2) / touchToBall0Norm) * (Vector2)(x1 - x2);
                    ball1.velocity -= ((2 * m1) / (m1 + m2)) * (Vector2.Dot(v2 - v1, x2 - x1) / touchToBall1Norm) * (Vector2)(x2 - x1);
                    Debug.Log("TouchBall");
                }
            }
        }
        touchedBalls = new List<Ball>();
    }

}
