namespace MinefieldConsoleGame
{
    public interface IGameModel
    {
        void InitModel();
        bool IsGameOver();
        bool IsGameWon();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        string GetGameStatus();
    }
}