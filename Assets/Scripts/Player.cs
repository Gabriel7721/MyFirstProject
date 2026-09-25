using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 3;
    public int gold = 250;
    void Start()
    {
        Debug.Log("Player started");
        Debug.Log("Player level: " + level);
        Debug.Log("Player gold: " + gold);
    }

    void Update()
    {

    }
}
