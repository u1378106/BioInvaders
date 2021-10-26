using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
   // private Vector2 screenBounds;


    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        if (this.gameObject.tag == "Grocery" || this.gameObject.tag == "Immunity")
        {
            speed = -0.1f;
        }
        else
        {
            speed = Random.Range(-1, -5);
        }

        rb.velocity = new Vector2(0, speed);
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Debug.Log("screenBounds ; " + screenBounds);

        //Debug.Log("transform y : " + transform.position.y);
        //Debug.Log("screenbound y : " + screenBounds.y);

        if (this.gameObject.tag == "Obstacle")
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 5) * 50 * Time.deltaTime);
        }




        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }
}