using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleToVector3 : MonoBehaviour
{
    public Transform subCube;
    public float Degrees = 45f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = subCube.position - transform.position;
        Quaternion cRotation = Quaternion.AngleAxis(Degrees, Vector3.right);
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            subCube.position = cRotation * subCube.position;
        }
    }
}
