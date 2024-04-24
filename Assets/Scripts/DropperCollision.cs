using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropperCollision : MonoBehaviour
{
    private Rigidbody dropperRB;

    public static DropperCollision instance;
   
    //dropper objects -- smallest (red) to biggest (purple)
    public GameObject redDropper;
    public GameObject orangeDropper; //default dropper   
    public GameObject yellowDropper;
    public GameObject greenDropper;
    public GameObject blueDropper;
    public GameObject purpleDropper;
    
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
            //Orange -> Yellow
            if (collision.gameObject.name == gameObject.name && gameObject.name == "OrangeDropper(Clone)")
            {
                
                Merge(yellowDropper);
                GameManager.instance.UpdateScore(50);
                Destroy(collision.gameObject);
            }
            //Red -> Orange
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "RedDropper(Clone)")
            {
                
                Merge(orangeDropper);
                GameManager.instance.UpdateScore(25);
                Destroy(collision.gameObject);
            }
            //Yellow -> Green
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "YellowDropper(Clone)")
            {
                
                Merge(greenDropper);
                GameManager.instance.UpdateScore(75);
                Destroy(collision.gameObject);
            }
            //Green -> Blue
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "GreenDropper(Clone)")
            {
               
                Merge(blueDropper);
                GameManager.instance.UpdateScore(100);
                Destroy(collision.gameObject);
            }
            //Blue -> Purple (final size)
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "BlueDropper(Clone)")
            {
                
                Merge(purpleDropper);
                GameManager.instance.UpdateScore(200);
                Destroy(collision.gameObject);
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
