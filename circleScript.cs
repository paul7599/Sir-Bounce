using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleScript : MonoBehaviour
{
    public GameObject heroGO;
    public GameObject circleGO;
    // Start is called before the first frame update
    void Start()
    {
        heroGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        circleGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {    
        if (Input.GetMouseButtonDown(0))
        {
            heroGO.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            if (circleGO != null)
            {
                circleGO.SetActive(true);
                Vector3 mousePos = Input.mousePosition;
                Vector3 circlePos = Camera.main.ScreenToWorldPoint(mousePos);
                circlePos.z = 0;
                circleGO.transform.position = circlePos;
            }
        }
    }
}
