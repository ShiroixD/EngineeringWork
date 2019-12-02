using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollision : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 startingRot;

    private void Awake()
    {
        startingPos = transform.position;
        startingRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Items")
        {
            collision.collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        } 
        else if (collision.collider.tag == "Ground")
            {
                transform.position = startingPos;
                transform.rotation = new Quaternion(startingRot.x, startingRot.y, startingRot.z, 1.0f);
            }
    }
}
