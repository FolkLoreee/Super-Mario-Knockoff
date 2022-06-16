using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;
    public void consumedBy(GameObject player)
    {
        //give player a jump boost
        player.GetComponent<PlayerController>().jumpSpeed += 10;
        Debug.Log("CONSUMEDD");
        StartCoroutine(removeEffect(player));
    }
    IEnumerator removeEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().jumpSpeed -= 10;
        Debug.Log("Effect Removed");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
            //update UI
            Debug.Log("Collided with player");
            CentralManager.centralManagerInstance.addPowerup(t, 0, this);
            GetComponent<Collider2D>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
