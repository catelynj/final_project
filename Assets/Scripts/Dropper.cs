using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public static Dropper instance;

    //dropper spawning variables
    public GameObject[] droppers;
    public Transform dropperSpawnPoint;
    private bool isDropperSpawned;
    private bool isFalling = false;
    private int randomDropper;

    //dropper movement variables
    private Rigidbody dropperRigidbody;
    public float h;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed = 1f;
    [SerializeField]
    private float moveForce;




    private Camera mainCamera;


    void Start()
    {
        
        mainCamera = Camera.main;
        SpawnDropper();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        if (speed <= maxSpeed)
        {
            dropperRigidbody.transform.Translate(new Vector3(h, 0, 0) * speed * Time.deltaTime);
            if (dropperRigidbody.velocity.magnitude > maxSpeed)
            {
                dropperRigidbody.velocity = Vector3.ClampMagnitude(dropperRigidbody.velocity, maxSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            DropDropper(); //drop
        }
    }
  
    void SpawnDropper()
    {
        randomDropper = Random.Range(0, droppers.Length - 1);
        
        GameObject newDropper = Instantiate(droppers[randomDropper], dropperSpawnPoint.position, Quaternion.identity);
        dropperRigidbody = newDropper.GetComponent<Rigidbody>();
        dropperRigidbody.useGravity = false;
      
        isDropperSpawned = true;
        isFalling = false;

    }

    void DropDropper()
    {
        if (isDropperSpawned && !isFalling)
        {
            dropperRigidbody.useGravity = true;
            isFalling = true;
            Invoke("SpawnDropper", 1f);
        }
    }

}
