using UnityEngine;

[CreateAssetMenu(fileName = "TileState", menuName = "ScriptableObjects/TileStateSO")]
public class TileStateSO : ScriptableObject
{
    public string fruitName;
    public Sprite spriteRef;
    public int chainNumber;
    public int point;

}
