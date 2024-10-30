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
    
    public void AddMultiplicateur()
    {
        multiActuel++;
    }

    public void RemoveMultiplicateur()
    {
        multiActuel--;
    }

    public void ResetMultiplicateur()
    {
        multiActuel = 0;
    }
}
