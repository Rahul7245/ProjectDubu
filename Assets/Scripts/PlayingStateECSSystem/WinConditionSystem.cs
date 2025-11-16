public static class WinConditionSystem
{
    public static bool IsGameComplete(CardGameData gameData)
    {
        return gameData.matchesFound >= gameData.totalPairs;
    }

    public static int GetMatchedPairs(CardGameData gameData)
    {
        return gameData.matchesFound;
    }

    public static float GetCompletionPercentage(CardGameData gameData)
    {
        if (gameData.totalPairs == 0) return 0f;
        return (float)gameData.matchesFound / gameData.turnsTaken * 100f;
    }
}