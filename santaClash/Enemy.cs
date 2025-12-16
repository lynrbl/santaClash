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

    public Enemy(GraphicsDeviceManager graphics, Texture2D texture, Vector2 vitesse, Vector2 position) : base(position, vitesse)
    {
        _graphics = graphics;
        _texture = texture;

    }

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
    {
        base.Update(gameTime);
        float spriteWidth = _texture.Width * scale;
        float spriteHeight = _texture.Height * scale;

        float maxX = screenWidth - spriteWidth;
        float maxY = screenHeight - spriteHeight;

        position.X = MathHelper.Clamp(position.X, 0, maxX);
        position.Y = MathHelper.Clamp(position.Y, 0, maxY);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // spriteBatch.Draw(_texture, position, Color.White, scale);
        //null signifie qu'on prend toute l'image source
        // 0f = Pas de rotation 
        //Vector2.Zero = Origine en haut à gauche
        //SpriteEffects.None pas d'éffet miroir
        // 0f = couche de profondeur
        spriteBatch.Draw(_texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

}