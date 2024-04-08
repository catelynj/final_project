using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropperCollision : MonoBehaviour
{

    private Rigidbody dropperRB;
    public static DropperCollision instance;
   
    public GameObject dropper;
    public GameObject bigDropper;
    public GameObject smallDropper;
    public bool isActiveDropper = true;
    private void Start()
    {
        dropperRB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (!isActiveDropper)
            return; 

        if (collision.gameObject.CompareTag("boundary"))
        {
            Debug.Log(gameObject.name);
            dropperRB.velocity = Vector3.zero;
            dropperRB.angularVelocity = Vector3.zero;
            isActiveDropper = false;

        }

        if (isActiveDropper)
        {
            if (collision.gameObject.name == gameObject.name && gameObject.name == "Dropper(Clone)")
            {
                Debug.Log("Dropper Collision");
                Merge(bigDropper);
                GameManager.instance.UpdateScore(50);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "SmallDropper(Clone)")
            {
                Debug.Log("Small Dropper Collision");
                Merge(dropper);
                GameManager.instance.UpdateScore(25);
                Destroy(collision.gameObject);
            }
        }

    }

    private void Merge(GameObject mergedDropperPrefab)
    {
        Debug.Log("Merge.");

        Vector3 mergeOffset = new Vector3(0.5f, 0.5f, 0);
        Vector3 newPosition = transform.position + mergeOffset;
        GameObject mergedDropper = Instantiate(mergedDropperPrefab, newPosition, Quaternion.identity);

        mergedDropper.GetComponent<DropperCollision>().isActiveDropper = false;

        Destroy(gameObject);

    }

    private void OnTriggerStay(Collider other)
    {
        //TODO:
        //when dropper stays in TopBoundary -- end game
    }
}
