using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    public class Grid
    {
        public Tile[,] Tiles;
        public const int TileWidth = 30;
        private int _width;
        private int _height;

        private readonly Texture2D _mineTexture;
        private readonly Sprite _mineHintSprite;
        private readonly SpriteManager _spriteManager;

        public enum Tile
        {
            Wall,
            Floor,
            Mine
        }

        private Dictionary<Color, Tile> tileDictionary = new Dictionary<Color, Tile>(){{Color.Black, Tile.Wall}, {Color.White, Tile.Floor}};

        public Rectangle TileBoundingRectangle(int x, int y)
        {
            return new Rectangle(x * TileWidth, y * TileWidth, TileWidth, TileWidth);
        }

        public Grid(Texture2D gridTexture, SpriteManager spriteManager, Texture2D floorTexture, Texture2D wallTexture, Texture2D mineTexture, Texture2D mineHintTexture)
        {
            _width = gridTexture.Width;
            _height = gridTexture.Height;
            _mineTexture = mineTexture;
            _mineHintSprite = new Sprite(mineHintTexture);
            _spriteManager = spriteManager;

            _spriteManager.Register(_mineHintSprite);

            Tiles = new Tile[_width, _height];

            Color[] colors1D = new Color[_width * _height];
            gridTexture.GetData(colors1D);

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Color color = colors1D[x + y * gridTexture.Width];
                    Tiles[y, x] = tileDictionary[color];

                    Sprite sprite;
                    switch (Tiles[y,x])
                    {
                            case Tile.Wall:
                                sprite = new Sprite(wallTexture, new Vector2(x * TileWidth, y * TileWidth), 1.0F);
                            break;

                            case Tile.Floor:
                                sprite = new Sprite(floorTexture, new Vector2(x * TileWidth, y * TileWidth), 1.0F);
                            break;

                            default:
                                throw new Exception("NO");
                    }

                    spriteManager.Register(sprite);
                }
            }
        }

        internal void SetMine(int x, int y)
        {
            Tiles[y, x] = Tile.Mine;
            _spriteManager.Register(new Sprite(_mineTexture, new Vector2(x * TileWidth, y * TileWidth)));
        }

        internal void SetMineHint(int x, int y)
        {
            _mineHintSprite.Position = new Vector2(x*TileWidth, y*TileWidth);
        }
    }
}