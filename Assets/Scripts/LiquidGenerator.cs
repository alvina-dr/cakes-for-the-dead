using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class LiquidGenerator : MonoBehaviour
{
    [SerializeField] private int _dropAmount;
    [SerializeField] private float _width;
    [SerializeField] private int _numPerLine;
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] private List<GameObject> _dropList = new List<GameObject>();

    [Button]
    public void GenerateLiquid()
    {
        DeletePool();

        for (int i = 0; i < _dropAmount; i++)
        {
            GameObject go = Instantiate(_dropPrefab, transform);
            float coeff = Mathf.RoundToInt(i / (float)_numPerLine);
            int numberInLine = Mathf.RoundToInt(i - (coeff * _numPerLine));
            go.transform.position = transform.position + new Vector3((float) numberInLine * (_width / _numPerLine), coeff * .15f, 0);
            _dropList.Add(go);
        }
    }

    [Button]
    public void DeletePool()
    {
        for (int i = _dropList.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(_dropList[i]);
        }

        _dropList.Clear();
    }
}
