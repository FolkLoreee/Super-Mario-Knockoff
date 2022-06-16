using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool isBroken = false;
    public GameObject debrisPrefab;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isBroken)
        {
            isBroken = true;
            CentralManager.centralManagerInstance.increaseScore(ObjectType.goombaEnemy);
            audioSource.Play();


            for (int x = 0; x < 5; x++)
            {
                Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            // Destroy(gameObject);
        }
    }
}
