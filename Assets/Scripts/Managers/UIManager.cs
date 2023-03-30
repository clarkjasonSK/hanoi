using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI textRingCount;
    [SerializeField] private TextMeshProUGUI txtMoveCount;

    private int ringCount;
    private int moveCount;

    void Start()
    {
        Instance = this;

        moveCount = 0;
        slider.onValueChanged.AddListener((sliderChange) =>
        {
            ringCount = (int)sliderChange;
            textRingCount.text = "Rings: " + sliderChange.ToString();
        });

        txtMoveCount.text = "Moves: 0";
    }

    // Update is called once per frame
    public void incrementCounter()
    {
        moveCount++;
        txtMoveCount.text = "Moves: " + moveCount;
    }
    public void resetCounter()
    {
        moveCount = 0;
        txtMoveCount.text = "Moves: " + moveCount;
    }

    public void resetGame()
    {
        // Debug.Log("sup " + ringCount);
        resetCounter();
        GameManager.Instance.setRingAmount(ringCount);
        GameManager.Instance.resetGame();
    }

    
}
