using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float movementSpeed;
    public float springForce;
    private Vector2 velocity;
    private int moveRight;
    private Rigidbody2D mushroomBody;
    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        moveRight = Random.Range(0, 2);
        if (moveRight == 0) moveRight = -1;
        ComputeVelocity();
        mushroomBody.AddForce(Vector2.up * springForce, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        MoveMushroom();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * movementSpeed, 0);
    }
    void MoveMushroom()
    {
        mushroomBody.MovePosition(mushroomBody.position + velocity * Time.fixedDeltaTime);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstructions") || col.gameObject.CompareTag("Blocks"))
        {
            moveRight *= -1;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            movementSpeed = 0;
        }
        ComputeVelocity();
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
