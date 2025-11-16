using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button homeButton;

    void Awake()
    {
        EventBusModel.score.Subscribe(ShowScore);
    }
    void Start()
    {
        homeButton.onClick.AddListener(OnHomeButtonClicked);
    }
    public void ShowScore()
    {
        scoreText.text = EventBusModel.score.Value.ToString("F2");
    }
    void OnDestroy()
    {
        homeButton.onClick.RemoveAllListeners();
         EventBusModel.score.Unsubscribe(ShowScore);
    }

    public void OnHomeButtonClicked()
    {
        EventBusModel.homeButton.Invoke();
    }
}
