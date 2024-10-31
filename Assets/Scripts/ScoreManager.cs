using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Singleton

    public static ScoreManager Instance;

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
    public int scoreTempActuel;
    public int scoreRecetteEnCours;
    public int TotalScore;
    public int NumberOfCustomerDay;
    public int DayNumber = 1;
    public TMP_Text scoreText;
    public TMP_Text multitext;

    private void Update()
    {
        if (scoreTempActuel < 0)
        {
            scoreTempActuel = 0;
        }

        multitext.text = "x" + multiActuel;
        scoreText.text = scoreTempActuel.ToString();
    }

    public void AddMultiplicateur(int increase)
    {
        multiActuel+=increase;
    }

    public void RemoveMultiplicateur(int decrease)
    {
        if (multiActuel > 0)
        { multiActuel -= decrease; }
    }

    public void ResetMultiplicateur()
    {
        multiActuel = 0;
    }

    public void CalculScore()
    {
        scoreRecetteEnCours = scoreTempActuel * multiActuel;
        scoreTempActuel = 0;
        multiActuel = 0;
    }
}