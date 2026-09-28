using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "ScriptableObjects/LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    public LevelData[] levels;
}