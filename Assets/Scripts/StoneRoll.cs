using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        stone = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<CharacterController>(out CharacterController player))
        {
            Time.timeScale = 0;
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        /*if (Mathf.Abs(direction.normalized.x) > Mathf.Abs(stone.velocity.normalized.x))
        {
            Vector3 xVector = Vector3.zero;
            xVector.x = direction.normalized.x;
            stone.AddForce(xVector * stoneTurnSpeed);
        }
        if (Mathf.Abs(direction.normalized.z) > Mathf.Abs(stone.velocity.normalized.z))
        {
            Vector3 zVector = Vector3.zero;
            zVector.z = direction.normalized.z;
            stone.AddForce(zVector * stoneTurnSpeed);
        }*/
        
        if (Vector3.Distance(transform.position, player.position) > PrevDistance)
        {
            //stone.AddForce(direction.normalized * stoneSpeed * 3);
        }
        stone.AddForce(direction.normalized * stoneSpeed);
        stone.velocity = (stone.velocity.normalized / 4) * stoneSpeed;
        //Debug.Log("RigidBody Magnitude: " + stone.velocity.magnitude);
        Debug.Log("Direction: " + direction);
        /*Debug.Log("Direction norm" + direction.normalized + "\nDirection: " + direction);
        Debug.Log("Velocity: " + stone.velocity);*/
        Debug.Log("VelMagnitude: " + stone.velocity.magnitude);
        Debug.Log("Distance: " + Vector3.Distance(transform.position, player.position) + "\nPrevDistance: " + PrevDistance);
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
