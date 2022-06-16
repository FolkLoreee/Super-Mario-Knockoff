using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    private int playerScore = 0;
    public delegate void gameEvent();
    public delegate void spawnEvent(ObjectType enemyType);
    public static gameEvent OnPlayerDeath;
    public static spawnEvent OnIncreaseScore;

    public void increaseScore(ObjectType enemyType)
    {
        playerScore++;
        score.text = playerScore.ToString();
        OnIncreaseScore(enemyType);
    }
    public void damagePlayer()
    {
        //OnPlayerDeath is an event that will be broadcast to subscribers
        OnPlayerDeath();
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
