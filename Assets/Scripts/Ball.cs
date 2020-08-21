using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 velocity;
    public float mouseImpulsePower;
    public float r;
    float speed;
    Wall[] walls;

    private void Start()
    {
        walls = FindObjectsOfType<Wall>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, transform.position + new Vector3(velocity.x, velocity.y, 0), 1 * Time.fixedDeltaTime);
        foreach (var item in walls)
        {
            if (IsTouchWall(item))
            {
                BounceOffWall(item);
                return;
            }
        }
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

    void BounceOffWall(Wall wall)
    {
        Debug.DrawLine(transform.position, transform.position + wall.transform.right, Color.green, 100);
        velocity = Vector2.Reflect(velocity, wall.transform.right);
        var cornersToPlayerDist = Vector2.Distance(wall.leftDownCorner, transform.position + r * wall.transform.right) + Vector2.Distance(wall.leftUpCorner, transform.position + r * wall.transform.right);
        Debug.Log(cornersToPlayerDist);
    }

    bool IsTouchWall(Wall wall) 
    {
        Transform wallTrans = wall.transform;

        Debug.DrawLine(wall.leftDownCorner, transform.position + r * wallTrans.right);
        Debug.DrawLine(wall.leftUpCorner, transform.position + r * wallTrans.right);

        var cornersToPlayerDist = Vector2.Distance(wall.leftDownCorner, transform.position + r * wallTrans.right) + Vector2.Distance(wall.leftUpCorner, transform.position + r * wallTrans.right);

        if (Mathf.Abs(wall.size.y - cornersToPlayerDist) < 0.02f)
            return true;
        else
            return false;
    }
}
