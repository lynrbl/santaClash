using System;
using System.Collections.Generic;
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

    // liste des ennemis (billets)
    private List<Enemy> enemies = new List<Enemy>();
    private Random random = new Random();
    private float spawnTimer = 0f;
    private float spawnInterval = 2f; // temps entre chaque vague
    private int enemiesPerWave = 3; // nombre de billets par vague

    // game over
    private bool gameOver = false;
    private SpriteFont font;
    private Texture2D pixel; // texture 1x1 pour dessiner les rectangles

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
        font = Content.Load<SpriteFont>("SpriteFont");
        // TODO: use this.Content to load your game content here

        // créer une texture blanche 1x1 pour dessiner des rectangles
        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        player1 = new Player(_graphics, leprechaun, new Vector2(10, 10), new Vector2(350, 465));
        player2 = new Player(_graphics, leprechaun, new Vector2(10, 10), new Vector2(850, 465));

        santa = new Santa(_graphics, santaCash, new Vector2(200, 200), new Vector2(1250, 1000) / 2);
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        // si game over, on arrête la logique
        if (gameOver)
        {
            // appuyer sur R pour recommencer
            if (keyboardState.IsKeyDown(Keys.R))
            {
                RestartGame();
            }
            base.Update(gameTime);
            return;
        }

        // TODO: Add your update logic here

        keyPressedPlayer();
        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;


        player1.Update(gameTime, screenWidth, screenHeight);
        player2.Update(gameTime, screenWidth, screenHeight);
        santa.Update();

        // spawn des ennemis
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnTimer >= spawnInterval)
        {
            SpawnWave();
            spawnTimer = 0f;
        }

        // update des ennemis
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime, santa.position);
        }

        // collisions joueurs avec ennemis
        CheckCollisions();

        // collision ennemis avec santa
        CheckSantaCollisions();

        // vérifier si game over
        if (santa.BarrePleine())
        {
            gameOver = true;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Rectangle(0, 0, gameSize.X, gameSize.Y), Color.White);

        santa.Draw(_spriteBatch);
        player1.Draw(_spriteBatch);
        player2.Draw(_spriteBatch);

        // dessiner les ennemis
        foreach (var enemy in enemies)
        {
            enemy.Draw(_spriteBatch);
        }

        // dessiner la barre d'argent du Père Noël
        DrawMoneyBar();

        // dessiner les scores
        DrawScores();

        // dessiner game over
        if (gameOver)
        {
            DrawGameOver();
        }

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

    private void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            // spawn sur les bords de l'écran
            Vector2 spawnPos = GetRandomSpawnPosition();
            Enemy enemy = new Enemy(_graphics, billet, new Vector2(2, 2), spawnPos);
            enemies.Add(enemy);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        int side = random.Next(4); // 0=haut, 1=bas, 2=gauche, 3=droite
        float x = 0;
        float y = 0;

        switch (side)
        {
            case 0: // haut
                x = random.Next(0, gameSize.X);
                y = -50;
                break;
            case 1: // bas
                x = random.Next(0, gameSize.X);
                y = gameSize.Y + 50;
                break;
            case 2: // gauche
                x = -50;
                y = random.Next(0, gameSize.Y);
                break;
            case 3: // droite
                x = gameSize.X + 50;
                y = random.Next(0, gameSize.Y);
                break;
        }
        return new Vector2(x, y);
    }

    private void CheckCollisions()
    {
        Rectangle player1Bounds = player1.GetBounds();
        Rectangle player2Bounds = player2.GetBounds();

        foreach (var enemy in enemies)
        {
            if (!enemy.isAlive) continue;

            Rectangle enemyBounds = enemy.GetBounds();

            // collision avec player1
            if (player1Bounds.Intersects(enemyBounds))
            {
                enemy.isAlive = false;
                player1.score++;
            }
            // collision avec player2
            else if (player2Bounds.Intersects(enemyBounds))
            {
                enemy.isAlive = false;
                player2.score++;
            }
        }

        // nettoyer les ennemis morts
        enemies.RemoveAll(e => !e.isAlive);
    }

    private void CheckSantaCollisions()
    {
        Rectangle santaBounds = santa.GetBounds();

        foreach (var enemy in enemies)
        {
            if (!enemy.isAlive) continue;

            Rectangle enemyBounds = enemy.GetBounds();

            if (santaBounds.Intersects(enemyBounds))
            {
                enemy.isAlive = false;
                santa.AjouterArgent(10f); // augmente la barre d'argent
            }
        }

        // nettoyer les ennemis morts
        enemies.RemoveAll(e => !e.isAlive);
    }

    private void DrawMoneyBar()
    {
        int barX = gameSize.X / 2 - 150;
        int barY = 20;
        int barWidth = 300;
        int barHeight = 30;

        // bordure noire
        _spriteBatch.Draw(pixel, new Rectangle(barX - 3, barY - 3, barWidth + 6, barHeight + 6), Color.Black);

        // barre de fond (gris foncé)
        _spriteBatch.Draw(pixel, new Rectangle(barX, barY, barWidth, barHeight), Color.DarkGray);

        // barre d'argent (dégradé rouge vers vert selon le remplissage)
        int fillWidth = (int)(barWidth * (santa.argentActuel / santa.argentMax));
        float ratio = santa.argentActuel / santa.argentMax;
        Color barColor = new Color(
            (int)(255 * ratio),      // rouge augmente
            (int)(100 * (1 - ratio)), // vert diminue
            0
        );
        _spriteBatch.Draw(pixel, new Rectangle(barX, barY, fillWidth, barHeight), barColor);

        // icône de billet à côté de la barre
        _spriteBatch.Draw(billet, new Rectangle(barX - 60, barY - 10, 50, 50), Color.White);
    }

    private void DrawScores()
    {
        int scoreBoxWidth = 200;
        int scoreBoxHeight = 50;
        int margin = 20;

        // Player 1 score (en bas à gauche)
        int p1X = margin;
        int p1Y = gameSize.Y - scoreBoxHeight - margin;

        // fond du score player 1
        _spriteBatch.Draw(pixel, new Rectangle(p1X - 2, p1Y - 2, scoreBoxWidth + 4, scoreBoxHeight + 4), Color.Black);
        _spriteBatch.Draw(pixel, new Rectangle(p1X, p1Y, scoreBoxWidth, scoreBoxHeight), new Color(50, 50, 100));

        // barre de score player 1 (jaune)
        int p1ScoreWidth = Math.Min(player1.score * 10, scoreBoxWidth - 10);
        _spriteBatch.Draw(pixel, new Rectangle(p1X + 5, p1Y + 5, p1ScoreWidth, scoreBoxHeight - 10), Color.Gold);


        // Player 2 score (en bas à droite)
        int p2X = gameSize.X - scoreBoxWidth - margin;
        int p2Y = gameSize.Y - scoreBoxHeight - margin;

        // fond du score player 2
        _spriteBatch.Draw(pixel, new Rectangle(p2X - 2, p2Y - 2, scoreBoxWidth + 4, scoreBoxHeight + 4), Color.Black);
        _spriteBatch.Draw(pixel, new Rectangle(p2X, p2Y, scoreBoxWidth, scoreBoxHeight), new Color(100, 50, 50));

        // barre de score player 2 (cyan)
        int p2ScoreWidth = Math.Min(player2.score * 10, scoreBoxWidth - 10);
        _spriteBatch.Draw(pixel, new Rectangle(p2X + 5, p2Y + 5, p2ScoreWidth, scoreBoxHeight - 10), Color.Cyan);
    }

    private void DrawGameOver()
    {
        // FOND NOIR TRANSPARENT
        _spriteBatch.Draw(
            pixel,
            new Rectangle(0, 0, gameSize.X, gameSize.Y),
            new Color(0, 0, 0, 200)
        );

        int centerX = gameSize.X / 2;
        int centerY = gameSize.Y / 2;

        int panelWidth = 480;
        int panelHeight = 300;
        int panelX = centerX - panelWidth / 2;
        int panelY = centerY - panelHeight / 2;

        // OMBRE DU PANNEAU
        _spriteBatch.Draw(
            pixel,
            new Rectangle(panelX + 6, panelY + 6, panelWidth, panelHeight),
            new Color(0, 0, 0, 120)
        );

        // PANNEAU PRINCIPAL
        _spriteBatch.Draw(
            pixel,
            new Rectangle(panelX, panelY, panelWidth, panelHeight),
            new Color(25, 25, 35)
        );

        // BORDURE
        _spriteBatch.Draw(
            pixel,
            new Rectangle(panelX - 3, panelY - 3, panelWidth + 6, panelHeight + 6),
            Color.Black
        );

        // TITRE
        string title = "GAME OVER";
        Vector2 titleSize = font.MeasureString(title);
        _spriteBatch.DrawString(
            font,
            title,
            new Vector2(centerX - titleSize.X / 2, panelY + 25),
            Color.OrangeRed
        );

        // LIGNE SEPARATION
        _spriteBatch.Draw(
            pixel,
            new Rectangle(panelX + 40, panelY + 80, panelWidth - 80, 2),
            Color.Black
        );

        // SCORES
        string score1 = $"PLAYER 1  :  {player1.score}";
        string score2 = $"PLAYER 2  :  {player2.score}";

        Vector2 s1Size = font.MeasureString(score1);
        Vector2 s2Size = font.MeasureString(score2);

        _spriteBatch.DrawString(
            font,
            score1,
            new Vector2(centerX - s1Size.X / 2, panelY + 120),
            Color.White
        );

        _spriteBatch.DrawString(
            font,
            score2,
            new Vector2(centerX - s2Size.X / 2, panelY + 160),
            Color.White
        );

        // TEXTE ACTION
        string action = "Press R to Restart";
        Vector2 actionSize = font.MeasureString(action);

        _spriteBatch.DrawString(
            font,
            action,
            new Vector2(centerX - actionSize.X / 2, panelY + panelHeight - 50),
            Color.LightGray
        );
    }


    private void RestartGame()
    {
        gameOver = false;
        santa.argentActuel = 0f;
        player1.score = 0;
        player2.score = 0;
        player1.position = new Vector2(350, 465);
        player2.position = new Vector2(850, 465);
        enemies.Clear();
        spawnTimer = 0f;
    }
}
