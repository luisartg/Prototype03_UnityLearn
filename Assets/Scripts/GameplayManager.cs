using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateVisualData() 
    {
        Debug.Log($"Score: {score}");
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateVisualData();
    }

}
