using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceType : MonoBehaviour
{
    [Header("Resource Info")]
    public int quantity;
    public int resourceType;
    public bool isTree;
    public bool isFood;
    public int currentlyCollected;
    public int gatheredResource;

    [Header("Components")]
    [SerializeField] Resources resource;

    private void Start()
    {
        gatheredResource = quantity / 2;
    }

    public void GatherResource(int amount, TeamManager team)
    {
        quantity -= amount;
        //Debug.Log(quantity);
        int amountToGive = amount;

        //To stop the player from getting more than the available resource
        if (quantity < 0)
            amountToGive = amount + quantity;

        if (team.isTeam1)
        {
            //Debug.Log("Team1");
            team.HarvestResource(resourceType,  amountToGive);

            currentlyCollected += amountToGive;
            if (currentlyCollected > gatheredResource)
            {
                //Debug.Log("Currently Collected = " + currentlyCollected);
                GameManager.instance.UpdateResourceGatheredResults(resourceType, 1);
                currentlyCollected = 0;
            }
        }
        else
        {
            //Debug.Log("Team2");
            team.HarvestResource(resourceType, amountToGive);

            currentlyCollected += amountToGive;
            if (currentlyCollected > gatheredResource)
            {
                //Debug.Log("Quantity " + quantity / 2);
                //Debug.Log("Currently Collected = " + currentlyCollected);
                GameManager.instance.UpdateResourceGatheredResults(resourceType, 2);
                currentlyCollected = 0;
            }
        }

        //If the resource has no quantity, then destroy it
        if (quantity <= 0 && resource.resourceList.Contains(this.transform))
        {
            resource.resourceList.Remove(this.transform);
            Destroy(gameObject);
        }

    }

}
