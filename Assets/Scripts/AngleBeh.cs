using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class AngleBeh : MonoBehaviour
{
    // Testing angle behavior

    public Transform pointerC;
    public Transform Tcube;
    public Transform ndCube;
    public Transform mndCube;
    public Transform fakeCube;

    private Vector3 nGizAng;
    private Vector3 mnGizAng;
    private Vector3 fGizAng;

    public float nDegrees = 90f;
    public float mnDegrees = 270f;
    public float fDegrees = 45f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointerC.LookAt(new Vector3(Tcube.position.x, pointerC.position.y, Tcube.position.z));
        Quaternion nangle = Quaternion.AngleAxis(nDegrees, Vector3.up);
        Quaternion mnangle = Quaternion.AngleAxis(mnDegrees, Vector3.up);
        Quaternion fangle = Quaternion.AngleAxis(fDegrees, fakeCube.up + fakeCube.position);

        ndCube.position = nangle * Tcube.position;
        nGizAng = nangle * Tcube.position;
        mndCube.position = mnangle * Tcube.position;
        mnGizAng = mnangle * Tcube.position;
        fGizAng = fangle * Tcube.position;
        //fGizAng = Vector3.Cross(Tcube.position - transform.position, transform.up);
        //fakeCube.position = fGizAng;
        //fakeCube.position = transform.position + Vector3.forward * 10;
    }
    private void OnDrawGizmos()
    {
        cGizmos(transform.position, Tcube.position, Color.yellow);
        cGizmos(transform.position, nGizAng, Color.red);
        cGizmos(transform.position, mnGizAng, Color.blue);
        cGizmos(transform.position, fakeCube.position, Color.white);
        cGizmos(fakeCube.position, fakeCube.position + fakeCube.forward * 5, Color.cyan);
        cGizmos(fakeCube.position, fakeCube.position + fakeCube.right * 5, Color.yellow);
        cGizmos(fakeCube.position, Tcube.position - transform.position, Color.magenta);
        cGizmos(fakeCube.position, fakeCube.position + (Tcube.position - transform.position), Color.black);
        cGizmos(fakeCube.position, fGizAng, Color.black);
    }

    private void cGizmos(Vector3 from, Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(from, to);
    }
}
