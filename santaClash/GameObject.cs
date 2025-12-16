using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace santaClash;

public class GameObject : Game
{

    public Vector2 position;
    public Vector2 vitesse;
    public bool isAlive;

    public GameObject(Vector2 position, Vector2 vitesse)
    {
        this.position = position;
        this.vitesse = vitesse;
        this.isAlive = true;
    }

    public void Update(GameTime gameTime)
    {

        base.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        
    }
}