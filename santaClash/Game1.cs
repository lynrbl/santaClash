using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace santaClash;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Point gameSize = new Point(1250, 1000);
    private Texture2D background;
    private Texture2D billet;
    private Texture2D leprechaun;
    private Texture2D santaCash;
    private Player player1;
    private Player player2;
    private Santa santa;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;


    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = gameSize.X;
        _graphics.PreferredBackBufferHeight = gameSize.Y;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        background = Content.Load<Texture2D>("background");
        billet = Content.Load<Texture2D>("dessin-billet-banque-euro-02");
        leprechaun = Content.Load<Texture2D>("leprechaun");
        santaCash = Content.Load<Texture2D>("santaCash");
        // TODO: use this.Content to load your game content here

        player1 = new Player(_graphics, leprechaun, new Vector2(10, 10), new Vector2(350, 465));
        player2 = new Player(_graphics, leprechaun, new Vector2(10, 10), new Vector2(850, 465));

        santa = new Santa(_graphics, santaCash, new Vector2(200, 200), new Vector2(1250, 1000) / 2);
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        keyPressedPlayer();
        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;


        player1.Update(gameTime, screenWidth, screenHeight);
        player2.Update(gameTime, screenWidth, screenHeight);
        santa.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Rectangle(0, 0, gameSize.X, gameSize.Y), Color.White);
        _spriteBatch.Draw(billet, new Rectangle(600, 330, 50, 50), Color.White);

        santa.Draw(_spriteBatch);
        player1.Draw(_spriteBatch);
        player2.Draw(_spriteBatch);


        _spriteBatch.End();
        base.Draw(gameTime);
    }


    // public void getKeyboardState()
    // {

    // }

    public void keyPressedPlayer()
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
        {
            player1.position.Y += player1.vitesse.Y;
            player1.position.X += player1.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
        {
            player1.position.Y += player1.vitesse.Y;
            player1.position.X -= player1.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
        {
            player1.position.Y -= player1.vitesse.Y;
            player1.position.X += player1.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
        {
            player1.position.Y -= player1.vitesse.Y;
            player1.position.X -= player1.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.D))
        {
            player1.position.X += player1.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.A))
        {
            player1.position.X -= player1.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.W))
        {
            player1.position.Y -= player1.vitesse.Y;
        }
        else if (keyboardState.IsKeyDown(Keys.S))
        {
            player1.position.Y += player1.vitesse.Y;
        }

        if (keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Right))
        {
            player2.position.Y -= player2.vitesse.Y;
            player2.position.X += player2.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Left))
        {
            player2.position.Y -= player2.vitesse.Y;
            player2.position.X -= player2.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyDown(Keys.Right))
        {
            player2.position.Y += player2.vitesse.Y;
            player2.position.X += player2.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyDown(Keys.Left))
        {
            player2.position.Y += player2.vitesse.Y;
            player2.position.X -= player2.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            player2.position.X += player2.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.Left))
        {
            player2.position.X -= player2.vitesse.X;
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            player2.position.Y += player2.vitesse.Y;
        }
        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            player2.position.Y -= player2.vitesse.Y;
        }
    }
}
