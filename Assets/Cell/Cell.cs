using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public event Action<int> OnValueChanged;
    public event Action<Vector2Int> OnPositionChanged;

    private Color startColor = Color.white;
    [SerializeField]
        private Color endColor;

    // x - столбец, y - строка
    private Vector2Int position;
    public Vector2Int Position
    {
        get => position;
        set
        {
            position = value;
            OnPositionChanged?.Invoke(value);
        }
    }

    private int value;
    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    public void Initialize(Vector2Int position, int value)
    {
        Position = position;
        Value = value;
    }

    public void UpdateCell(Vector2Int pos, int value)
    {
        Position = position;
        Value = value;
    }

    public Color GetNewColor(float t)
    {
        return Color.Lerp(startColor, endColor, t);
    }

    public void MoveLeft()
    {
        Position = new Vector2Int(Position.x - 1, Position.y);
    }
    public void MoveRight()
    {
        Position = new Vector2Int(Position.x + 1, Position.y);
    }
    public void MoveDown()
    {
        Position = new Vector2Int(Position.x, Position.y - 1);
    }
    public void MoveUp()
    {
        Position = new Vector2Int(Position.x, Position.y + 1);
    }
}
