using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Go.CustomControls;
using static Go.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Go.UnionFind;

namespace Go
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Border _boardBorder;

        private bool _blackTurn;

        private int _boardSize;

        public Tile[][] BoardTiles;

        private UnionFind<Tile> _chains;


        public GameWindow()
        {
            InitializeComponent();

            _blackTurn = true;
            _boardSize = BOARD_SIZE;

            DrawBoard();
        }

        private void DrawBoard()
        {
            if (BoardTiles == null || BoardTiles.Length != _boardSize)
            {
                BoardTiles = new Tile[_boardSize][];

                for (var i = 0; i < BoardTiles.Length; i++)
                {
                    BoardTiles[i] = new Tile[_boardSize];
                }
            }

            _chains = new UnionFind<Tile>(_boardSize);

            BoardGrid.Children.Clear();
            Tree<Tile> firstNode = null;

            for (var i = 0; i < _boardSize; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                BoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                for (var j = 0; j < _boardSize; j++)
                {
                    var tile = new Tile();
                    tile.SetValue(Grid.RowProperty, i);
                    tile.SetValue(Grid.ColumnProperty, j);
                    tile.X = i;
                    tile.Y = j;

                    tile.MouseUp += (sender, args) => PlaceStoneAt((Tile)sender);

                    BoardTiles[i][j] = tile;
                    BoardGrid.Children.Add(tile);
                    if (firstNode == null)
                    {
                        firstNode = new Tree<Tile>(null, tile);
                    }
                    else
                    {
                        var node = new Tree<Tile>(firstNode, tile);
                    }
                }
            }
            for (var i = 0; i < BoardTiles.Length; i++)
            {
                for (var j = 0; j < BoardTiles[i].Length; j++)
                {
                    BoardTiles[i][j].Left = i - 1 > 0 ? BoardTiles[i - 1][j] : null;
                    BoardTiles[i][j].Top = j - 1 > 0 ? BoardTiles[i][j - 1] : null;
                    BoardTiles[i][j].Right = i + 1 < BoardTiles.Length ? BoardTiles[i + 1][j] : null;
                    BoardTiles[i][j].Bottom = j + 1 < BoardTiles[i].Length ? BoardTiles[i][j + 1] : null;
                }
            }
        }

        private void PlaceStoneAt(Tile tile)
        {
            #region place stone
            if (!CheckIfMoveLegal(tile))
            {
                ShowInvalidMoveLabel();
                return;
            }

            if (_blackTurn)
            {
                tile.SetBlack();
            }
            else
            {
                tile.SetWhite();
            }
            #endregion
            #region capture enemy

            #endregion
            #region selfcapture
            #endregion

            _blackTurn = !_blackTurn;
        }

        private void PlaceStoneAt(int x, int y)
        {
            var tile = BoardTiles[x][y];
            PlaceStoneAt(tile);
        }

        private bool CheckIfMoveLegal(int x, int y)
        {
            var tile = BoardTiles[x][y];

            return CheckIfMoveLegal(tile);
        }

        private bool CheckIfMoveLegal(Tile tile)
        {
            var isLegal = tile.IsEmpty;

            if (!isLegal) return false;

            return isLegal;
        }

        //public int GetBlackPoints()
        //{
        //    _chains
        //}

        #region STRICTLY  UI
        private int invalidMoveSemaphore = 0;
        private async void ShowInvalidMoveLabel()
        {
            InvalidMoveLabel.Visibility = Visibility.Visible;

            invalidMoveSemaphore++;

            await Task.Delay(3000);

            invalidMoveSemaphore--;

            if (invalidMoveSemaphore == 0)
            {
                InvalidMoveLabel.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}