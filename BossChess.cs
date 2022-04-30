using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using BossChess.Interfaces;
using BossChess.Components;
using BossChess.Components.AI;
using System.Collections.Generic;

namespace BossChess;

public class BossChess : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Point WindowSize = new Point(800,800);

    private TextureManager tm;

    private ChessUI chessUI;

    private IBoard currentBoard = new Board();
    private List<IMove> currentMoves = new List<IMove>();

    public BossChess()
    {
        _graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = WindowSize.X;
        _graphics.PreferredBackBufferHeight = WindowSize.Y;
        _graphics.ApplyChanges();



        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        tm = new TextureManager(_spriteBatch, Content);
        chessUI = new ChessUI(_spriteBatch, tm);
        chessUI.SetUISpace(new Point(0,0), WindowSize.Y);
        chessUI.AddOnClickEventListner(OnClick);
        
        chessUI.LoadRequiredTextures();
    }

    private void OnClick(Point p)
    {
        PrimitivePiece pieceAtClick = currentBoard.GetPieceAt(p);
        if (pieceAtClick.Type!=PieceType.None && pieceAtClick.IsWhite==currentBoard.isWhitesTurn)
        {
            currentMoves = BoardGenerator.GenerateValidMovesAt(currentBoard, p);
        }
        else
        {
            foreach (IMove m in currentMoves)
            {
                Point moveLoc = m.ActualMoves[0].to;
                if (moveLoc==p)
                {
                    currentBoard = BoardGenerator.GenerateNewBoardWithMove(currentBoard, m);
                    currentMoves = new List<IMove>();
                    MinMaxAI ai = new MinMaxAI();
                    ai.Init(currentBoard, 3);
                    currentBoard = BoardGenerator.GenerateNewBoardWithMove(currentBoard, ai.GetBestMove());
                    break;
                }
            }
        }
    }

    protected override async void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        chessUI.Update(Mouse.GetState());

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

        chessUI.DrawBoard(currentBoard);

        chessUI.DrawMoves(currentMoves);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
