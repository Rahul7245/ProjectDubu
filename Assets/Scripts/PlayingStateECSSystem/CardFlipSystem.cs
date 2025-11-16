public static class CardFlipSystem
{
    public static bool TryFlipCard(CardGameData gameData, int cardEntityId)
    {
        if (gameData.isProcessingMatch) return false;
        if (gameData.flippedCardIds.Count >= 2) return false;

        int cardIndex = gameData.cards.FindIndex(c => c.entityId == cardEntityId);
        if (cardIndex == -1) return false;

        CardComponent card = gameData.cards[cardIndex];

        // Can't flip if already face up or matched
        if (card.state == CardState.FaceUp || card.isMatched) return false;

        // Flip the card
        card.state = CardState.FaceUp;
        gameData.cards[cardIndex] = card;
        gameData.flippedCardIds.Add(cardEntityId);

        return true;
    }
}