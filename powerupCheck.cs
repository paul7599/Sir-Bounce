using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupCheck : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle") || collision.gameObject.CompareTag("islandTag"))
        {
            Destroy(transform.gameObject);
        }
    }
}
