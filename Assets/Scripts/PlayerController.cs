using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
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
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                score++;
                countScoreState = false;
                scoreText.text = score.ToString();
            }
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
            Vector2 newVelocity = marioBody.velocity;
            if (onGroundState)
            {
                newVelocity.x = 0;
            }
            marioBody.velocity = newVelocity;
        }
        //if spacebar then jump
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
            countScoreState = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // Time.timeScale = 0.0f;
            SceneManager.LoadScene("MarioScene");

        }
    }
}
