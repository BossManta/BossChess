using System;
using System.Collections.Generic;
using BossChess.Components;
using BossChess.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BossChess;

public class ChessUI
{
    public int BoardSize { get; set; } = 8;
    public SpriteBatch spriteBatch { get; set; }
    public TextureManager textureManager { get; set; }

    private event Action<Point> OnClick;
    private bool prevClickStatus = false;

    private Point pos;
    private int sizeInPixels;
    private int blockSize;

    public ChessUI(SpriteBatch sb, TextureManager tm)
    {
        spriteBatch = sb;
        textureManager = tm;
    }

    public void AddOnClickEventListner(Action<Point> toCall)
    {
        OnClick+=toCall;
    }

    public void Update(MouseState ms)
    {
        bool buttonClicked = ms.LeftButton==ButtonState.Pressed;
        if (buttonClicked && !prevClickStatus)
        {
            Point gridPos = new Point(ms.Position.X/blockSize, ms.Position.Y/blockSize);
            OnClick(gridPos);
        }
        prevClickStatus = buttonClicked;
    }

    public void SetUISpace(Point pos, int sizeInPixels)
    {
        this.pos = pos;
        this.sizeInPixels = sizeInPixels;
        blockSize = sizeInPixels/BoardSize;
    }

    public void LoadRequiredTextures()
    {
        textureManager.AddTexture("Pixel", "Pixel");

        textureManager.AddTexture("Pawn", "Pawn");
        textureManager.AddTexture("Rook", "Rook");
        textureManager.AddTexture("Knight", "Knight");
        textureManager.AddTexture("Biship", "Biship");
        textureManager.AddTexture("Queen", "Queen");
        textureManager.AddTexture("King", "King");
    }

    public void DrawBoard(IBoard board)
    {
        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y<BoardSize; y++)
            {
                //Draws Checker Pattern
                Rectangle pixRect = new Rectangle(x*blockSize, y*blockSize, blockSize, blockSize);
                Color pixCol = (y+x)%2==0?Color.White:Color.DarkSlateGray;
                spriteBatch.Draw(textureManager.GetTexture("Pixel"), pixRect, pixCol);


                //Draws Piece
                PrimitivePiece? p = board.GetPieceAt(new Point(x,y));
                if (p is null || ((PrimitivePiece)p).Type==PieceType.None) continue;

                PrimitivePiece piece = (PrimitivePiece)p;
                string pieceName = piece.Type.ToString();
                Color c = piece.IsWhite?Color.NavajoWhite:Color.Black;

                spriteBatch.Draw(textureManager.GetTexture(pieceName), pixRect, c);
            }
        }
    }

    public void DrawMoves(List<IMove> moves)
    {
        foreach (IMove mov in moves)
        {
            Rectangle pixRect = new Rectangle(mov.ActualMoves[0].to.X*blockSize, mov.ActualMoves[0].to.Y*blockSize, blockSize, blockSize);
            Color c = new Color()
            {
                R=100,
                G=0,
                B=100,
                A=100
            };
            spriteBatch.Draw(textureManager.GetTexture("Pixel"), pixRect, c);
        }
    }
}