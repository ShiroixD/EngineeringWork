using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronController : MonoBehaviour
{
    public TavernWorldController WorldController;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Item"))
        {
            CauldronItem item = collision.collider.gameObject.GetComponent<CauldronItem>();
            string collFoodNameStr = collision.collider.gameObject.GetComponent<CauldronItem>().FoodName.ToString();
            FoodRequirement foodReq = WorldController.GetCurrentRequirement();
            //Debug.Log("My cauldron tag: " + gameObject.tag);
            //Debug.Log("Item tag: " + collision.collider.tag);
            //Debug.Log("Item name: " + collFoodNameStr);
            if (collFoodNameStr.Equals(foodReq.name))
            {
                WorldController.UpdateCurrentRequirement();
                return;
            }
            else
            {
                WorldController.ResetRequirements();
            }
        }
    }
}
