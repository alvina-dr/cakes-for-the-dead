using UnityEngine;

public class AppearCutLines : MonoBehaviour
{

    [SerializeField]
    private GameObject cutLinePoint;
    private GameObject linePointInstantiated;
    [SerializeField]
    private GameObject[] allCutLines = new GameObject[10];
    private Vector3 tempPosition; //position qui sera instanciée puis modifiée pour avoir toute la ligne
    private int lines = 0;

    private bool randomizeLines = false;
    private bool makeAppearLines = true;
    [SerializeField]
    private GameObject positionFirstPoint;
    [SerializeField]
    private GameObject positionSecondPoint;
    [SerializeField]
    private GameObject positionThirdPoint;
    [SerializeField]
    private GameObject positionFourthPoint;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (randomizeLines)
        {
            RandomizeLines();
            randomizeLines = false;
        }
        if (makeAppearLines)
        {
            for (int y = 0; y < 3; y++)
            {
                switch (y)
                {
                    case 0:
                        _startPosition = positionFirstPoint.transform.position;
                        _endPosition = positionSecondPoint.transform.position;
                        break;
                    case 1:
                        _startPosition = positionSecondPoint.transform.position;
                        _endPosition = positionThirdPoint.transform.position;
                        break;
                    case 2:
                        _startPosition = positionThirdPoint.transform.position;
                        _endPosition = positionFourthPoint.transform.position;
                        break;
                }
                for (int i = 0; i < 10; i++)
                {
                    CalculateNextCutPoint();
                    linePointInstantiated = Instantiate(cutLinePoint, tempPosition, Quaternion.identity);
                    allCutLines[i] = linePointInstantiated;
                }
            }
           
            makeAppearLines = false;
        }
    }

    private void RandomizeLines()
    {
        positionFirstPoint.transform.position = new Vector3(positionFirstPoint.transform.position.x, Random.Range(-5,5), positionFirstPoint.transform.position.z);
        positionSecondPoint.transform.position = new Vector3(positionSecondPoint.transform.position.x, Random.Range(-5, 5), positionSecondPoint.transform.position.z);
        positionThirdPoint.transform.position = new Vector3(positionThirdPoint.transform.position.x, Random.Range(-5, 5), positionThirdPoint.transform.position.z);
        positionFourthPoint.transform.position = new Vector3(positionFourthPoint.transform.position.x, Random.Range(-5, 5), positionFourthPoint.transform.position.z);
    } //Permet d'avoir des position differentes a chaque entrée dans le mini jeu // il faudra simplement passer le bool randomizeLines a true dans l'appel
    private void CalculateNextCutPoint()
    {
        tempPosition = positionFirstPoint.transform.position;
    }
}