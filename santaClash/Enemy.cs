using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace santaClash;

public class Enemy : GameObject
{

    private readonly GraphicsDeviceManager _graphics;
    private Texture2D _texture;

    private float scale = 0.2f;
    private float speed = 2f;

    public Enemy(GraphicsDeviceManager graphics, Texture2D texture, Vector2 vitesse, Vector2 position) : base(position, vitesse)
    {
        _graphics = graphics;
        _texture = texture;

    }

    public void Update(GameTime gameTime, Vector2 santaPosition)
    {
        if (!isAlive) return;

        base.Update(gameTime);
        
        // déplacement vers le Père Noël
        Vector2 direction = santaPosition - position;
        if (direction != Vector2.Zero)
            direction.Normalize();
        
        position += direction * speed;
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)position.X, (int)position.Y, (int)(_texture.Width * scale), (int)(_texture.Height * scale));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!isAlive) return;

        // spriteBatch.Draw(_texture, position, Color.White, scale);
        //null signifie qu'on prend toute l'image source
        // 0f = Pas de rotation 
        //Vector2.Zero = Origine en haut à gauche
        //SpriteEffects.None pas d'éffet miroir
        // 0f = couche de profondeur
        spriteBatch.Draw(_texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

}