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
        CauldronItem item = collision.collider.gameObject.GetComponent<CauldronItem>();
        string collFoodNameStr = Enum.GetName(typeof(FoodName), 
            collision.collider.gameObject.GetComponent<CauldronItem>().FoodName);
        FoodRequirement foodReq = WorldController.GetCurrentRequirement();
        if (collFoodNameStr.Equals(foodReq.name))
        {
            WorldController.UpdateCurrentRequirement();
            return;
        }
        List<string> foodNames = new List<string>(WorldController.FoodNames);
        if (foodNames.Contains(collFoodNameStr))
        {
            WorldController.ResetRequirements();
        }
    }
}
