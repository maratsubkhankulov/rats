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
        private Tile[,] _tiles;
        public const int TileWidth = 30;
        private int _width;
        private int _height;
        private List<Sprite> _mineSprites;

        private readonly Texture2D _mineTexture;
        private readonly Sprite _mineHintSprite;
        private readonly SpriteManager _spriteManager;

        public enum Tile
        {
            OutsideGrid,
            Wall,
            Floor,
            Mine
        }

        private Dictionary<Color, Tile> tileDictionary = new Dictionary<Color, Tile>(){{Color.Black, Tile.Wall}, {Color.White, Tile.Floor}};

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < _tiles.GetLength(0) && y >= 0 && y < _tiles.GetLength(1))
            {
                return _tiles[x, y];
            }
            else
            {
                return Tile.OutsideGrid;
            }
        }

        public Rectangle TileBoundingRectangle(int x, int y)
        {
            return new Rectangle(x * TileWidth, y * TileWidth, TileWidth, TileWidth);
        }

        public void IndicesAtCoordinate(float xCoord, float yCoord, out int xIndex, out int yIndex)
        {
            xIndex = (int)Math.Floor(xCoord / TileWidth);
            yIndex = (int)Math.Floor(yCoord / TileWidth);
        }

        public Vector2 PositionAtIndices(int xIndex, int yIndex)
        {
            return new Vector2(xIndex*TileWidth, yIndex*TileWidth);
        }

        public Grid(Texture2D gridTexture, SpriteManager spriteManager, Texture2D floorTexture, Texture2D wallTexture, Texture2D mineTexture, Texture2D mineHintTexture)
        {
            _width = gridTexture.Width;
            _height = gridTexture.Height;
            _mineTexture = mineTexture;
            _mineHintSprite = new Sprite(mineHintTexture);
            _spriteManager = spriteManager;
            _mineSprites = new List<Sprite>();

            _spriteManager.Register(_mineHintSprite);

            _tiles = new Tile[_width, _height];

            Color[] colors1D = new Color[_width * _height];
            gridTexture.GetData(colors1D);

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Color color = colors1D[x + y * gridTexture.Width];
                    _tiles[x, y] = tileDictionary[color];

                    Sprite sprite;
                    switch (_tiles[x,y])
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
            _tiles[x, y] = Tile.Mine;
            Sprite mine = new Sprite(_mineTexture, new Vector2(x * TileWidth, y * TileWidth));
            //_mineSprites.Add(mine);
            _spriteManager.Register(mine);
        }

        public void RemoveMine(int x, int y)
        {
            _tiles[x, y] = Tile.Floor;
        }

        internal void SetMineHint(int x, int y)
        {
            _mineHintSprite.Position = new Vector2(x*TileWidth, y*TileWidth);
            //_mineSprites.Remove(_mineSprites.l
        }
    }
}