using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardInitializationSystem
{
    public static void InitializeGame(CardGameData gameData, List<IconData> cardDataList, int rows, int cols)
    {
        gameData.cards.Clear();
        gameData.flippedCardIds.Clear();
        gameData.matchesFound = 0;
        gameData.isProcessingMatch = false;
        gameData.totalPairs = cardDataList.Count;
        gameData.rows = rows;
        gameData.columns = cols;

        // Create pairs and shuffle
        List<CardComponent> allCards = new List<CardComponent>();
        int entityId = 0;

        foreach (var cardData in cardDataList)
        {
            // Create first card of pair
            allCards.Add(CreateCardComponent(entityId++, cardData));
            // Create second card of pair
            allCards.Add(CreateCardComponent(entityId++, cardData));
        }

        // Shuffle cards
        allCards = allCards.OrderBy(x => Random.value).ToList();

        // Assign grid positions
        for (int i = 0; i < allCards.Count; i++)
        {
            CardComponent card = allCards[i];
            card.gridIndex = i;
            allCards[i] = card;
        }

        gameData.cards = allCards;
    }

    private static CardComponent CreateCardComponent(int entityId, IconData data)
    {
        return new CardComponent
        {
            entityId = entityId,
            matchId = data.id,
            sprite = data.icon,
            state = CardState.FaceDown,
            isMatched = false,
            gridIndex = -1
        };
    }

//If the sprite was not getting serialized this method should be called.
    public static void ReassignSprites(List<CardComponent> cards, IconHolder iconHolder)
    {
        for(int i=0;i<cards.Count;i++)
        {
            var iconData = iconHolder.iconDataList.FirstOrDefault((c)=>c.id==cards[i].matchId);
            cards[i].AssignSprite(iconData.icon);
        }
    } 
}