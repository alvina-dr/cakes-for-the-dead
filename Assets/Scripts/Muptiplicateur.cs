using UnityEngine;

public class Multiplicateur : MonoBehaviour
{
    #region Singleton

    public static Multiplicateur Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion
    public int multiActuel;
    
    public void AddMultiplicateur(int increase)
    {
        multiActuel+=increase;
    }

    public void RemoveMultiplicateur(int decrease)
    {
        multiActuel-= decrease;
    }

    public void ResetMultiplicateur()
    {
        multiActuel = 0;
    }
}