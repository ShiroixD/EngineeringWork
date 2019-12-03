using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ItemsCollision : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 startingRot;
    private ShapesCubeController ShapesCubeController;

    private void Awake()
    {
        startingPos = transform.position;
        startingRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        ShapesCubeController = GameObject.FindWithTag("GameCubeController").GetComponent<ShapesCubeController>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObjectProperties colProps = collision.collider.gameObject.GetComponent<GameObjectProperties>();
        GameObjectProperties myProps = gameObject.GetComponent<GameObjectProperties>();

        if (collision.collider.tag == "Ground")
        {
            if (!myProps.ShouldDisappear)
            {
                transform.position = startingPos;
                transform.rotation = new Quaternion(startingRot.x, startingRot.y, startingRot.z, 1.0f);
            }
            else
            {
                gameObject.SetActive(false);
            }
            return;
        }

        if (myProps.Type == ObjectType.ShapesCubeItem)
        {
            if (colProps.Name == myProps.Name && colProps.Type == ObjectType.Item)
            {
                InteractionBehaviour behaviour = collision.collider.gameObject.GetComponent<InteractionBehaviour>();
                behaviour.ignoreGrasping = true;
                behaviour.ignoreContact = true;
                behaviour.ignorePrimaryHover = true;
                collision.collider.gameObject.SetActive(false);
                colProps.ShouldDisappear = true;
                ShapesCubeController.DecreaseShapesCubeParts();
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
