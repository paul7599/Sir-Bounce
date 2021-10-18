using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraMovement : MonoBehaviour
{
    public GameObject heroGO;
    public GameObject scoreGO;
    public string scoreText;
    public float laziness = 1f;
    public int terrainsCreated = 0;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (heroGO.transform.position.y - transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, heroGO.transform.position.y - 5, transform.position.z);

            scoreGO.GetComponent<Text>().text = "Score: " + (int) transform.position.y;
        }
    }
}
