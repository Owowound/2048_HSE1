using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    private Cell cell;

    public TextMeshProUGUI valueText;

    private Image image;

    private GameField gameField;

    [SerializeField]
    private float speed;

    public void Init(Cell cell)
    {
        image = GetComponent<Image>();
        this.cell = cell;
        UpdateValue(cell.Value);
        UpdatePosition(cell.Position);

        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;
    }

    private void UpdateValue(int value)
    {
        if (value == 0)
        {
            valueText.text = "";
        }
        else
        {
            float t = Mathf.Clamp01(cell.Value / 11f);
            image.color = cell.GetNewColor(t);
            valueText.text = Mathf.Pow(2, value).ToString();
        }
        return;
    }

    private void UpdatePosition(Vector2Int pos)
    {
        Vector3 newPos = new Vector3(
            70 + pos.x * (140f + 40f / 3f) - 300,
            70 + pos.y * (140f + 40f / 3f) - 300,
            0
        );

        transform.localPosition = newPos;
        return;
    }
}
