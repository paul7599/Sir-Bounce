using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroScript : MonoBehaviour
{
    public GameObject heroGO;
    Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = heroGO.transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("heroTag"))
        {
            float heroMagnitude = rb.velocity.magnitude;
            if (heroMagnitude == 0)
            {
                heroMagnitude = 1;
            }
            rb.velocity = new Vector2(rb.velocity.x / (heroMagnitude * 2), 1) * 25;

            anim.Play("New Animation2", 0, 0);
        }
    }
}
