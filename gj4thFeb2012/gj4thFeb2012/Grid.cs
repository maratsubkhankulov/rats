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

        public enum Tile
        {
            Wall,
            Floor,
            Mine
        }

        private Dictionary<Color, Tile> tileDictionary = new Dictionary<Color, Tile>(){{Color.Black, Tile.Wall}, {Color.White, Tile.Floor}};

        public Grid(Texture2D gridTexture)
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
                }
            }
        }
    }
}