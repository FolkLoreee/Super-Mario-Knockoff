using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public List<GameObject> powerUpIcons;
    private List<ConsumableInterface> powerups;
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<ConsumableInterface>();
        for (int i = 0; i < powerUpIcons.Count; i++)
        {
            powerUpIcons[i].SetActive(false);
            powerups.Add(null);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addPowerup(Texture texture, int index, ConsumableInterface i)
    {
        Debug.Log("Adding powerup to index: " + index);
        if (index < powerUpIcons.Count)
        {
            powerUpIcons[index].GetComponent<RawImage>().texture = texture;
            powerUpIcons[index].SetActive(true);
            powerups[index] = i;
        }
    }

    public void removePowerup(int index)
    {
        if (index < powerUpIcons.Count)
        {
            Debug.Log("Removing powerup of index: " + index);
            powerUpIcons[index].SetActive(false);
            powerups[index] = null;
        }
    }

    void cast(int i, GameObject p)
    {
        if (powerups[i] != null)
        {
            Debug.Log("Casting powerup index: " + i);
            powerups[i].consumedBy(p);
            removePowerup(i);
        }
    }

    public void consumePowerup(KeyCode k, GameObject player)
    {
        switch (k)
        {
            case KeyCode.Z:
                cast(0, player);
                break;
            case KeyCode.X:
                cast(1, player);
                break;
            default:
                break;
        }
    }

}
