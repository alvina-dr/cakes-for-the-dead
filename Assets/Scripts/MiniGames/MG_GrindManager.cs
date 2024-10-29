using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_GrindManager : MonoBehaviour
{
    #region Singleton
    public static MG_GrindManager Instance;

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

    public void StartGame()
    {
        //if something needs to be done on start
        //ingredients already in
    }

    public void EndGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("NO GAME MANAGER IN SCENE");
            return;
        }
        SceneManager.UnloadSceneAsync(GameManager.Instance.GetCurrentMiniGame().MiniGameLevelName);
        GameManager.Instance.CurrentRecipeMiniGameIndex++;
    }
}
