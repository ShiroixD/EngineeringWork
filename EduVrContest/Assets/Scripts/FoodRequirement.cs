using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRequirement
{
    public string name;
    public int requiredAmount;
    public int currentAmount;

    public FoodRequirement(string n, int req)
    {
        name = n;
        requiredAmount = req;
        currentAmount = 0;
    }
}
