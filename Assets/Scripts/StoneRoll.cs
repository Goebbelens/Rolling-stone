using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneRoll : MonoBehaviour
{
    [Header ("References")]
    private Rigidbody stone;
    public Transform Pointer;
    public Transform player;

    [Header ("Movement")]
    public float stoneSpeed = 1f;
    public float stoneTurnSpeed = 1f;
    public float rotationSpeedChange = 1f;
    private float minDistance;
    private float prevDistance;

    [Header("Temporary")]
    public Transform AngleCanvas;
    public float allowedAngle = 5f;
    public TextMeshProUGUI TXTStoneAngle;
    private Vector3 direction;
    private bool isMoving = true;
    public float allowedDistance = 5f;
    private float stoneStopTime;
    public float velocityDecrease = 2f;
    public float timeBeforeMove = 1f;
    private Vector3 zeroYpointer;
    
    public float tvDegrees;
    public float nvDegrees;
    public float mvDegrees;

    public Transform nSphere;
    public Transform mSphere;

    public Quaternion nAngle;
    public Quaternion mnAngle;

    [Range(5f, 20f)]
    private float arrowLength = 10f;
    //Vector3 nineAngle;
    //Vector3 tineAngle;

    void Start()
    {
        stone = GetComponent<Rigidbody>();
        TXTStoneAngle = AngleCanvas.GetComponent<TextMeshProUGUI>();
        minDistance = Vector3.Distance(transform.position, player.position);
        zeroYpointer = Pointer.position - new Vector3(0f, Pointer.position.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<CharacterController>(out CharacterController player))
        {
            Time.timeScale = 0;
        }
    }
    private void Update()
    {
        Pointer.LookAt(player.position);
        Vector3 pointZzero = Pointer.eulerAngles;
        pointZzero = new Vector3 (0f, pointZzero.y, 0f);
        Pointer.eulerAngles = pointZzero;
        zeroYpointer = Pointer.position - new Vector3(0f, Pointer.position.y, 0f);
        TXTStoneAngle.text = "Angle: " + Vector3.Angle(stone.velocity.normalized, direction.normalized).ToString();

        nAngle = Quaternion.AngleAxis(90 + Pointer.eulerAngles.y, Vector3.up);
        mnAngle = Quaternion.AngleAxis(270 + Pointer.eulerAngles.y, Vector3.up);
        nSphere.position = nAngle * Vector3.forward * 5 + Pointer.position;
        mSphere.position = mnAngle * Vector3.forward * 5 + Pointer.position;
    }
    void FixedUpdate()
    {
        direction = player.position - transform.position;
        direction.y /= 2;




        if (isMoving && prevDistance - minDistance >= allowedDistance)
        {
            isMoving = false;
            
            stoneStopTime = Time.time;
        }
        if (!isMoving)
        {
            stone.AddForce(-stone.velocity * velocityDecrease);
            
            if (Time.time - stoneStopTime >= timeBeforeMove)
            {
                isMoving = true;
                minDistance = prevDistance;
                stone.velocity = Vector3.zero;
            }
        }
        if (isMoving)
        {
            stone.AddForce(direction.normalized * stoneSpeed);
            if(Vector3.Angle(stone.velocity.normalized, direction.normalized) > allowedAngle)
            {
                //stone.AddForce((player.position - stone.velocity));
            }
        }

        if (minDistance > Vector3.Distance(transform.position, player.position))
        {
            minDistance = Vector3.Distance(transform.position, player.position);
        }
        prevDistance = Vector3.Distance(transform.position, player.position);



    }
    private void OnDrawGizmos()
    {
        BallGizmos(player.position, Color.blue);
        BallGizmos(Pointer.position, Pointer.forward * 10 + Pointer.position, new Color(0.5f, 0.5f, 1f, 1f));

        if (!Application.isPlaying)
        {
            return;
        }

        Vector3 position = transform.position;
        Vector3 drawVelocity = stone.velocity;
        BallGizmos(stone.velocity, Color.yellow);
        BallGizmos(Quaternion.LookRotation(stone.velocity).eulerAngles, new Color(1f,0f,1f,1f));

        BallGizmos(Pointer.position, nAngle * Vector3.forward * 5 + Pointer.position, Color.cyan);
        BallGizmos(Pointer.position, mnAngle * Vector3.forward * 5 + Pointer.position, new Color(1f, 0.5f, 0f, 1f));
        

        if (stone.velocity.magnitude < 0.1f)
        {
            return;
        }

        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(drawVelocity), arrowLength, EventType.Repaint);


        //BallGizmos(stone.velocity, player.position, Color.red);
        //BallGizmos(Vector3.Distance(stone.velocity, player.position) * Pointer.right, Color.red);
        //BallGizmos(nineAngle, new Color(0.2f, 0.5f, 0.1f));
        //BallGizmos(tineAngle, new Color(0.5f, 0.5f, 0.1f));
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
