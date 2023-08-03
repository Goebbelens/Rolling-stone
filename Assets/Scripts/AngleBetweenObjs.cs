using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AngleBetweenObjs : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform fCube;
    [SerializeField]
    private Transform nCube;
    [SerializeField]
    private Transform mnCube;
    [SerializeField]
    private Transform tCube;

    [SerializeField]
    private float degrees;

    [SerializeField]
    private float betNangle;
    [SerializeField]
    private float betMNangle;

    //[SerializeField]
    //[SerializeField]


    void Update()
    {
        //Debug.Log("Angle f and t: " + Vector3.Angle(fCube.position, tCube.position));
        transform.LookAt(tCube);
        Transform currCubeGlobal = transform;
        Quaternion nAngle = Quaternion.AngleAxis(degrees + currCubeGlobal.eulerAngles.y, Vector3.up);
        Quaternion mnAngle = Quaternion.AngleAxis(degrees + 180 + currCubeGlobal.eulerAngles.y, Vector3.up);
        Vector3 forwardCubeGlobal = Vector3.forward * 5;

        if (Input.GetMouseButtonDown(1) && degrees != 0)
        {
            //Debug.Log(degrees + currCubeGlobal.eulerAngles.y);
            
        }

        nCube.position = (nAngle * forwardCubeGlobal) + transform.position;
        nCube.rotation = transform.rotation;
        mnCube.position = (mnAngle * forwardCubeGlobal) + transform.position;
        mnCube.rotation = transform.rotation;


        betNangle = Vector3.Angle(tCube.position - transform.position, transform.position - nCube.position);
        betMNangle = Vector3.Angle(tCube.position - transform.position, transform.position - mnCube.position);

    }

    private void OnDrawGizmos()
    {
        BallGizmos(transform.position, nCube.position, Color.white);
        BallGizmos(transform.position, mnCube.position, Color.white);
        BallGizmos(transform.position, tCube.position, Color.black);
        BallGizmos(Vector3.zero, -(transform.position - nCube.position), Color.blue);
        BallGizmos(Vector3.zero, -(transform.position - mnCube.position), Color.red);
        BallGizmos(Vector3.zero, tCube.position - transform.position, Color.cyan);
        //BallGizmos(Vector3.zero, nCube.position, Color.red);
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
