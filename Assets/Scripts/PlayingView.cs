using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingView : MonoBehaviour
{
    [SerializeField]private CardGridResizer cardGridResizer;
    [SerializeField]private IconHolder iconHolder;
    [SerializeField]private CardView cardViewPrefab;
    [SerializeField]private Transform cardParentGrid;
    [SerializeField]private TextMeshProUGUI matchedText;
    [SerializeField]private TextMeshProUGUI turnsTakenText;
    [SerializeField] private Button homeButton;
    private List<CardView> cardViews = new List<CardView>();
    private bool isCheckingMatch = false;
    private CardGameData gameData;
    void Awake()
    {
        EventBusModel.gameStart.Subscribe(InitGameView);
    }
    void Start()
    {
        homeButton.onClick.AddListener(OnHomeButtonClicked);
    }
      public void OnHomeButtonClicked()
    {
        EventBusModel.playAudio.Value = AudioType.BUTTON;
        EventBusModel.homeButton.Invoke();
    }
    void OnDestroy()
    {
        EventBusModel.gameStart.Unsubscribe(InitGameView);
        homeButton.onClick.RemoveAllListeners();
    }

    private void InitGameView()
    {
        
        var (rows,columns) = EventBusModel.gameStart.Value;
        cardGridResizer.Init(rows,columns);
        InitSession(rows,columns);
    }

    private void InitSession(int rows, int columns)
    {
        gameData = new CardGameData();
        var iconList = iconHolder.FetchIcons((rows*columns)/2);
        foreach (var view in cardViews)
        {
            if (view != null) Destroy(view.gameObject);
        }
        cardViews.Clear();

        // Initialize game data using system
        CardInitializationSystem.InitializeGame(gameData, iconList, rows, columns);

        // Create views for each card
        foreach (var cardComponent in gameData.cards)
        {
            CardView view = Instantiate(cardViewPrefab,cardParentGrid);
            view.Initialize(cardComponent.entityId, this,cardComponent.sprite);
            view.UpdateView(cardComponent,2f);
            cardViews.Add(view);
        }
        UpdateScores();

    }


    public void OnCardClicked(int cardEntityId)
    {
        if (isCheckingMatch) return;

        // Try to flip card using system
        bool flipped = CardFlipSystem.TryFlipCard(gameData, cardEntityId);
        
        if (flipped)
        {
            // Update view
            UpdateCardView(cardEntityId);

            // Check if we need to process a match
            if (gameData.flippedCardIds.Count == 2)
            {
                StartCoroutine(ProcessMatchCheck());
            }
        }
    }
     private void UpdateCardView(int cardEntityId)
    {
        int cardIndex = gameData.cards.FindIndex(c => c.entityId == cardEntityId);
        if (cardIndex == -1) return;

        CardComponent card = gameData.cards[cardIndex];
        CardView view = cardViews.Find(v => v.EntityId == cardEntityId);
        
        if (view != null)
        {
            view.UpdateView(card);
        }
    }

     private System.Collections.IEnumerator ProcessMatchCheck()
    {
        isCheckingMatch = true;
        gameData.isProcessingMatch = true;

        yield return new WaitForSeconds(Constants.matchCheckDelay);

        // Check for match using system
        bool isMatch = CardMatchSystem.CheckForMatch(gameData);

        if (isMatch)
        {
            EventBusModel.playAudio.Value = AudioType.SCORE;
            // Update matched card views
            foreach (int id in gameData.flippedCardIds)
            {
                UpdateCardView(id);
            }
            gameData.flippedCardIds.Clear();
            // Check win condition
            if (WinConditionSystem.IsGameComplete(gameData))
            {
                EventBusModel.gameOver.Value= 
                WinConditionSystem.GetCompletionPercentage(gameData);
            }
        }
        else
        {
            // Flip cards back using system
            CardMatchSystem.FlipCardsBack(gameData);

            // Update views
            foreach (var card in gameData.cards)
            {
                if (card.state == CardState.FaceDown)
                {
                    UpdateCardView(card.entityId);
                }
            }
        }

        isCheckingMatch = false;
        gameData.isProcessingMatch = false;
        UpdateScores();
    }

    void UpdateScores()
    {
        matchedText.text = gameData.matchesFound.ToString();
        turnsTakenText.text = gameData.turnsTaken.ToString();
    } 
}
