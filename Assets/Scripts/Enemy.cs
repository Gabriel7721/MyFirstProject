using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyType = "Goblin";
    public int damage = 25;
    void Start()
    {
        Debug.Log("Enemy:" + enemyType);
        Debug.Log("Damage:" + damage);
    }

    void Update()
    {

    }
}
