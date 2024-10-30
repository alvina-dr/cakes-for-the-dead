using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    private Vector3 startCutPosition; //position qui sera instanci�e puis modifi�e pour avoir toute la ligne
    private int lines = 0;

    private Vector3 mousePosition;

    [SerializeField]
    private Transform parentLines;

    private bool gameStarted = false;
    private bool makeAppearLines = true;
    private bool isPressed = false;

    [SerializeField]
    private int linesLeft = 3;
    private float scoreTotal;
    void Start()
    {

    }

    void Update()
    {
        if (!makeAppearLines && !gameStarted && linesLeft > 0)
        {
            makeAppearLines = true;
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
        }
        if (gameStarted && isPressed && Input.GetMouseButton(0))
        {
            linePointInstantiated = Instantiate(mousePoint, mousePosition, Quaternion.identity, parentLines);
            mouseLines.Add(linePointInstantiated);
        }
        if (gameStarted && isPressed && Input.GetMouseButtonUp(0))
        {
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

            scoreTotal += Mathf.Clamp(scoreCut, 5, 100);

            DestroyLines();
            gameStarted = false;
            Debug.Log(scoreTotal);
        }
    }
}