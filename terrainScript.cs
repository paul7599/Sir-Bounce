using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainScript : MonoBehaviour
{

    public GameObject heroGO;
    public GameObject obstaclesGO;
    public GameObject containerGO;
    public GameObject powerupsGO;
    public GameObject powerupContainerGO;
    GameObject newContainer;
    public bool containerCreated = false;

    public cameraMovement cameraMovementGO;

    float minSpace;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y >= 21.6f * cameraMovementGO.terrainsCreated - 10.8f)
        {
            // Create a new set of walls and obstacles
            float cameraPosition = 21.6f * cameraMovementGO.terrainsCreated;
            Instantiate(gameObject, new Vector3(transform.position.x, cameraPosition + 21.6f), transform.rotation);
            newContainer = Instantiate(containerGO, new Vector3(transform.position.x, cameraPosition + 21.6f), transform.rotation);
            containerCreated = true;
            cameraMovementGO.terrainsCreated++;
            // Create a new set of obstacles
            CreateObstacles();

            // Create powerups
            if (Random.Range(0, 100) > 80)
            {
                GeneratePowerups();
            }
        }

        // Destroy the walls and the obstacles left behind
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wallTag");
        GameObject[] containers = GameObject.FindGameObjectsWithTag("containerTag");
        if (walls.Length >= 5)
        {
            Destroy(walls[1]);
            Destroy(containers[1]);
        }
    }

    void CreateObstacles()
    {
        // Rules for obstacle spawn
        int obsPos = Random.Range(0, obstaclesGO.transform.childCount);
        float xPos;
        float yPos = (Random.Range(8, 21.6f) + 10.8f) + (21.6f * (cameraMovementGO.terrainsCreated - 1));

        // Check if the obstacle is an island or a tree branch
        if (obstaclesGO.transform.GetChild(obsPos).CompareTag("islandTag"))
        {
            xPos = Random.Range(-2f, 2f);
        }
        else
        {
            xPos = 5.9f * (Random.Range(0, 99) >= 50 ? 1 : -1);
        }

        // Check obstacle position if it's not the first one to spawn
        if (newContainer.transform.childCount != 0)
        {
            yPos = CheckObstaclesPosition(yPos);
            if (yPos == 0)
            {
                return;
            }
        }

        // Create the obstacle
        GameObject newObstacle = Instantiate(obstaclesGO.transform.GetChild(obsPos).gameObject, new Vector3(xPos, yPos), transform.rotation, newContainer.transform);

        if (xPos >= 0)
        {
            newObstacle.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        CreateObstacles();
    }

    float CheckObstaclesPosition(float yPos)
    {
        int score = (int)Camera.main.transform.position.y;

        if (score < 500)
        {
            minSpace = 7.5f;
        }
        else if (score < 1000)
        {
            minSpace = 7f;
        }
        else if (score < 2000)
        {
            minSpace = 6.5f;
        }
        else if (score < 3000)
        {
            minSpace = 6f;
        }
        else if (score < 5000)
        {
            minSpace = 5.5f;
        }
        else
        {
            minSpace = 4.5f;
        }

        for (int i = 0; i < newContainer.transform.childCount; i++)
        {
            if (yPos + minSpace > newContainer.transform.GetChild(i).position.y && yPos - minSpace < newContainer.transform.GetChild(i).position.y)
            {
                yPos = newContainer.transform.GetChild(i).position.y + minSpace + 0.5f;
                i = -1;

                if (yPos > newContainer.transform.position.y + 10.8f)
                {
                    return 0;
                }
            }
        }
        return yPos;
    }

    void GeneratePowerups()
    {
        // Rules for obstacle spawn
        int powerupPos = Random.Range(0, powerupsGO.transform.childCount);
        float xPos = Random.Range(-4.5f, 4.5f);
        float yPos = (Random.Range(8, 21.6f) + 10.8f) + (21.6f * (cameraMovementGO.terrainsCreated - 1));

        // Create the powerup

        Instantiate(powerupsGO.transform.GetChild(powerupPos).gameObject, new Vector3(xPos, yPos), transform.rotation, powerupContainerGO.transform);
    }
}
