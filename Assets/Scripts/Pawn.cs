using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ChessColor _pawnColor;
    [SerializeField] private Vector2Int[] _possibleMovement;
    private Vector2Int Coord;

    public void InitBoard(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void InitChess(Sprite newSprite, ChessColor chessColor, Vector2Int[] possibleMov, Vector2Int coord)
    {
        _pawnColor = chessColor;
        _spriteRenderer.sortingOrder = 2;
        _possibleMovement = possibleMov;
        _spriteRenderer.sprite = newSprite;
        Coord = coord;
    }

    private void OnMouseDown()
    {
        DrawMovement();
    }

    private void DrawMovement()
    {
        print("down");
        GridManager.Instance.DrawPossibleMovement(_possibleMovement, Coord);
    }
}
