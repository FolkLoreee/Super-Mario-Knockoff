using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    void Start()
    {
        GameManager.OnIncreaseScore += spawnFromPooler;
        //spawn 2 goombas
        for (int j = 0; j < 2; j++)
        {
            spawnFromPooler(ObjectType.koopaEnemy);
            spawnFromPooler(ObjectType.goombaEnemy);
        }
    }
    void spawnFromPooler(ObjectType type)
    {
        //static method access
        Debug.Log("Spawning Enemy: " + type);
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(type);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.transform.localScale = gameConstants.enemyOriginalScale;
            item.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough items in the pool");
        }
    }
}
