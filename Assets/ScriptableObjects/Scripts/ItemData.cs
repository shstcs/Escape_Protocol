using UnityEngine;

public enum ItemType
{
    RedKey,
    GreenKey,
    BlueKey
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string _displayName;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] private Vector3 _itemPosition;

    public string DisplayName
    {
        get { return _displayName; }
        private set { _displayName = value; }
    }
}