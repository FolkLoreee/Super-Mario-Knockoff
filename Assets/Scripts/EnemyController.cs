using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private ObjectType enemyType;
    private int moveRight;
    private bool isVictory = false;
    private float originalX;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyType = ObjectPooler.SharedInstance.GetEnemyTypeByName(this.name);
        GameManager.OnPlayerDeath += EnemyRejoice;

        //get starting position
        originalX = transform.position.x;

        //randomise initial direction
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;

        //compute initial velocity
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatrolTime, 0);
    }

    void MoveEnemy()
    {
        if (!isVictory)
        {
            enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);

        }
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {
            MoveEnemy();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //check if collides on top
            float yOffset = (other.transform.position.y - this.transform.position.y);
            if (yOffset > .75f)
            {
                KillSelf();
            }
            else
            {
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }
    }

    void KillSelf()
    {
        //enemy dies
        CentralManager.centralManagerInstance.increaseScore(enemyType);
        StartCoroutine(flatten());
    }

    IEnumerator flatten()
    {
        Debug.Log("Flattening start");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            //make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flattening ends");
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield break;
    }
    //animation when player is dead
    private void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario!");
        enemyBody.velocity = Vector2.up;
        //todo: add animation here
    }
}