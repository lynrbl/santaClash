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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = gameSize.X;
        _graphics.PreferredBackBufferHeight = gameSize.Y;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here


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

        player1 = new Player(_graphics,leprechaun, new Vector2(10, 10), new Vector2(5, 5));
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

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
        
        player1.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Rectangle(0, 0, gameSize.X, gameSize.Y), Color.White);
        _spriteBatch.Draw(billet, new Rectangle(600, 330, 50, 50), Color.White);

        _spriteBatch.Draw(leprechaun, new Rectangle(350, 465, 75, 75), Color.White);
        _spriteBatch.Draw(leprechaun, new Rectangle(850, 465, 75, 75), Color.White);
        _spriteBatch.Draw(santaCash, new Rectangle(525, 400, 200, 200), Color.White);

        player1.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }


    // public void getKeyboardState()
    // {
        
    // }
}
