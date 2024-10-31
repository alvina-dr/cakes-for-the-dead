using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public UI_TextValue TimerUI;
    public UI_TextValue ScoreUI;
    public UI_TextValue DayUI;

    [Header("Layouts")]
    public UI_StartDay StartDay;
    public UI_NewCommand NewCommand;
    public UI_EndDay EndDayScene;
    public UI_EndRecipe EndRecipe;
    public UI_MainMenu MainMenu;
}
