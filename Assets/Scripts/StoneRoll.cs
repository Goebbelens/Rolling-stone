using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private float PrevDistance;

    [Header("Temporary")]
    public Transform AngleCanvas;
    private float stoneAngle;
    public TextMeshProUGUI TXTStoneAngle;
    Vector3 direction;
    bool isMoving = true;

    void Start()
    {
        stone = GetComponent<Rigidbody>();
        TXTStoneAngle = AngleCanvas.GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<CharacterController>(out CharacterController player))
        {
            //Time.timeScale = 0;
        }
    }
    private void Update()
    {
        TXTStoneAngle.text = "Angle: " + Vector3.Angle(stone.velocity.normalized, direction.normalized).ToString();
    }
    void FixedUpdate()
    {
        direction = player.position - transform.position;
        direction.y /= 2;
        
        /*if(Vector3.Distance(transform.position, player.position) > PrevDistance)
        {
            stone.AddForce((-stone.velocity.normalized / 4) * stoneSpeed);
        }*/
        if(stoneAngle > 45)
        {
            //isMoving = false;
        }
        if (!isMoving)
        {
            
        }
        if (isMoving)
        {
            stone.AddForce(direction.normalized * stoneSpeed);
        }
        
        //stone.velocity = (stone.velocity.normalized / 2) * stoneSpeed;

        //Debug.Log("RigidBody Magnitude: " + stone.velocity.magnitude);
        //Debug.Log("Direction: " + direction);
        /*Debug.Log("Direction norm" + direction.normalized + "\nDirection: " + direction);
        Debug.Log("Velocity: " + stone.velocity);*/
        //Debug.Log("VelMagnitude: " + stone.velocity.magnitude);
        //Debug.Log("Distance: " + Vector3.Distance(transform.position, player.position) + "\nPrevDistance: " + PrevDistance);
        PrevDistance = Vector3.Distance(transform.position, player.position);
    }
    private void OnDrawGizmos()
    {
        BallGizmos(stone.velocity, Color.yellow);
        BallGizmos(player.position, Color.blue);
    }

    private void BallGizmos(Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, to);
    }
}
