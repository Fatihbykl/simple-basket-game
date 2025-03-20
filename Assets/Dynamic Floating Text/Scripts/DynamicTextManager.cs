using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicTextManager : MonoBehaviour
{

    public static DynamicTextData defaultData;
    public static DynamicTextData basketData;
    public static DynamicTextData perfectBasketData;
    public static GameObject canvasPrefab;
    public static Transform mainCamera;

    [SerializeField] private DynamicTextData _defaultData;
    [SerializeField] private DynamicTextData _basketData;
    [SerializeField] private DynamicTextData _perfectBasketData;
    [SerializeField] private GameObject _canvasPrefab;
    [SerializeField] private Transform _mainCamera;

    private void Awake()
    {
        defaultData = _defaultData;
        basketData = _basketData;
        perfectBasketData = _perfectBasketData;
        mainCamera = _mainCamera;
        canvasPrefab = _canvasPrefab;
    }

    public static void CreateText2D(Vector2 position, string text, DynamicTextData data)
    {
        GameObject newText = Instantiate(canvasPrefab, position, Quaternion.identity);
        newText.transform.GetComponent<DynamicText2D>().Initialise(text, data);
    }

    public static void CreateText(Vector3 position, string text, DynamicTextData data)
    {
        GameObject newText = Instantiate(canvasPrefab, position, Quaternion.identity);
        newText.transform.GetComponent<DynamicText>().Initialise(text, data);
    }

}
