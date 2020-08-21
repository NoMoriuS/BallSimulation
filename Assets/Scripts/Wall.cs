using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Wall : MonoBehaviour
{
    public Vector2 size;
    public Vector3 leftUpCorner;
    public Vector3 rightUpCorner;
    public Vector3 leftDownCorner;
    public Vector3 rightDownCorner;

    private void Update()
    {
        leftUpCorner = transform.position + transform.up * size.y / 2f - transform.right * size.x / 2f;
        rightUpCorner = transform.position + transform.up * size.y / 2f + transform.right * size.x / 2f;
        leftDownCorner = transform.position - (transform.up * size.y / 2f) - transform.right * size.x / 2f;
        rightDownCorner = transform.position - transform.up * size.y / 2f + transform.right * size.x / 2f;

        Debug.DrawLine(leftUpCorner, rightUpCorner, Color.green);
        Debug.DrawLine(rightUpCorner, rightDownCorner, Color.green);
        Debug.DrawLine(rightDownCorner, leftDownCorner, Color.green);
        Debug.DrawLine(leftDownCorner, leftUpCorner, Color.green);
    }
}
