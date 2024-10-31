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

    public List<RecipeData> RecipeDataList = new List<RecipeData>();
    public RecipeData CurrentRecipeData;
    public int CurrentRecipeMiniGameIndex = 0;
    public float Timer = 0;
    public bool IsFirstGameLaunched = false;
    public bool IsPlayerMakingRecipe = false;

    [Header("Score")]
    public int TotalCurrentScore = 0;

    private void Start()
    {
        RecipeDataList.Clear();
        RecipeData[] recipeDataArray = Resources.LoadAll<RecipeData>("RecipeData");
        for (int i = 0; i < recipeDataArray.Length; i++)
        {
            RecipeDataList.Add(recipeDataArray[i]);
        }

        Timer = GeneralData.DayDuration;
        UIManager.TimerUI.SetTextValue(Mathf.RoundToInt(Timer).ToString());
        UIManager.ScoreUI.SetTextValue(ScoreManager.Instance.TotalScore.ToString());

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
        if (CurrentRecipeData == null) SelectRandomRecipe();
    }

    public void SelectRandomRecipe()
    {
        CurrentRecipeData = RecipeDataList[Random.Range(0, RecipeDataList.Count)];
    }

    public void LaunchRecipe()
    {
        IsFirstGameLaunched = true;
        IsPlayerMakingRecipe = true;
        if (CurrentRecipeData == null)
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
        DOVirtual.DelayedCall(.5f, () =>
        {
            if (Timer <= 0)
            {
                EndDay();
            }
            else
            {
                //show next recipe
                Debug.Log("next command");
                NextCommand();
            }
        });
    }

    public void NextCommand()
    {
        SelectRandomRecipe();
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
        if (CurrentRecipeData == null) return null;
        return CurrentRecipeData.MiniGameList[CurrentRecipeMiniGameIndex];
    }

    public void EndMiniGame()
    {
        SceneManager.UnloadSceneAsync(GetCurrentMiniGame().MiniGameLevelName);
        CurrentRecipeMiniGameIndex++;
        if (CurrentRecipeMiniGameIndex >= CurrentRecipeData.MiniGameList.Count)
        {
            //end recipe
            //select new recipe
            //show final result
            FinishRecipe();
        }
        else //load next minigame
        {
            SceneManager.LoadScene(GetCurrentMiniGame().MiniGameLevelName, LoadSceneMode.Additive);
        }
    }

    public void NextDay()
    {
        UIManager.StartDay.gameObject.SetActive(true);
        UIManager.StartDay.Open();
        UIManager.EndDayScene.Close();
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
        UIManager.TimerUI.SetTextValue(Mathf.RoundToInt(Timer).ToString());
        IsFirstGameLaunched = false;
        UIManager.EndDayScene.gameObject.SetActive(true);
        UIManager.EndDayScene.Open();
        //show interface for end of day
        //check if enough score
    }

    public void AddToTotalScore(int score)
    {
        //here add small animation that indicates the score gained in the minigame
        TotalCurrentScore += score;
    }
}
