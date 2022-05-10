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

    private Point WindowSize = new Point(512,512);
    private bool hasPlayer = false;

    private TextureManager tm;

    private ChessUI chessUI;

    private HumanPlayer humanPlayer = new HumanPlayer();
    
    private AIPlayer aiPlayer = new AIPlayer(2);
    private AIPlayer aiPlayer2 = new AIPlayer();

    private GameManager gm;

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

        gm = new GameManager(hasPlayer?humanPlayer:aiPlayer2, aiPlayer);
        //gm = new GameManager(aiPlayer, humanPlayer);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        tm = new TextureManager(_spriteBatch, Content);
        chessUI = new ChessUI(_spriteBatch, tm);
        chessUI.SetUISpace(new Point(0,0), WindowSize.Y);

        chessUI.AddOnClickEventListner(humanPlayer.RegisterClick);
        
        chessUI.LoadRequiredTextures();
    }

    protected override async void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // gm.AuthorizeNextPlayer();
        chessUI.Update(Mouse.GetState());
        gm.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

        chessUI.DrawBoard(gm.currentBoard);
        chessUI.DrawMoves(humanPlayer.currentMoves);     

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}