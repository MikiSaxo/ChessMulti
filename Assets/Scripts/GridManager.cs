using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum ChessColor
{
    White = 0,
    Black = 1,
    Nothing = 2
}

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    private const int CHESS_SIZE = 8;

    [Header("Setup")] [SerializeField] private GameObject _pawnPrefab;
    [SerializeField] private Transform _boardParent;
    [SerializeField] private Transform _pawnParent;
    [SerializeField] private float _cellSize;
    [SerializeField] private GameObject _hoverCursor;
    [SerializeField] private PawnCharac[] _pawnCharacs;

    [Header("Sprites")] [SerializeField] private Sprite[] _boardSquare;
    [SerializeField] private Sprite[] _white;
    [SerializeField] private Sprite[] _black;

    private SpriteRenderer[,] _placementGrid = new SpriteRenderer[CHESS_SIZE, CHESS_SIZE];
    private GameObject[,] _pawnGrid = new GameObject[CHESS_SIZE, CHESS_SIZE];
    private string _filePath;
    private string[] _chessMap;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ReadFile();
        InitAllGrids();
    }

    private void ReadFile()
    {
        _filePath = $"chessMap.txt";
        string path = Path.Combine(Application.streamingAssetsPath, _filePath);
        _chessMap = File.ReadAllLines(path);
    }

    private void InitAllGrids()
    {
        var count = 0;
        for (int y = 0; y < _pawnGrid.GetLength(0); y++)
        {
            for (int x = 0; x < _pawnGrid.GetLength(1); x++)
            {
                GameObject board = Instantiate(_pawnPrefab, _boardParent);
                count++;
                board.GetComponent<Pawn>().InitBoard(_boardSquare[count % 2]);
                PlacePawn(board, x, y);

                AddHover(x, y, count);

                AddPawn(x, y);
            }

            count++;
        }
    }

    private void AddHover(int x, int y, int count)
    {
        GameObject hover = Instantiate(new GameObject(), _boardParent);
        var hoverSpr = hover.AddComponent<SpriteRenderer>();
        hoverSpr.sprite = _boardSquare[count % 2];
        hoverSpr.color = Color.magenta;
        hoverSpr.enabled = false;
        _placementGrid[x, y] = hoverSpr;
        PlacePawn(hover, x, y);
    }

    private void AddPawn(int x, int y)
    {
        ChessColor chessColor = ChessColor.White;
        if (y > 4)
            chessColor = ChessColor.Black;

        char letter = _chessMap[y][x];


        foreach (var pawnCharac in _pawnCharacs)
        {
            if (pawnCharac.Letter == letter)
            {
                GameObject pawn = Instantiate(_pawnPrefab, _pawnParent);
                pawn.GetComponent<Pawn>().InitChess(pawnCharac.Sprites[(int)chessColor], chessColor,
                    pawnCharac.PossibleMovements, new Vector2Int(x, y));
                _pawnGrid[x, y] = pawn;
                PlacePawn(pawn, x, y);
                break;
            }
        }
    }

    private void Update()
    {
        UpdateHoverPos();
    }

    private void UpdateHoverPos()
    {
        var mousePos = GetMouseWorldPos();
        var boardPosition = GetBoardPos(mousePos.x, mousePos.y);
        _hoverCursor.transform.position = GetWorldPosition(boardPosition.x, boardPosition.y);
    }

    private void PlacePawn(GameObject pawn, int x, int y)
    {
        Vector2 newPos = GetWorldPosition(x, y);

        pawn.transform.position = new Vector3(newPos.x, newPos.y, 0);
    }

    public void DrawPossibleMovement(Vector2Int[] coords, Vector2Int pawnCoord)
    {
        foreach (var coord in coords)
        {
            print("draw");
            _placementGrid[coord.x + pawnCoord.x, coord.y + pawnCoord.y].enabled = true;
        }
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector2(x * _cellSize, y * _cellSize);
    }

    private Vector2Int GetBoardPos(float x, float y)
    {
        float newX = Mathf.Floor((x + _cellSize / 2) / _cellSize);
        float newY = Mathf.Floor((y + _cellSize / 2) / _cellSize);
        return new Vector2Int((int)newX, (int)newY);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }

    private Vector3 GetMouseWorldPosWithZ(Vector3 screenPos, Camera worldCam)
    {
        Vector3 worldPos = worldCam.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}