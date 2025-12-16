using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace santaClash;

public class Enemy : GameObject
{

    private Texture2D _texture;


    public Enemy(Texture2D texture, Vector2 vitesse, Vector2 position) : base(position, vitesse)
    {
        _texture = texture;


    }



}