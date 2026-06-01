using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative)),
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/BlockEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/IBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/JBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/OBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/SBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/ZBlock.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;

        private GameState gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) 
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                // Move block around
                case Key.A:
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.D:
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.S:
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;

                // Rotate block
                case Key.J:
                    gameState.RotateBlockCCW();
                    break;
                case Key.L:
                    gameState.RotateBlockCW();
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Draw(gameState);
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)       {

        }
    }
}