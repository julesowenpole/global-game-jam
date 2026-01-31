using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> bloodClues = new List<GameObject>();
    [SerializeField] public List<GameObject> oracleClues = new List<GameObject>();
    [SerializeField] public List<GameObject> emotionClues = new List<GameObject>();
    public MaskManager maskManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        if (maskManager.GetCurrentMaskId() == 0)
        {
            foreach (var item in bloodClues)
            {
                item.SetActive(true);
            }
        }

        if (maskManager.GetCurrentMaskId() == 1)
        {
            foreach (var item in oracleClues)
            {
                item.SetActive(true);
            }
        }

        if (maskManager.GetCurrentMaskId() == 2)
        {
            foreach (var item in emotionClues)
            {
                item.SetActive(true);
            }
        }

    }
}
