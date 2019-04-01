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

namespace PLLUGTaskFractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int depth = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            labelDepth.Content = "Depth = " + depth;
            try
            {
                int maxDepth = int.Parse(TextBoxInput.Text);

                if (depth <= maxDepth)
                {
                    canvasFractal.Children.Clear();
                    Sierpinski();
                    depth++;
                }
                else
                {
                    labelDepth.Content = string.Empty;
                    canvasFractal.Children.Clear();
                    depth = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Sierpinski()
        {
            Point p1 = new Point(canvasFractal.Width / 2, 10);
            Point p2 = new Point(10, canvasFractal.Height - 10);
            Point p3 = new Point(canvasFractal.Width - 10, canvasFractal.Height - 10);
            DrawSierpinski(canvasFractal, p1, p2, p3, depth);
        }

        private void DrawSierpinski(Canvas canvasFractal, Point p1, Point p2, Point p3, int deep)
        {

            if (deep > 0)
            {

                Point pm12 = GetMiddlePoint(p1, p2);
                Point pm23 = GetMiddlePoint(p2, p3);
                Point pm13 = GetMiddlePoint(p1, p3);
                DrawSierpinski(canvasFractal, p1, pm12, pm13, deep - 1);
                DrawSierpinski(canvasFractal, pm12, p2, pm23, deep - 1);
                DrawSierpinski(canvasFractal, pm13, pm23, p3, deep - 1);

            }
            else
            {
                Draw(canvasFractal, p1, p2);
                Draw(canvasFractal, p2, p3);
                Draw(canvasFractal, p1, p3);
                return;
            }

        }

        private void Draw(Canvas canvas, Point pointFirst, Point pointSecond)
        {
            Line line = new Line
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = pointFirst.X,
                X2 = pointSecond.X,
                Y1 = pointFirst.Y,
                Y2 = pointSecond.Y
            };
            canvas.Children.Add(line);
        }

        private Point GetMiddlePoint(Point pointFirst, Point pointSecond)
        {
            Point pointMid = new Point((pointFirst.X + pointSecond.X) / 2, (pointFirst.Y + pointSecond.Y) / 2);
            return pointMid;
        }


        private void CanvasFractal_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // check if not number => not accept
            if (!IsNumber(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumber(string inputText)
        {
            return int.TryParse(inputText, out var output);
        }

        private void TextBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.Parse(TextBoxInput.Text) > 12)
            {
                TextBoxInput.Text = "12";
                TextBoxInput.SelectionStart = TextBoxInput.Text.Length;
                TextBoxInput.SelectionLength = 0;
            }
        }
    }
}

