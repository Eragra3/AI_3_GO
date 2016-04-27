using System;
using System.Collections.Generic;
using System.Globalization;
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
using Go.Enums;
using Go.UnionFind;

namespace Go.CustomControls
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        public bool Occupied { get; private set; }

        public int X;

        public int Y;

        public TileStateEnum TileState { get; private set; }

        public Tree<Tile> Tree;

        public Tile Left;

        public Tile Right;

        public Tile Top;

        public Tile Bottom;

        public Tile()
        {
            InitializeComponent();

            Stone.Fill = Brushes.Transparent;
            TileState = TileStateEnum.Empty;
            Tree = new Tree<Tile>(null, this);
        }

        public void SetBlack()
        {
            Stone.Fill = Brushes.Black;
            Occupied = true;
            TileState = TileStateEnum.Black;
        }

        public void SetWhite()
        {
            Stone.Fill = Brushes.White;
            Occupied = true;
            TileState = TileStateEnum.White;
        }

        public void PickUp()
        {
            Stone.Fill = Brushes.Transparent;
            Occupied = false;
            TileState = TileStateEnum.Empty;
        }


        #region bool methods

        public bool IsBlack => TileState == TileStateEnum.Black;

        public bool IsWhite => TileState == TileStateEnum.White;

        public bool IsEmpty => TileState == TileStateEnum.Empty;

        #endregion
    }
}
