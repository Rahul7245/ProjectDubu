using System.Collections.Generic;

[System.Serializable]
public class CardGameData
{
    public List<CardComponent> cards = new List<CardComponent>();
    public List<int> flippedCardIds = new List<int>();
    public int matchesFound = 0;
    public int totalPairs = 0;
    public int turnsTaken = 0;
    public bool isProcessingMatch = false;
}