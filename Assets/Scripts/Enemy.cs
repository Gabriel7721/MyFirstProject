using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyType = "Goblin";
    public int damage = 25;
    public int healthPoint = 75;
    void Start()
    {
        Debug.Log("Enemy:" + enemyType);
        Debug.Log("Damage:" + damage);
        Debug.Log("Enemy HP:" + healthPoint);
    }

    void Update()
    {

    }
}
