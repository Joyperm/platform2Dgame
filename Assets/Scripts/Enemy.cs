using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //create variable to keep position
    public Transform leftPoint;
    public Transform rightPoint;

    //speed
    public float speed = 3f;

    private Rigidbody2D rb;

    //check player current position
    public bool moveRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check the position of enemy before assign direction
        if (moveRight && transform.position.x > rightPoint.position.x) 
        {
            moveRight = false;
        }

        if (!moveRight && transform.position.x < leftPoint.position.x) 
        {
            moveRight = true;
        }

        //make enemy move back and forth
        if (moveRight)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, 0f);

            //flip the enemy to the left
            transform.localScale = new Vector3(-2f, 2f, 2f);
        }
        else 
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0f);

            //flip the enemy to the right
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }
}
