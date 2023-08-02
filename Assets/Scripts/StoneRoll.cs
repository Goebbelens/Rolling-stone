using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private float stoneStopTime;
    public float timeBeforeMove = 1f;
    private Vector3 zeroYpointer;
    
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
        Pointer.rotation = new Quaternion (0, Pointer.rotation.y, 0 , Pointer.rotation.w);
        zeroYpointer = Pointer.position - new Vector3(0f, Pointer.position.y, 0f);
        TXTStoneAngle.text = "Angle: " + Vector3.Angle(stone.velocity.normalized, direction.normalized).ToString();
    }
    void FixedUpdate()
    {
        direction = player.position - transform.position;
        direction.y /= 2;


        if (isMoving && prevDistance - minDistance >= 5f)
        {
            isMoving = false;
            
            stoneStopTime = Time.time;
        }
        if (!isMoving)
        {
            stone.AddForce(-stone.velocity * 2);
            
            if (Time.time - stoneStopTime >= timeBeforeMove)
            {
                isMoving = true;
                minDistance = prevDistance;
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

        Debug.Log("Right ang: " + Vector3.Angle(zeroYpointer + Pointer.right - new Vector3(0f, Pointer.position.y, 0f), 
            stone.velocity - new Vector3 (0, stone.velocity.y, 0)) + 
            "\nLeft ang: 0" + Vector3.Angle(Vector3.zero - Pointer.right - new Vector3(0f, Pointer.position.y, 0f), stone.velocity - new Vector3(0, stone.velocity.y, 0)));
    }
    private void OnDrawGizmos()
    {
        BallGizmos(zeroYpointer, stone.velocity - new Vector3(0, stone.velocity.y, 0), Color.yellow);
        BallGizmos(player.position - new Vector3 (0f,player.position.y, 0), Color.blue);

        BallGizmos(zeroYpointer, zeroYpointer + Pointer.right * 20, Color.red);
        BallGizmos(zeroYpointer, zeroYpointer - Pointer.right * 20, new Color(1f, 0.5f, 0, 1));
        //BallGizmos(stone.velocity, player.position, Color.red);
        //BallGizmos(Vector3.Distance(stone.velocity, player.position) * Pointer.right, Color.red);
        BallGizmos(zeroYpointer, zeroYpointer + Pointer.forward * 100, Color.black);
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
