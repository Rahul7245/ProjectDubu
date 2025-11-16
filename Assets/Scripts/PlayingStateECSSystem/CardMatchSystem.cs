public static class CardMatchSystem
{
    public static bool CheckForMatch(CardGameData gameData)
    {
        if (gameData.flippedCardIds.Count != 2) return false;

        int card1Index = gameData.cards.FindIndex(c => c.entityId == gameData.flippedCardIds[0]);
        int card2Index = gameData.cards.FindIndex(c => c.entityId == gameData.flippedCardIds[1]);

        if (card1Index == -1 || card2Index == -1) return false;

        CardComponent card1 = gameData.cards[card1Index];
        CardComponent card2 = gameData.cards[card2Index];
        gameData.turnsTaken++;
        // Check if they match
        if (card1.matchId == card2.matchId)
        {
            // Match found!
            card1.state = CardState.Matched;
            card1.isMatched = true;
            card2.state = CardState.Matched;
            card2.isMatched = true;

            gameData.cards[card1Index] = card1;
            gameData.cards[card2Index] = card2;
            gameData.matchesFound++;

           // gameData.flippedCardIds.Clear();
            return true;
        }

        return false;
    }

    public static void FlipCardsBack(CardGameData gameData)
    {
        foreach (int entityId in gameData.flippedCardIds)
        {
            int cardIndex = gameData.cards.FindIndex(c => c.entityId == entityId);
            if (cardIndex == -1) continue;

            CardComponent card = gameData.cards[cardIndex];
            card.state = CardState.FaceDown;
            gameData.cards[cardIndex] = card;
        }

        gameData.flippedCardIds.Clear();
    }
}