using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;



public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    //speed of player
    public float moveSpeed = 5f;

    //jump speed (y axis)
    public float jumpSpeed = 8f;

    //work with animation
    private Animator anim;

    //check if player hit ground
    public LayerMask groundLayer;

    public bool isGrounded; //true if hit ground

    //variable to keep enemy dissaapare effect
    public float hitForce;

    //sound manage
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip itemSound;
    public AudioClip hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        //assign an audio for player
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();

        //check when user press key on keyboard
        if (Input.GetAxis("Horizontal") > 0 && !LevelManager.instance.isGameOver)
        {
            //move right (x,y,z)
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);

            //player turn right : 2f is from the scale in player (unity)
            transform.localScale = new Vector3(2f,2f,2f);

        }
        else if (Input.GetAxis("Horizontal") < 0 && !LevelManager.instance.isGameOver)
        {
            //move left
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.x, 0f);

            //player turn left : 2f is from the scale in player (unity)
            transform.localScale = new Vector3(-2f, 2f, 2f);
        }
        else 
        {
            //stand still/ no move
            rb.velocity = new Vector3(0f,rb.velocity.y, 0f);
        }


        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !LevelManager.instance.isGameOver) 
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, 0f);
            //add sound
            audioSource.PlayOneShot(jumpSound);
        
        }


        //play animation

        //idle -> walk
        anim.SetFloat("speed",Mathf.Abs(rb.velocity.x));

        //Play Animation jump when jump only
        anim.SetBool("isJumping",isGrounded);

    }

    public void checkGrounded() 
    {
        Vector2 position = transform.position; //keep player current position

        Vector2 direction = Vector2.down; //direction y axis

        
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position,direction,distance,groundLayer);

        //check if player hit anything
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else 
        {
            isGrounded = false;        
        }

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("KillPlane"))
        {
            //access static method from another class by classname.obj.method()
            LevelManager.instance.RestartGame();
            
        }


        //check if player hit item to score or not
        if (target.gameObject.CompareTag("Item")) 
        {
            //remove item after hit 
            Destroy(target.gameObject);

            //invoke AddScore from LevelManager
            LevelManager.instance.AddScore();

            //add sound
            audioSource.PlayOneShot(itemSound);
        }

    }

    


    //make player stick with the floting ground
    private void OnCollisionEnter2D(Collision2D target)
    {
        //Platformer ack as mother and target ack as child
        //in order to stick on the floatting ground
        if (target.gameObject.CompareTag("Platformer")) 
        {
            transform.parent = target.transform;
        }


        //check if player hit enemy, then make enemy dissapare
        if (target.gameObject.CompareTag("Enemy"))
        {
            Destroy(target.gameObject);

            rb.velocity = new Vector3(rb.velocity.x,hitForce,0f);

            LevelManager.instance.SubtractHP();

            //add sound
            audioSource.PlayOneShot(hitSound);
        }
        
    }

    //if player is not on the platformer()floating ground, don't stick on the floating ground.(return to original)
    private void OnCollisionExit2D(Collision2D target)
    {
        transform.parent = null;
    }

}
