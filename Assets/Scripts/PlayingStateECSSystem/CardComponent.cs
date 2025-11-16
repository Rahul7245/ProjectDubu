using UnityEngine;

[System.Serializable]
public struct CardComponent
{
    public int entityId;              // Unique identifier for this card instance
    public string matchId;            // ID used for matching (multiple cards share same matchId)
    public Sprite sprite;             // Visual representation
    public CardState state;           // Current state
    public bool isMatched;            // Has this card been matched?
    public int gridIndex;             // Position in grid

    public void AssignSprite(Sprite _sprite)
    {
        sprite = _sprite;
    }
}

