using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gj4thFeb2012
{
    class Grid
    {
        public Tile[,] Tiles;

        private int _width;
        private int _height;
        private const int TileWidth = 10;

        public enum Tile
        {
            Wall,
            Floor,
            Mine
        }

        private Dictionary<Color, Tile> tileDictionary = new Dictionary<Color, Tile>(){{Color.Black, Tile.Wall}, {Color.White, Tile.Floor}};

        public Grid(Texture2D gridTexture, SpriteManager spriteManager, Texture2D floorTexture, Texture2D wallTexture)
        {
            _width = gridTexture.Width;
            _height = gridTexture.Height;

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
                                sprite = new Sprite(wallTexture, new Vector2(x * TileWidth, y * TileWidth));
                            break;

                            case Tile.Floor:
                                sprite = new Sprite(floorTexture, new Vector2(x * TileWidth, y * TileWidth));
                            break;

                            default:
                                throw new Exception("NO");
                    }

                    spriteManager.Register(sprite);
                }
            }
        }
    }
}