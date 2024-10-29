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

    [Header("References")]
    public UI_Manager UIManager;

    public List<RecipeData> RecipeDataList = new List<RecipeData>();
    public RecipeData CurrentRecipeData;
    public int CurrentRecipeMiniGameIndex = 0;
    public float Timer = 0;
    public bool IsFirstGameLaunched;

    [Header("Score")]
    public int CurrentScore = 0;

    private void Start()
    {
        RecipeDataList.Clear();
        RecipeData[] recipeDataArray = Resources.LoadAll<RecipeData>("RecipeData");
        for (int i = 0; i < recipeDataArray.Length; i++)
        {
            RecipeDataList.Add(recipeDataArray[i]);
        }

        LaunchGame();
    }

    public void LaunchGame()
    {
        SelectRandomRecipe();
        UIManager.RecipeRequest.SetupRecipeRequest(CurrentRecipeData);
    }

    public void SelectRandomRecipe()
    {
        CurrentRecipeData = RecipeDataList[Random.Range(0, RecipeDataList.Count)];
    }

    public void LaunchRecipe()
    {
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
        CurrentRecipeMiniGameIndex = 0;
    }

    public MiniGameData GetCurrentMiniGame()
    {
        if (CurrentRecipeData == null) return null;
        return CurrentRecipeData.MiniGameList[CurrentRecipeMiniGameIndex];
    }
}
