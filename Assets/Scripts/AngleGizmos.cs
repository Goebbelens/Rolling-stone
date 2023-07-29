using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AngleGizmos : MonoBehaviour
{
    public Transform Cube2;
    public Transform AngleCanvas;
    public TextMeshProUGUI textAngle;
    public float Angle;

    private void Start()
    {
        AngleCanvas.gameObject.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        CubGiz(transform.position, Cube2.position, Color.magenta);
    }

    // Update is called once per frame
    void Update()
    {
        Angle = Vector3.Angle(transform.position, Cube2.position);
        textAngle.text = "Angle: " + Angle.ToString();
    }

    private void CubGiz(Vector3 from, Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(from, to);
    }
}
