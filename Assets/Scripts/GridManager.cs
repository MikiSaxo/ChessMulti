using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private const int CHESS_SIZE = 8;

    [SerializeField] private GameObject _pawnPrefab;
    [SerializeField] private Transform _boardParent;
    [SerializeField] private float _cellSize;

    [Header("Sprites")] [SerializeField] private Sprite[] _boardSquare;
    [SerializeField] private Sprite[] _white;
    [SerializeField] private Sprite[] _black;

    private Sprite[,] _boardGrid = new Sprite[CHESS_SIZE, CHESS_SIZE];

    private void Start()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        var count = 0;
        for (int y = 0; y < _boardGrid.GetLength(0); y++)
        {
            for (int x = 0; x < _boardGrid.GetLength(1); x++)
            {
                GameObject go = Instantiate(_pawnPrefab, _boardParent);
                count++;
                go.GetComponent<Pawn>().Init(_boardSquare[count % 2]);
                PlacePawn(go, x, y);
            }

            count++;
        }
    }

    private void PlacePawn(GameObject pawn, int x, int y)
    {
        pawn.transform.position = new Vector3(x * _cellSize, y * _cellSize, 0);
    }
}