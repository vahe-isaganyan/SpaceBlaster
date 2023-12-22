using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _textScore;

    // Start is called before the first frame update
    void Start()
    {
        _textScore.text = "Score: " + 0;

        
        Debug.Log("UIManager started");
    }

    public void UpdateScore(int playerScore)
    {
        _textScore.text = "Score: " + playerScore.ToString();

        
        Debug.Log("Score updated: " + playerScore);
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("UIManager Update");
    }
}

