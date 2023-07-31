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
    Vector3 direction;
    bool isMoving = true;
    float stoneStopTime;
    public float timeBeforeMove = 1f;
    Vector3 nineAngle;

    void Start()
    {
        stone = GetComponent<Rigidbody>();
        TXTStoneAngle = AngleCanvas.GetComponent<TextMeshProUGUI>();
        minDistance = Vector3.Distance(transform.position, player.position);
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
        TXTStoneAngle.text = "Angle: " + Vector3.Angle(stone.velocity.normalized, direction.normalized).ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }
    void FixedUpdate()
    {
        direction = player.position - transform.position;
        direction.y /= 2;


        if(isMoving && prevDistance - minDistance >= 5f)
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
                stone.AddForce((player.position - stone.velocity));
            }
        }

        if (minDistance > Vector3.Distance(transform.position, player.position))
        {
            minDistance = Vector3.Distance(transform.position, player.position);
        }
        prevDistance = Vector3.Distance(transform.position, player.position);
        Quaternion anglen = Quaternion.AngleAxis(90, Vector3.up);
        //
        //
        //
        //
        //nineAngle = Vector3.forward * anglen;
    }
    private void OnDrawGizmos()
    {
        BallGizmos(stone.velocity, Color.yellow);
        BallGizmos(player.position, Color.blue);
        BallGizmos(nineAngle, new Color(0.2f, 0.5f, 0.1f));
    }

    private void BallGizmos(Vector3 to, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, to);
    }
    
}
