using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private TMP_InputField rows;
    [SerializeField] private TMP_InputField columns;
    [SerializeField] private TextMeshProUGUI totalCards;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI warningText;
    void Awake()
    {
        rows.onSubmit.AddListener((_) => CalculateTotalCards());
        rows.onDeselect.AddListener((value) => { CalculateTotalCards(); });
        columns.onSubmit.AddListener((_) => CalculateTotalCards());
        columns.onDeselect.AddListener((value) => { CalculateTotalCards(); });
        playButton.onClick.AddListener(OnPlayButtonClicked);
        rows.onSelect.AddListener((_)=> {HideShowDisplay(false);});
        columns.onSelect.AddListener((_)=> {HideShowDisplay(false);});

    }

    void Start()
    {
        CalculateTotalCards();
    }

    private void CalculateTotalCards()
    {
        try{
        Debug.Log("Calculate called");
        int x = int.Parse(rows.text);
        int y = int.Parse(columns.text);
        totalCards.text = (x * y).ToString();
        }
        catch(Exception e)
            {
                 totalCards.text = "Invalid";
            }
    }

    private void OnPlayButtonClicked()
    {
        if (!int.TryParse(totalCards.text,out int _totalCards))
        {
            DisplayWarning("Invalid");
        }
        if (_totalCards % 2 == 1)
        {
           DisplayWarning("Total Cards cannot be odd");
           return;
        }
        if (_totalCards == 0)
        {
           DisplayWarning("Total Cards cannot be 0");
           return;
        }
        EventBusModel.playButtonClicked.Value = (int.Parse(rows.text),int.Parse(columns.text));

    }

    private void DisplayWarning(string warning)
    {
        warningText.text = warning;
        HideShowDisplay(true);
        
    }

    private void HideShowDisplay(bool enable)
    {
        warningText.gameObject.SetActive(enable);
    }

}
