using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class powerUpScript : MonoBehaviour
{
    public GameObject circleGO;
    public GameObject CaneGO;
    public GameObject TimeGO;
    public GameObject TimeMushroom;
    public GameObject powerupContainerGO;
    public GameObject CanvasCaneGO;
    public GameObject GameOverGO;
    
    Rigidbody2D rb;
    //bool biggerPowerup = false;
    //bool slowmoPowerup = false;
    //bool reversegPowerup = false;
    bool transparencyPowerup = false;
    bool canePowerup = false;
    bool firstDisable = true;
    bool sirOverlapped = false;

    public terrainScript terrainScriptGO;

    // Start is called before the first frame update
    void Start()
    {
        GameOverGO.SetActive(false);
        Time.timeScale = 1;
        TimeGO.SetActive(false);
        TimeMushroom.SetActive(false);
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Game Over

        if (transform.position.y < Camera.main.transform.position.y - 12)
        {
            GameOverGO.SetActive(true);
            if ((int) Camera.main.transform.position.y > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", (int) Camera.main.transform.position.y);
                GameOverGO.transform.GetChild(0).GetComponent<Text>().text = "New High Score: " + PlayerPrefs.GetInt("HighScore");
            }
            else
            {
                GameOverGO.transform.GetChild(0).GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore");
            }
            
            Time.timeScale = 0;
        }


        if (powerupContainerGO.transform.childCount >= 5)
        {
            Destroy(powerupContainerGO.transform.GetChild(0).gameObject);
        }

        if (transparencyPowerup && terrainScriptGO.containerCreated)
        {
            terrainScriptGO.containerCreated = false;
            TransparencyFunc();
        }

        if (canePowerup)
        {
            for (int i = 0; i < CanvasCaneGO.transform.childCount; i++)
            {
                CanvasCaneGO.transform.GetChild(i).position = new Vector3(0, Camera.main.transform.position.y - 9 + i);
            }
        }
    }

    private void LateUpdate()
    {
        // Check to see if the animation has finished

        if (TimeGO.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {           
            circleGO.transform.localScale = new Vector3(0.7f, 0.2381f);            
            Time.timeScale = 1;
            rb.gravityScale = 1;
        }

        if (TimeMushroom.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            transparencyPowerup = false;

            // call transparencyFunc() only 1 time
            if (firstDisable)
            {
                if (!sirOverlapped)
                {
                    firstDisable = false;
                    TransparencyFunc();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("powerupTag") && collision.name != "Cane(Clone)")
        {
            TimeGO.SetActive(true);
            TimeGO.GetComponent<Animator>().Play("TimeAnimation", 0, 0);
            Destroy(collision.gameObject);

            circleGO.transform.localScale = new Vector3(0.7f, 0.2381f);
            Time.timeScale = 1;
            rb.gravityScale = 1;
        }

        if (collision.name == "Bigger(Clone)")
        {
            //biggerPowerup = true;
            circleGO.transform.localScale = new Vector3(0.96f, 0.32f);
        }

        if (collision.name == "SlowMo(Clone)")
        {
            //slowmoPowerup = true;
            Time.timeScale = 0.5f;
        }

        if (collision.name == "ReverseG(Clone)")
        {
            //reversegPowerup = true;
            rb.gravityScale = -0.5f;
        }

        if (collision.name == "Cane(Clone)")
        {
            canePowerup = true;
            Destroy(collision.gameObject);

            if (CanvasCaneGO.transform.GetChild(0).position.x == 0)
            {
                Instantiate(CaneGO, new Vector3(0, CaneGO.transform.position.y + 2), CaneGO.transform.rotation, CanvasCaneGO.transform);
            }
        }
        
        if (collision.gameObject.CompareTag("obstacle") || collision.gameObject.CompareTag("islandTag"))
        {
            sirOverlapped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle") || collision.gameObject.CompareTag("islandTag"))
        {
            sirOverlapped = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        float heroMagnitude = rb.velocity.magnitude;
        if (collision.gameObject.CompareTag("caneTag"))
        {
            if (rb.velocity.magnitude == 0)
            {
                heroMagnitude = 1;
            }
            rb.velocity = new Vector2(rb.velocity.x / (heroMagnitude * 2), 1) * 25;
        }

        if (collision.gameObject.CompareTag("helperTag"))
        {
            collision.transform.position = new Vector3(20, Camera.main.transform.position.y - 9);
            canePowerup = false;

            if (CanvasCaneGO.transform.childCount > 1)
            {
                Destroy(CanvasCaneGO.transform.GetChild(CanvasCaneGO.transform.childCount - 1).gameObject);
            }
        }

        if (collision.gameObject.name == "mushroom_red(Clone)" || collision.gameObject.name == "mushroom_purple")
        {
            collision.gameObject.SetActive(false);
            transparencyPowerup = true;
            firstDisable = true;
            TransparencyFunc();
            TimeMushroom.SetActive(true);
            transform.GetComponent<SpriteRenderer>().color = new Color(0.735849f, 0, 0);

            if (collision.gameObject.name == "mushroom_red(Clone)")
            {
                TimeMushroom.GetComponent<Animator>().Play("TimeAnimation", 0, 0.7f);
                rb.velocity = new Vector2(0, 30);
            }
            else
            {
                TimeMushroom.GetComponent<Animator>().Play("TimeAnimation", 0, 0.6f);
                rb.velocity = new Vector2(0, 40);
            }
        }
    }

    void TransparencyFunc()
    {
        GameObject[] containers = GameObject.FindGameObjectsWithTag("containerTag");

        for (int i = 0; i < containers.Length; i++)
        {
            for (int j = 0; j < containers[i].transform.childCount; j++)
            {
                if (transparencyPowerup)
                {
                    containers[i].transform.GetChild(j).GetComponent<PolygonCollider2D>().isTrigger = true;
                }
                else
                {
                    containers[i].transform.GetChild(j).GetComponent<PolygonCollider2D>().isTrigger = false;
                }
                    
                for (int k = 0; k < containers[i].transform.GetChild(j).transform.childCount; k++)
                {
                    if (transparencyPowerup)
                    {
                        containers[i].transform.GetChild(j).GetChild(k).GetComponent<PolygonCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        containers[i].transform.GetChild(j).GetChild(k).GetComponent<PolygonCollider2D>().isTrigger = false;
                    }
                }
            }
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
        circleGO.transform.localScale = new Vector3(0.7f, 0.2381f);
        Time.timeScale = 1;
        rb.gravityScale = 1;
    }
}
