using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Scriptable Objects/RecipeData")]
public class RecipeData : ScriptableObject
{
    public Sprite CakeSprite;
    public string CakeName;
    public List<MiniGameData> MiniGameList = new List<MiniGameData>();
}
