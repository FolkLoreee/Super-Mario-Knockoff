using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    int currentScore;
    int currentPlayerHealth;

    // Reset values
    public Vector3 goombaSpawnPointStart = new Vector3(0, -0.45f, 0);
    public Vector3 enemyOriginalScale = new Vector3(1, 1, 1);

    //EnemyController.cs
    public int maxOffset = 10;
    public int enemyPatrolTime = 10;
    public float groundSurface = 1.5f;
    //Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    //Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    //SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    //Rotator.cs
    public int rotatorRotateSpeed = 6;

    //Testing
    public int testValue;
}