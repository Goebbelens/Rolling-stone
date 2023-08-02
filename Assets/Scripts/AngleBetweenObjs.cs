using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBetweenObjs : MonoBehaviour
{
    public Transform fCube;
    public Transform tCube;
    public float degrees;
    

    void Update()
    {
        Debug.Log("Angle f and t: " + Vector3.Angle(fCube.position, tCube.position));
        Quaternion angle = Quaternion.AngleAxis(degrees, Vector3.up);

        if (Input.GetMouseButtonDown(1) && degrees != 0)
        {
            tCube.position = Vector3.right * 5;
            tCube.position = (angle * tCube.position) + transform.position;
            degrees += 90;
        }
        
        
    }

    private void OnDrawGizmos()
    {
        BallGizmos(transform.position, tCube.position, Color.white);
        BallGizmos(transform.position, fCube.position, Color.black);
        BallGizmos(Vector3.zero, tCube.position, Color.red);
    }

    public void BallGizmos(Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, to);
    }

    public void BallGizmos(Vector3 from, Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(from, to);
    }
}
