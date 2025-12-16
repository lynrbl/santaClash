using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace santaClash;

public class Player : GameObject
{
    private readonly GraphicsDeviceManager _graphics;
    private Texture2D _texture;

    public Player(GraphicsDeviceManager graphics, Texture2D texture, Vector2 vitesse, Vector2 position) : base(position, vitesse)
    {
        _graphics = graphics;
        _texture = texture;

    }

    public void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        float minX = 0 ;
        float maxX = 1250 - _texture.Width ;
        float minY = 0;
        float maxY = 1000 - _texture.Height;

        // bloque la raquette pour rester à l'écran
        if (position.X < minX)
            position = new Vector2(minX, position.Y);
        else if (position.X > maxX)
            position = new Vector2(maxX, position.Y);
        else if (position.Y < minY)
            position = new Vector2(position.X, minY);
        else if (position.Y > maxY)
            position = new Vector2(position.X, maxY);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, position, Color.White);
    }


}