using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotateLocal : MonoBehaviour
{
    
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
}
