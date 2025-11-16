
public struct GameStartData
{
    public int rows;
    public int columns;
    public bool continueGame;
    public GameStartData(int _rows, int _columns, bool _continueGame)
    {
        rows = _rows;
        columns = _columns;
        continueGame = _continueGame;
    }
}