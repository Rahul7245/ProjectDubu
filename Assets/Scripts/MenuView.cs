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
    [SerializeField] private Button continueButton;
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
        continueButton.onClick.AddListener(OnContinueButtonClicked);

    }

    void Start()
    {
        CalculateTotalCards();
       
    }
    void OnEnable()
    {
         InitContinueButton();
    }

    void InitContinueButton()
    {
        if (CheckForSavedGame())
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }
    private bool CheckForSavedGame()
    {
        if (SaveLoadSystem.HasSaveData())
        {
            return true;
        }
        return false;
    }
    private void CalculateTotalCards()
    {
        try{
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
        EventBusModel.playAudio.Value = AudioType.BUTTON;
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
        EventBusModel.playButtonClicked.Value = new GameStartData(int.Parse(rows.text),int.Parse(columns.text),false);

    }

    private void OnContinueButtonClicked()
    {
        EventBusModel.playAudio.Value = AudioType.BUTTON;
        EventBusModel.playButtonClicked.Value = new GameStartData(0, 0, true);

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
