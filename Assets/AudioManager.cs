using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager Instance;

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
    public UnityEvent onGameStart = new UnityEvent();
    public UnityEvent onChaudronScene = new UnityEvent();
    public UnityEvent onCut = new UnityEvent();
    public UnityEvent onCut2 = new UnityEvent();
    public UnityEvent onCut3 = new UnityEvent();
    public UnityEvent onPerfect = new UnityEvent();
    public UnityEvent onSoufflet = new UnityEvent();
    public UnityEvent onGrind = new UnityEvent();
    public UnityEvent onPour = new UnityEvent();
    public UnityEvent onFireOn = new UnityEvent();
    public UnityEvent onFireOff = new UnityEvent();
}
