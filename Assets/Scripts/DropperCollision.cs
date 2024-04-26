using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Catelyn J. | April 2024 | Collision Script for Dropper Objects -- handles all merging and checks for lose condition
public class DropperCollision : MonoBehaviour
{
    private Rigidbody dropperRB;

    public static DropperCollision instance;

    //dropper objects -- smallest (red) to biggest (purple)
    public GameObject redDropper;
    public GameObject orangeDropper;
    public GameObject yellowDropper;
    public GameObject greenDropper;
    public GameObject blueDropper;
    public GameObject purpleDropper;

    public bool isActiveDropper = true;

    private bool occupied = false;
    private bool spawnCalled = false;


    private void Start()
    {
        dropperRB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if dropper collides with boundary...
        if (collision.gameObject.CompareTag("boundary"))
        {
            spawnCalled = true;
            isActiveDropper = false;
            return;
        }

        if (isActiveDropper)
        {
            //Orange -> Yellow
            if (collision.gameObject.name == gameObject.name && gameObject.name == "OrangeDropper(Clone)")
            {

                GameManager.Instance.pop.Play();
                Merge(yellowDropper);
                UIManager.Instance.UpdateScore(50);
                Destroy(collision.gameObject);
                //Debug.Log("returned");
                return;

            }
            //Red -> Orange
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "RedDropper(Clone)")
            {

                GameManager.Instance.pop.Play();
                Merge(orangeDropper);
                UIManager.Instance.UpdateScore(25);
                Destroy(collision.gameObject);
                //Debug.Log("returned");
                return;
            }
            //Yellow -> Green
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "YellowDropper(Clone)")
            {

                GameManager.Instance.pop.Play();
                Merge(greenDropper);
                UIManager.Instance.UpdateScore(75);
                Destroy(collision.gameObject);
                //Debug.Log("returned");
                return;
            }
            //Green -> Blue
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "GreenDropper(Clone)")
            {

                GameManager.Instance.pop.Play();
                Merge(blueDropper);
                UIManager.Instance.UpdateScore(100);
                Destroy(collision.gameObject);
                //Debug.Log("returned");
                return;
            }
            //Blue -> Purple (final size)
            else if (collision.gameObject.name == gameObject.name && gameObject.name == "BlueDropper(Clone)")
            {

                GameManager.Instance.pop.Play();
                Merge(purpleDropper);
                UIManager.Instance.UpdateScore(200);
                Destroy(collision.gameObject);
                //Debug.Log("returned");
                return;
            }
        } 
       
    }

    private void Merge(GameObject mergedDropperPrefab)
    {
        Debug.Log("Merge.");

        Vector3 mergeOffset = new Vector2(0.5f, 0.5f); // Offset to avoid collisions on merge
        Vector3 newPosition = transform.position + mergeOffset;

        GameObject mergedDropper = Instantiate(mergedDropperPrefab, newPosition, Quaternion.identity);

        //set merged dropper as inactive
        mergedDropper.GetComponent<DropperCollision>().isActiveDropper = false;
        Destroy(gameObject); // Destroy other dropper
    }
    private void OnTriggerStay(Collider other)
    {
        //TODO:
        //when dropper stays in TopBoundary -- end game

        occupied = false;

        if (other.CompareTag("top_boundary") && !isActiveDropper)
        {
            occupied = true;
            StartCoroutine("InitiateGameOver");
        }
    }

    private IEnumerator InitiateGameOver()
    {
        occupied = false;
        spawnCalled = false;

        yield return new WaitForSeconds(1f);

        UIManager.Instance.EnableGameOverCanvas();
        Debug.Log("GAME OVER GO");

    }

}
