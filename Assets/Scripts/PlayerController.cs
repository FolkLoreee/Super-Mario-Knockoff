using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform enemyLocation;
    private Animator marioAnimator;
    private AudioSource marioAudio;

    private SpriteRenderer marioSprite;
    private bool onGroundState = true;
    private bool isFacingRight = true;
    public ParticleSystem dustCloud;
    public float speed;
    public float jumpSpeed;
    public const float maxSpeed = 50;
    private Rigidbody2D marioBody;
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }
    //non-physics updates are handled within this function
    void Update()
    {
        if (Input.GetKeyDown("a") && isFacingRight)
        {
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
            isFacingRight = false;
            marioSprite.flipX = true;
        }
        if (Input.GetKeyDown("d") && !isFacingRight)
        {
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
            isFacingRight = true;
            marioSprite.flipX = false;
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
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
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Enemy"))
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            dustCloud.Play();
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
    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    private void someFunction()
    {
        Debug.Log("hello");
    }
}
