using UnityEngine;

[CreateAssetMenu(fileName = "GeneralData", menuName = "Scriptable Objects/GeneralData")]
public class GeneralData : ScriptableObject
{
    public float DayDuration;
    public int BaseRent;
    public float RentMultiplier;
}
