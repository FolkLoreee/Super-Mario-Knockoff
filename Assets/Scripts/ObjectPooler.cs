using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    goombaEnemy = 0,
    koopaEnemy = 1
}
[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
}
public class ExistingPoolItem
{
    public GameObject gameObject;
    public ObjectType type;

    //constructor
    public ExistingPoolItem(GameObject gameObject, ObjectType type)
    {
        //reference input
        this.gameObject = gameObject;
        this.type = type;
    }
}
public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> itemsToPool;
    public List<ExistingPoolItem> pooledObjects;
    public static ObjectPooler SharedInstance;
    void Awake()
    {
        SharedInstance = this;
        Debug.Log("Instance created");
        pooledObjects = new List<ExistingPoolItem>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                //this 'pickup' is a local variable, but Unity will not remove it since it exists in the scene
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent = this.transform;
                pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
            }
        }
    }
    public GameObject GetPooledObject(ObjectType type)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //if object is not yet placed in the scene and if they are of the requested type
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
            {
                return pooledObjects[i].gameObject;
            }
        }
        //when no more active object is present, expand the pool
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type)
            {
                if (item.expandPool)
                {
                    GameObject pickup = (GameObject)Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent = this.transform;
                    pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
                    return pickup;
                }
            }
        }
        return null;
    }
    public ObjectType GetEnemyTypeByName(string enemyName)
    {
        if (enemyName.Contains("GoombaEnemy"))
        {
            return ObjectType.goombaEnemy;
        }
        else if (enemyName.Contains("KoopaEnemy"))
        {
            return ObjectType.koopaEnemy;
        }
        return ObjectType.goombaEnemy;
    }
}
