using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public int x;
    public int y;
    public GameObject cellPrefab;

    private bool[,] taken;
    private GameObject[,] cells;

    private RectTransform parent;


    private void Start()
    {
        taken = new bool[x, y];
        cells = new GameObject[x, y];
        parent = GetComponent<RectTransform>();
    }

    public void LoadField(bool[,] positions, int[,] values)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (positions[i, j])
                {
                    CreateCell(new Vector2Int(i, j), values[i, j]);
                }
            }
        }
    }
    public void GenerateField()
    {
        CreateCell();
        CreateCell();
    }
    public bool GenerateCell()
    {
        return CreateCell();
    }





    public void ClearField()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y ; j++)
            {
                if (taken[i, j])
                {
                    Destroy(cells[i, j].gameObject);
                    cells[i, j] = null;
                    taken[i, j] = false;
                }
            }
        }
    }


    public Vector2Int GetEmptyPosition()
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>();

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (!taken[i, j])
                {
                    emptyPositions.Add(new Vector2Int(i, j));
                }
            }
        }

        if (emptyPositions.Count > 0)
        {
            return emptyPositions[Random.Range(0, emptyPositions.Count - 1)];
        }
        return new Vector2Int(-1, -1);
    }
    public bool CreateCell()
    {
        Vector2Int pos = GetEmptyPosition();

        if (pos.x == -1)
        {
            Debug.Log("Пустых позиций нет");
            return false;
        }
        taken[pos.x, pos.y] = true;

        int value = Random.Range(0, 100) < 80 ? 1 : 2;

        GameObject newCell = Instantiate(cellPrefab, parent);
        Cell cellComponent = newCell.GetComponent<Cell>();

        newCell.GetComponent<CellView>().Init(cellComponent);
        cellComponent.Initialize(pos, value);
        cells[pos.x, pos.y] = newCell;

        return true;
    }

    public bool CreateCell(Vector2Int pos, int value)
    {
        taken[pos.x, pos.y] = true;
        GameObject newCell = Instantiate(cellPrefab, parent);
        Cell cellComponent = newCell.GetComponent<Cell>();

        newCell.GetComponent<CellView>().Init(cellComponent);
        cellComponent.Initialize(pos, value);
        cells[pos.x, pos.y] = newCell;

        return true;
    }

    public void MoveCell(int i, int j, Direction dir)
    {
        if (!taken[i, j])
        {
            return;
        }
        switch (dir)
        {
            case Direction.Up:
                while (j < 3)
                {
                    j++;
                    if (taken[i, j])
                    {
                        TryToMerge(i, j, i, j - 1);
                        return;
                    }
                    cells[i, j - 1].GetComponent<Cell>().MoveUp();
                    cells[i, j] = cells[i, j - 1];
                    cells[i, j - 1] = null;
                    taken[i, j] = true;
                    taken[i, j - 1] = false;
                }
                break;
            case Direction.Down:
                while (j > 0)
                {
                    j--;
                    if (taken[i, j])
                    {
                        TryToMerge(i, j, i, j + 1);
                        return;
                    }
                    cells[i, j + 1].GetComponent<Cell>().MoveDown();
                    cells[i, j] = cells[i, j + 1];
                    cells[i, j + 1] = null;
                    taken[i, j] = true;
                    taken[i, j + 1] = false;
                }
                break;
            case Direction.Left:
                while (i > 0)
                {
                    i--;
                    if (taken[i, j])
                    {
                        TryToMerge(i, j, i + 1, j);
                        return;
                    }
                    cells[i + 1, j].GetComponent<Cell>().MoveLeft();
                    cells[i, j] = cells[i + 1, j];
                    cells[i + 1, j] = null;
                    taken[i, j] = true;
                    taken[i + 1, j] = false;
                }
                break;
            case Direction.Right:
                while (i < 3)
                {
                    i++;
                    if (taken[i, j])
                    {
                        TryToMerge(i, j, i - 1, j);
                        return;
                    }
                    cells[i - 1, j].GetComponent<Cell>().MoveRight();
                    cells[i, j] = cells[i - 1, j];
                    cells[i - 1, j] = null;
                    taken[i, j] = true;
                    taken[i - 1, j] = false;
                }
                break;
        }
    }

    private void TryToMerge(int i, int j, int i1, int j1)
    {
        Cell cell1 = cells[i, j].GetComponent<Cell>();
        Cell cell2 = cells[i1, j1].GetComponent<Cell>();

        if (cell1.Value == cell2.Value)
        {
            cell1.Value = cell1.Value + 1;
            cells[i1, j1] = null;
            taken[i1, j1] = false;
            Destroy(cell2.gameObject);
        }
    }


    public bool[,] GetTakenCells()
    {
        return taken;
    }
    public int[,] GetValues()
    {
        int[,] result = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (taken[i, j])
                {
                    result[i, j] = cells[i, j].GetComponent<Cell>().Value;
                }
                else
                {
                    result[i, j] = 0;
                }
            }
        }
        return result;
    }
    public int GetCurrentScore()
    {
        int result = 0;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (taken[i, j])
                {
                    result += (int)Mathf.Pow(2, cells[i, j].GetComponent<Cell>().Value);
                }
            }
        }
        return result;
    }
}
