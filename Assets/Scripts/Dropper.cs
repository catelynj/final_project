using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject dropperPrefab;
    public Transform dropperSpawnPoint;
    private bool isDropperSpawned;
    private bool isFalling = false;
    private Rigidbody dropperRigidbody;
    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
        SpawnDropper();
    }

    void Update()
    {
        if (isDropperSpawned && !isFalling)
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z));
            dropperRigidbody.MovePosition(new Vector3(newPosition.x, dropperSpawnPoint.position.y, dropperSpawnPoint.position.z));
        }
    }

    void SpawnDropper()
    {
        GameObject newDropper = Instantiate(dropperPrefab, dropperSpawnPoint.position, Quaternion.identity);
        dropperRigidbody = newDropper.GetComponent<Rigidbody>();
        dropperRigidbody.useGravity = false;
        isDropperSpawned = true;
        isFalling = false;
    }

    void OnMouseDown()
    {
        if (isDropperSpawned & !isFalling)
        {
            dropperRigidbody.useGravity = true;
            isFalling = true;

            Invoke("SpawnDropper", 1f);
        }
    }

}
