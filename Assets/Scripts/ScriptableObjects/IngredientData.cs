using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/IngredientData")]
public class IngredientData : ScriptableObject
{
    public Sprite InitialSprite;   
    public List<Sprite> GrindedSpriteList = new List<Sprite>();
}
