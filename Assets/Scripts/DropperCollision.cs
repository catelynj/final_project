using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropperCollision : MonoBehaviour
{
    private Rigidbody dropperRB;

    public static DropperCollision instance;
    //need to change these to colors or items later bc this is confusing
    public GameObject dropper;
    public GameObject bigDropper;
    public GameObject extrabigDropper;
    public GameObject hugeDropper;
    public GameObject smallDropper;
    public bool isActiveDropper = true;

    private void Start()
    {
        dropperRB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //check if active dropper
        if (!isActiveDropper)
            return; 

        //if dropper collides with boundary...
        if (collision.gameObject.CompareTag("boundary"))
        {
            //Debug.Log(gameObject.name);

            //adjusting velocity to limit random movement
            dropperRB.velocity = Vector3.zero;
            dropperRB.angularVelocity = Vector3.zero;
            

            isActiveDropper = false;

        }

        if (isActiveDropper) //if active -- hasn't collided with boundary yet
        {
            if (collision.gameObject.name == gameObject.name && gameObject.name == "ExtraBigDropper(Clone)") //two "ExtraBigDropper" collide
            {
                Debug.Log("Dropper Collision");
                Merge(hugeDropper); //make hugedropper
                GameManager.instance.UpdateScore(100); //update score
                Destroy(collision.gameObject); //destroy dropper
            }
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "BigDropper(Clone)") //two "BigDropper" collide
            {
                Debug.Log("Dropper Collision");
                Merge(extrabigDropper); //make extrabigdropper
                GameManager.instance.UpdateScore(100); //update score
                Destroy(collision.gameObject); //destroy dropper
            }
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "MediumDropper(Clone)") //two "MediumDropper" collide
            {
                Debug.Log("Dropper Collision");
                Merge(bigDropper); //make bigdropper
                GameManager.instance.UpdateScore(50); //update score
                Destroy(collision.gameObject); //destroy dropper
            }
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "SmallDropper(Clone)") //two "SmallDropper" collide
            {
                Debug.Log("Small Dropper Collision");
                Merge(dropper); //make medium dropper
                GameManager.instance.UpdateScore(25); //update score
                Destroy(collision.gameObject); //destroy dropper
            }
        }

    }
    

    private void Merge(GameObject mergedDropperPrefab)
    {
        Debug.Log("Merge.");

        Vector3 mergeOffset = new Vector3(0.5f, 0.5f, 0); //offset to avoid collisions on merge
        Vector3 newPosition = transform.position + mergeOffset;
        GameObject mergedDropper = Instantiate(mergedDropperPrefab, newPosition, Quaternion.identity);

        mergedDropper.GetComponent<DropperCollision>().isActiveDropper = false;

        Destroy(gameObject); //destroy other dropper

    }

    private void OnTriggerStay(Collider other)
    {
        //TODO:
        //when dropper stays in TopBoundary -- end game
    }
}
