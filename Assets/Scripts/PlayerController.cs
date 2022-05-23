using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer marioSprite;
    private bool onGroundState = true;
    private bool isFacingRight = true;
    public float speed;
    public float jumpSpeed;
    public const float maxSpeed = 30;
    private Rigidbody2D marioBody;
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }
    //non-physics updates are handled within this function
    void Update()
    {
        //TODO: Add walking animation
        //TODO: Add jumping animation
        if (Input.GetKeyDown("a") && isFacingRight)
        {
            isFacingRight = false;
            marioSprite.flipX = true;
        }
        if (Input.GetKeyDown("d") && !isFacingRight)
        {
            isFacingRight = true;
            marioSprite.flipX = false;
        }
    }

    //physics updates are handled within this function
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            marioBody.velocity = Vector2.zero;
        }
        //if spacebar then jump
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("OMONA");
        }
    }
}
