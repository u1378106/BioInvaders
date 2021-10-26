using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalVirus : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    


    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        speed = Random.Range(-1, -5);

        //rb.velocity = new Vector2(0, speed);

        if (this.gameObject.name.Contains("Left"))
        {
            Vector3 dir = Quaternion.AngleAxis(45, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * 5);
            rb.velocity = new Vector2(5, -5);
        }

        if (this.gameObject.name.Contains("Right"))
        {
            Vector3 dir = Quaternion.AngleAxis(45, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * 5);
            rb.velocity = new Vector2(-5, -5);
        }
    }
}