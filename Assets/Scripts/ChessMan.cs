using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using UnityEngine;

public class ChessMan : MonoBehaviour
{

    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;

    public Sprite blackQueen, blackKnigth, blackBishop, blackKing, blackRook, blackPawn;
    public Sprite whiteQueen, whiteKnigth, whiteBishop, whiteKing, whiteRook, whitePawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "blackRook": this.GetComponent<SpriteRenderer>().sprite = blackRook; player = "black"; break;
            case "blackPawn": this.GetComponent<SpriteRenderer>().sprite = blackPawn; player = "black"; break;
            case "blackKing": this.GetComponent<SpriteRenderer>().sprite = blackKing; player = "black"; break;
            case "blackQueen": this.GetComponent<SpriteRenderer>().sprite = blackQueen; player = "black"; break;
            case "blackKnigth": this.GetComponent<SpriteRenderer>().sprite = blackKnigth; player = "black"; break;
            case "blackBishop": this.GetComponent<SpriteRenderer>().sprite = blackBishop; player = "black"; break;

            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; player = "white"; break;
            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = whiteKing; player = "white"; break;
            case "whiteRook": this.GetComponent<SpriteRenderer>().sprite = whiteRook; player = "white"; break;
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen; player = "white"; break;
            case "whiteKnigth": this.GetComponent<SpriteRenderer>().sprite = whiteKnigth; player = "white"; break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop; player = "white"; break;

        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 1.0f;
        y *= 0.98f;

        x += -3.5f;
        y += -3.5f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetPositionBoard(int x, int y)
    {
        xBoard = x;
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            IntiateMovePlates();
        }
    }

    public void IntiateMovePlates()
    {
        switch (this.name)
        {
            case "blackQueen":
            case "whiteQueen":
                LineaMovePlate(1, 0);
                LineaMovePlate(0, 1);
                LineaMovePlate(1, 1);
                LineaMovePlate(-1, 0);
                LineaMovePlate(0, -1);
                LineaMovePlate(-1, -1);
                LineaMovePlate(-1, 1);
                LineaMovePlate(1, -1);
                break;
            case "blackKnight":
            case "whiteKnight":
                LMovePlate();
                break;
            case "blackBishop":
            case "whiteBishop":
                LineaMovePlate(1, 1);
                LineaMovePlate(-1, -1);
                LineaMovePlate(-1, 1);
                LineaMovePlate(1, -1);
                break;
            case "blackKing":
            case "whiteKing":
                KingMovePlate();
                break;
            case "blackRook":
            case "whiteRook":
                LineaMovePlate(1, 0);
                LineaMovePlate(0, 1);
                LineaMovePlate(-1, 0);
                LineaMovePlate(0, -1);
                break;
            case "blackPawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    private void LineaMovePlate(int xIncremente, int yIncremente)
    {
        Game game = controller.GetComponent<Game>();

        int x = xBoard + xIncremente;
        int y = yBoard + yIncremente;

        while (game.PositionOnBoard(x, y) && game.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncremente;
            y += yIncremente;
        }

        if (game.PositionOnBoard(x, y) && game.GetPosition(x, y).GetComponent<ChessMan>().player != player)
        {
            MovePlateAttackSpawn(x, y);

        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }


    public void KingMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);

    }

    public void PointMovePlate(int x, int y)
    {
        Game game = controller.GetComponent<Game>();

        if (game.PositionOnBoard(x, y))
        {
            GameObject chessPiece = game.GetPosition(x, y);

            if (chessPiece == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (chessPiece.GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game game = controller.GetComponent<Game>();

        if (game.PositionOnBoard(x, y))
        {
            if (game.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (game.PositionOnBoard(x + 1, y) && game.GetPosition(x + 1, y) != null && game.GetPosition(x + 1, y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (game.GetPosition(x - 1, y) != null && game.GetPosition(x - 1, y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }


    private void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.0f;
        y *= 0.98f;

        x += -3.5f;
        y += -3.5f;

        GameObject movePlateObj = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate movePlateScript = movePlateObj.GetComponent<MovePlate>();
        movePlateScript.SetReference(this.gameObject);
        movePlateScript.SetCoords(matrixX, matrixY);

    }
    private void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.0f;
        y *= 0.98f;

        x += -3.5f;
        y += -3.5f;

        GameObject movePlateObj = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate movePlateScript = movePlateObj.GetComponent<MovePlate>();
        movePlateScript.attack = true;
        movePlateScript.SetReference(this.gameObject);
        movePlateScript.SetCoords(matrixX, matrixY);
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }
}
