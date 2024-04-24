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
    //private float moveForce;
    private Transform dropperTransform;
    private Camera mainCamera;


    void Start()
    {
        
        mainCamera = Camera.main;
        SpawnDropper();
    }

    void Update()
    {

        //movement
        h = Input.GetAxis("Horizontal");
        if (speed <= maxSpeed)
        {
            
            dropperRigidbody.transform.Translate(new Vector3(h, 0, 0) * speed * Time.deltaTime);
            //clamp velocity to stop objects from going through boundary
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
        
        //get random number between 0-length of dropper array and instantiate random dropper at spawn point
        GameObject newDropper = Instantiate(droppers[randomDropper], dropperSpawnPoint.position, Quaternion.identity);

        //get rigidbody and disable gravity until user drops gameobject
        dropperRigidbody = newDropper.GetComponent<Rigidbody>();
        dropperRigidbody.useGravity = false;
        dropperTransform = newDropper.GetComponent<Transform>();
        //clamp Z position of dropper transform!!!!!
        isDropperSpawned = true;
        isFalling = false;

    }

    void DropDropper()
    {
        if (isDropperSpawned && !isFalling)
        {
            //enable gravity on rb and invoke spawn method
            dropperRigidbody.useGravity = true;
            isFalling = true;
            Invoke("SpawnDropper", 1f);
        }
    }

}
