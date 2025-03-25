using TMPro;
using UnityEngine;

public enum Direction
{
    Down,
    Up, 
    Left, 
    Right
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
        private GameField gameField;

    [SerializeField]
    private GameObject endMenu;


    [SerializeField]
    private TextMeshProUGUI currentScoreValue;

    [SerializeField]
    private TextMeshProUGUI maxScoreValue;

    public int MaxScore { get; set; }

    private void Start()
    {
        StartGame();
        UpdateScore();
    }


    public void StartResetGame()
    {
        gameField.ClearField();
        gameField.GenerateField();
        UpdateScore();
        SaveDataToFile();
    }
    public void StartNewGame()
    {
        gameField.ClearField();
        gameField.GenerateField();
        UpdateScore();
        SetStartMaxScore();
        SaveDataToFile();
    }
    public void StartGame()
    {
        TryLoadFileData();
        SaveDataToFile();
    }



    public void UpdateScore()
    {
        int value = gameField.GetCurrentScore();
        currentScoreValue.text = value.ToString();
    }
    private void SetStartMaxScore()
    {
        MaxScore = 0;
        maxScoreValue.text = "0";
    }

    private void UpdateMaxScore()
    {
        int score = gameField.GetCurrentScore();
        MaxScore = Mathf.Max(MaxScore, score);
        maxScoreValue.text = MaxScore.ToString();
    }
    private void UpdateMaxScore(int score)
    {
        MaxScore = score;
        maxScoreValue.text = MaxScore.ToString();
    }



    private bool TryLoadFileData()
    {
        GameData data = FileWork.LoadData();
        UpdateMaxScore(data.MaxScore);

        gameField.LoadField(data.Positions, data.values);

        return true;
    }
    private void SaveDataToFile()
    {
        FileWork.SaveData(gameField.GetTakenCells(), gameField.GetValues(), MaxScore);
    }


    public void Move(Direction dir)
    {
        switch (dir) {
            case Direction.Down:
                MoveDown();
                break;
            case Direction.Up:
                MoveUp(); 
                break;
            case Direction.Left:
                MoveLeft();
                break;
            case Direction.Right:
                MoveRight();
                break;
        }
        UpdateScore();
        SaveDataToFile();
    }
    private void MoveUp()
    {
        for (int j = gameField.y - 1; j >= 0; j--)
        {
            for (int i = 0; i < gameField.x; i++)
            {
                gameField.MoveCell(i, j, Direction.Up);
            }
        }
        if (!NextStep())
        {
            EndGame();
        }
    }
    private void MoveDown()
    {
        for (int j = 0; j < gameField.y; j++)
        {
            for (int i = 0; i < gameField.x; i++)
            {
                gameField.MoveCell(i, j, Direction.Down);
            }
        }
        if (!NextStep())
        {
            EndGame();
        }
    }
    private void MoveLeft()
    {
        for (int i = 0; i < gameField.x; i++)
        {
            for (int j = 0; j < gameField.y; j++)
            {
                gameField.MoveCell(i, j, Direction.Left);
            }
        }
        if (!NextStep())
        {
            EndGame();
        }
    }
    private void MoveRight()
    {
        Debug.Log("Движение вправо");
        for (int i = gameField.x - 1; i >= 0; i--)
        {
            for (int j = 0; j < gameField.y; j++)
            {
                gameField.MoveCell(i, j, Direction.Right);
            }
        }
        if (!NextStep())
        {
            EndGame();
        }
    }


    private bool NextStep()
    {
        return gameField.GenerateCell();
    }

    private void EndGame()
    {
        UpdateMaxScore();
        endMenu.gameObject.SetActive(true);
    }


}
