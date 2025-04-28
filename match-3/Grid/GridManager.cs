using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Match3Game.Grid
{
    public class GridManager
    {
        private int gridSize = 8;
        private Cell[,] cells;
        private List<Texture2D> elementTextures;
        private Random random = new Random();
        private Cell selectedCell = null;
        private int score = 0;
        public int Score => score;

        public GridManager(List<Texture2D> textures)
        {
            elementTextures = textures;
            cells = new Cell[gridSize, gridSize];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Vector2 position = new Vector2(x * Cell.cellSize + 100, y * Cell.cellSize + 50);
                    cells[x, y] = new Cell(x, y, position);

                    cells[x, y].Element = GenerateNonMatchingElement(x, y);

                    cells[x, y].Clicked += OnCellClicked;
                }
            }
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            float speed = 500f;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    var element = cells[x, y].Element;
                    if (element != null)
                    {
                        element.Offset = Vector2.Lerp(element.Offset, Vector2.Zero, elapsed * speed * 0.01f);

                        if (element.Offset.Length() < 1f)
                        {
                            element.Offset = Vector2.Zero;
                        }
                    }
                }
            }
        }

        private void CollapseGrid()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = gridSize - 1; y >= 0; y--)
                {
                    if (cells[x, y].Element == null)
                    {
                        for (int aboveY = y - 1; aboveY >= 0; aboveY--)
                        {
                            if (cells[x, aboveY].Element != null)
                            {
                                cells[x, y].Element = cells[x, aboveY].Element;
                                cells[x, aboveY].Element = null;

                                cells[x, y].Element.Offset = new Vector2(0, (aboveY - y) * Cell.cellSize);
                                break;
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (cells[x, y].Element == null)
                    {
                        cells[x, y].Element = GenerateRandomElement(new List<int>());

                        cells[x, y].Element.Offset = new Vector2(0, -Cell.cellSize * (gridSize - y));
                    }
                }
            }
        }

        private Element GenerateRandomElement(List<int> forbiddenTypes)
        {
            List<int> allowedTypes = new List<int>();

            for (int i = 0; i < elementTextures.Count; i++)
            {
                if (!forbiddenTypes.Contains(i))
                {
                    allowedTypes.Add(i);
                }
            }

            int selectedType = allowedTypes[random.Next(allowedTypes.Count)];
            return new Element(selectedType, elementTextures[selectedType]);
        }


        private Element GenerateNonMatchingElement(int x, int y)
        {
            List<int> forbiddenTypes = new List<int>();

            if (x >= 2)
            {
                var e1 = cells[x - 1, y].Element;
                var e2 = cells[x - 2, y].Element;

                if (e1 != null && e2 != null && e1.Type == e2.Type)
                {
                    forbiddenTypes.Add(e1.Type);
                }
            }

            if (y >= 2)
            {
                var e1 = cells[x, y - 1].Element;
                var e2 = cells[x, y - 2].Element;

                if (e1 != null && e2 != null && e1.Type == e2.Type)
                {
                    forbiddenTypes.Add(e1.Type);
                }
            }

            return GenerateRandomElement(forbiddenTypes);
        }

        private void OnCellClicked(object sender, Cell clickedCell)
        {
            if (selectedCell == null)
            {
                selectedCell = clickedCell;
                selectedCell.Element.IsSelected = true;
            }
            else
            {
                if (AreNeighbors(selectedCell, clickedCell))
                {
                    SwapElements(selectedCell, clickedCell);

                    bool matched = CheckMatches();

                    if (!matched)
                    {
                        SwapElements(selectedCell, clickedCell);
                    }

                    selectedCell.Element.IsSelected = false;
                    clickedCell.Element.IsSelected = false;
                    selectedCell = null;
                }
                else
                {
                    selectedCell.Element.IsSelected = false;
                    selectedCell = clickedCell;
                    selectedCell.Element.IsSelected = true;
                }
            }
        }

        private bool AreNeighbors(Cell a, Cell b)
        {
            int dx = Math.Abs(a.GridPosition.X - b.GridPosition.X);
            int dy = Math.Abs(a.GridPosition.Y - b.GridPosition.Y);

            return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
        }

        private void SwapElements(Cell a, Cell b)
        {
            var temp = a.Element;
            a.Element = b.Element;
            b.Element = temp;

            var delta = new Vector2(b.WorldPosition.X - a.WorldPosition.X, b.WorldPosition.Y - a.WorldPosition.Y);
            a.Element.Offset = delta;
            b.Element.Offset = -delta;
        }

        private bool CheckMatches()
        {
            bool[,] matched = new bool[gridSize, gridSize];
            bool anyMatched = false;

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize - 2; x++)
                {
                    var a = cells[x, y].Element;
                    var b = cells[x + 1, y].Element;
                    var c = cells[x + 2, y].Element;

                    if (a != null && b != null && c != null &&
                        a.Type == b.Type && b.Type == c.Type)
                    {
                        matched[x, y] = true;
                        matched[x + 1, y] = true;
                        matched[x + 2, y] = true;
                    }
                }
            }

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize - 2; y++)
                {
                    var a = cells[x, y].Element;
                    var b = cells[x, y + 1].Element;
                    var c = cells[x, y + 2].Element;

                    if (a != null && b != null && c != null &&
                        a.Type == b.Type && b.Type == c.Type)
                    {
                        matched[x, y] = true;
                        matched[x, y + 1] = true;
                        matched[x, y + 2] = true;
                    }
                }
            }

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if (matched[x, y])
                    {
                        cells[x, y].Element = null;
                        score += 10;
                        anyMatched = true;
                    }
                }
            }

            if (anyMatched)
            {
                CollapseGrid();
                CheckMatches();
            }

            return anyMatched;
        }

        public void Update(GameTime gameTime)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    cells[x, y].Update(gameTime);
                }
            }

            UpdateAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    cells[x, y].Draw(spriteBatch);
                }
            }
        }
    }
}
