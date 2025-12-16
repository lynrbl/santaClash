using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace santaClash;

public class Santa : GameObject
{
    private readonly GraphicsDeviceManager _graphics;
    private Vector2 _position;
    private Vector2 _velocity;
    private Texture2D _texture;
    private readonly Vector2 _targetPosition;
    private readonly Random _random = new Random();

    private Vector2 _origin;

    private float scale = 0.4f;
    private float _attractionStrength = 50f; // force qui attire vers le centre
    private float _noiseStrength = 20f; // intensité du "chaos"
    private float _damping = 0.90f; // amortissement de la vitesse

    public Santa(GraphicsDeviceManager graphics, Texture2D texture, Vector2 vitesse, Vector2 position) : base(position, vitesse)
    {
        _graphics = graphics;
        _texture = texture;
        _origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        _position = position;
        _velocity = vitesse;
        _targetPosition = new Vector2(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f);

    }


    public void Update()
    {
        // Direction vers la position cible
        Vector2 toCenter = _targetPosition - _position;
        if (toCenter != Vector2.Zero)
            toCenter.Normalize();
        // Petit bruit aléatoire
        float noiseX = (float)(_random.NextDouble() - 10); // [-0.5, 0.5]
        float noiseY = (float)(_random.NextDouble() - 10);
        Vector2 noise = new Vector2(noiseX, noiseY);
        if (noise != Vector2.Zero)
            noise.Normalize();
        // Force totale = attraction vers le centre + bruit

        Vector2 acceleration = toCenter * _attractionStrength + noise * _noiseStrength;
        Debug.WriteLine("Acceleration: " + acceleration);
        // Intégration simple
        _velocity += acceleration;
        // _velocity *= _damping; // amortissement
        _position += _velocity;
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
        // spriteBatch.Draw(_texture, position, Color.White, scale);
        //null signifie qu'on prend toute l'image source
        // 0f = Pas de rotation 
        //Vector2.Zero = Origine en haut à gauche
        //SpriteEffects.None pas d'éffet miroir
        // 0f = couche de profondeur
        spriteBatch.Draw(_texture, position, null, Color.White, 0f, _origin, scale, SpriteEffects.None, 0f);
    }
}