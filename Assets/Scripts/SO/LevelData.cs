using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public int targetCount;

    public TileStateSO targetFruit;
}
