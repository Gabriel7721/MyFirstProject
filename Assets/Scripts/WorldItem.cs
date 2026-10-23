using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private string uniqueId;
    public string UniqueId => uniqueId;
}
