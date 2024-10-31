using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class AppearCutLines : MonoBehaviour
{

    [SerializeField]
    private GameObject cutLinePoint;
    [SerializeField]
    private GameObject mousePoint;
    private GameObject linePointInstantiated;
    public List<GameObject> allCutLines = new List<GameObject>();
    public List<GameObject> mouseLines = new List<GameObject>();
    private Vector3 startCutPosition; 
    private Vector3 mousePosition;
    [SerializeField]
    private TrailRenderer trail;

    [SerializeField]
    private Transform parentLines;

    private bool gameStarted = false;
    private bool makeAppearLines = true;
    private bool isPressed = false;

    [SerializeField]
    private int linesLeft = 3;
    private int scoreTotal;

    public UnityEvent onCut = new UnityEvent();
    private void Start()
    {
    }

    void Update()
    {

        if (!makeAppearLines && !gameStarted && linesLeft > 0)
        {
            makeAppearLines = true;
        } 
        else if (!makeAppearLines && !gameStarted && linesLeft <= 0 )
        {
            //End game -> here add to total score
            GameManager.Instance.EndMiniGame();
        }
        
        Decoupe();
    }

    private void Lines()
    {
        float affine = Random.Range(-2.5f, 2.5f);
        float x = -4;
        int line = 0;
        startCutPosition = new Vector3(x, positionY(x, affine));
        float y = startCutPosition.y;
        while (x < 4)
        {
            if (line > 3)
            {
                x += .25f;
                line = 0;
            }
            if (positionY(x, affine) < 4 && positionY(x, affine) > -4)
            {
                linePointInstantiated = Instantiate(cutLinePoint, new Vector3(x, positionY(x, affine)), Quaternion.identity, parentLines);
                linePointInstantiated.transform.position = new Vector3 (linePointInstantiated.transform.position.x, linePointInstantiated.transform.position.y, -1);
                allCutLines.Add(linePointInstantiated);
            }
            line++;
            x += 0.05f;
        }
    }

    private float positionY(float x, float affine)
    {
        float y = 0;
        y = x * affine;
        return y;
    }

    private void DestroyLines()
    {
        foreach (GameObject line in allCutLines)
        {
            Destroy(line);
        }

        foreach (GameObject line in mouseLines)
        {
            Destroy(line);
        }

        allCutLines.Clear();
        mouseLines.Clear();
    }

    private void Decoupe()
    {
        Vector3 mousePos = Input.mousePosition + new Vector3(0, 0, 10);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePos);

        if (makeAppearLines)
        {
            Lines();
            gameStarted = true;
            linesLeft--;
            makeAppearLines = false;
        }
        if (gameStarted && Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            //trail.enabled = true;
            //trail.transform.position = new Vector3(mousePosition.x, mousePosition.y, -1);
        }
        if (gameStarted && isPressed && Input.GetMouseButton(0))
        {
            linePointInstantiated = Instantiate(mousePoint, mousePosition, Quaternion.identity, parentLines);
            mouseLines.Add(linePointInstantiated);
        }
        if (gameStarted && isPressed && Input.GetMouseButtonUp(0))
        {
            onCut.Invoke();
            switch (linesLeft)
            {
                case 2:
                    AudioManager.Instance.onCut.Invoke();
                    break;
                case 1:
                    AudioManager.Instance.onCut2.Invoke();
                    break;
                case 0:
                    AudioManager.Instance.onCut3.Invoke();
                    break;
            }

            isPressed = false;

            float scoreCut = 0;
            int scoreCutCount = 1;

            foreach (GameObject objet in mouseLines)
            {
                Vector3 mousePoint = objet.transform.position;
                scoreCutCount++;


                float distance = Mathf.Sqrt(Mathf.Pow(mousePoint.x - allCutLines[0].transform.position.x, 2) + Mathf.Pow(mousePoint.y - allCutLines[0].transform.position.y, 2));

                for (int i = 1; i < allCutLines.Count; i++)
                {

                    if (Mathf.Sqrt(Mathf.Pow(mousePoint.x - allCutLines[i].transform.position.x, 2) + Mathf.Pow(mousePoint.y - allCutLines[i].transform.position.y, 2)) < distance)
                    {

                        distance = Mathf.Sqrt(Mathf.Pow(mousePoint.x - allCutLines[i].transform.position.x, 2) + Mathf.Pow(mousePoint.y - allCutLines[i].transform.position.y, 2));

                    }

                }

                scoreCut += distance;
            }

            if (mouseLines.Count < 20) { scoreCut *= 20; }
            else { scoreCut /= 1.5f; }
            scoreCut /= scoreCutCount;
            scoreCut *= 10;
            scoreCut = (1 / scoreCut);
            scoreCut *= 100;
            scoreCut *= 1.5f;
            scoreCut = Mathf.Clamp(scoreCut, 5, 100);

            scoreTotal += Mathf.RoundToInt(scoreCut);
            #region Multis
            if (scoreCut < 50)
            {
                ScoreManager.Instance.RemoveMultiplicateur(1);
            }
            else if (scoreCut > 75 && scoreCut < 90)
            {
                ScoreManager.Instance.AddMultiplicateur(1);
                AudioManager.Instance.onPerfect.Invoke();
            }
            else if (scoreCut > 90)
            {
                ScoreManager.Instance.AddMultiplicateur(3);
                AudioManager.Instance.onPerfect.Invoke();
            }


            if (scoreTotal < 50)
            {
                ScoreManager.Instance.ResetMultiplicateur();
            }
            else if (scoreTotal > 275)
            {
                ScoreManager.Instance.AddMultiplicateur(2);
                AudioManager.Instance.onPerfect.Invoke();
            }
            else if (scoreTotal > 300)
            {
                ScoreManager.Instance.AddMultiplicateur(6);
                AudioManager.Instance.onPerfect.Invoke();
            }

            ScoreManager.Instance.scoreTempActuel += scoreTotal;

            if (linesLeft == 0)
            {
                ScoreManager.Instance.CalculScore();
            }

            #endregion
            DestroyLines();
            gameStarted = false;
            //trail.enabled = false;
        }
    }
}