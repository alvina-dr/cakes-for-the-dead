using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

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

    [Header("Data")]
    public GeneralData GeneralData;

    [Header("References")]
    public UI_Manager UIManager;

    public List<CustomerData> RecipeDataList = new List<CustomerData>();
    public CustomerData CurrentCustomerData;
    public int CurrentRecipeMiniGameIndex = 0;
    public float Timer = 0;
    public bool IsFirstGameLaunched = false;
    public bool IsPlayerMakingRecipe = false;
    public int RentDue;

    private void Start()
    {
        RecipeDataList.Clear();
        CustomerData[] recipeDataArray = Resources.LoadAll<CustomerData>("CustomerData");
        for (int i = 0; i < recipeDataArray.Length; i++)
        {
            RecipeDataList.Add(recipeDataArray[i]);
        }

        Timer = GeneralData.DayDuration;
        UIManager.TimerUI.SetTextValue(Mathf.RoundToInt(Timer).ToString());
        UIManager.ScoreUI.SetTextValue(ScoreManager.Instance.TotalScore.ToString());
        UIManager.DayUI.SetTextValue(ScoreManager.Instance.DayNumber.ToString());
        RentDue = GeneralData.BaseRent;

        LaunchGame();
    }

    private void Update()
    {
        if (IsFirstGameLaunched)
        {
            Timer -= Time.deltaTime;
            UIManager.TimerUI.SetTextValue(Mathf.RoundToInt(Timer).ToString());
        }

        if (Timer <= 0)
        {
            Timer = 0;
            if (!IsPlayerMakingRecipe)
            {
            }
        }
    }

    public void LaunchGame()
    {
        if (CurrentCustomerData == null) SelectRandomRecipe();
    }

    public void SelectRandomRecipe()
    {
        CurrentCustomerData = RecipeDataList[Random.Range(0, RecipeDataList.Count)];
    }

    public void LaunchRecipe()
    {
        IsFirstGameLaunched = true;
        IsPlayerMakingRecipe = true;
        if (CurrentCustomerData == null)
        {
            Debug.LogError("NO CURRENT RECIPE SELECTED");
            return;
        }
        //load level of first minigame
        SceneManager.LoadScene(GetCurrentMiniGame().MiniGameLevelName, LoadSceneMode.Additive);
    }

    public void FinishRecipe()
    {
        UIManager.UI_EndRecipe.gameObject.SetActive(true);
        UIManager.UI_EndRecipe.Open();
        IsPlayerMakingRecipe = false;
        CurrentRecipeMiniGameIndex = 0;
    }

    //Called by button Close in menu EndRecipe
    public void CloseEndRecipe()
    {
        ScoreManager.Instance.NumberOfCustomerDay++;
        UIManager.UI_EndRecipe.Close();
        CurrentCustomerData = null;
        DOVirtual.DelayedCall(.5f, () =>
        {
            if (Timer <= 0)
            {
                EndDay();
            }
            else
            {
                NextCommand();
            }
        });
    }

    public void NextCommand()
    {
        if (CurrentCustomerData == null) SelectRandomRecipe();
        UIManager.NewCommand.gameObject.SetActive(true);
        UIManager.NewCommand.Open();
    }

    //In new command menu, button prepare calls this function
    public void AcceptCommand()
    {
        UIManager.NewCommand.Hide();
        DOVirtual.DelayedCall(.8f, () => LaunchRecipe());
    }

    public MiniGameData GetCurrentMiniGame()
    {
        if (CurrentCustomerData == null) return null;
        return CurrentCustomerData.Recipe.MiniGameList[CurrentRecipeMiniGameIndex];
    }

    public void EndMiniGame()
    {
        SceneManager.UnloadSceneAsync(GetCurrentMiniGame().MiniGameLevelName);
        CurrentRecipeMiniGameIndex++;
        if (CurrentRecipeMiniGameIndex >= CurrentCustomerData.Recipe.MiniGameList.Count) //end recipe
        {
            FinishRecipe();
        }
        else //load next minigame
        {
            SceneManager.LoadScene(GetCurrentMiniGame().MiniGameLevelName, LoadSceneMode.Additive);
        }
    }

    public void NextDay()
    {
        UIManager.TimerUI.SetTextValue(Mathf.RoundToInt(Timer).ToString());
        UIManager.ScoreUI.SetTextValue(ScoreManager.Instance.TotalScore.ToString());
        ScoreManager.Instance.DayNumber++;
        UIManager.DayUI.SetTextValue(ScoreManager.Instance.DayNumber.ToString());
        UIManager.StartDay.gameObject.SetActive(true);
        UIManager.StartDay.Open();
        UIManager.EndDayScene.Close();
        RentDue = GeneralData.BaseRent * (Mathf.RoundToInt(ScoreManager.Instance.DayNumber * GeneralData.RentMultiplier) + 1);

    }

    public void StartDay()
    {
        ScoreManager.Instance.NumberOfCustomerDay = 0;
        UIManager.StartDay.Hide();
        NextCommand();
    }

    public void EndDay()
    {
        Timer = GeneralData.DayDuration;
        IsFirstGameLaunched = false;
        UIManager.EndDayScene.gameObject.SetActive(true);
        UIManager.EndDayScene.Open();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
