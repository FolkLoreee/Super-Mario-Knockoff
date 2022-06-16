using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameObject powerupManagerObject;
    private PowerupManager powerupManager;
    private GameManager gameManager;
    public static CentralManager centralManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        centralManagerInstance = this;
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerupManager = powerupManagerObject.GetComponent<PowerupManager>();
    }
    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerupManager.consumePowerup(k, g);
    }
    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        powerupManager.addPowerup(t, i, c);
    }
    public void increaseScore(ObjectType enemyType)
    {
        gameManager.increaseScore(enemyType);
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
