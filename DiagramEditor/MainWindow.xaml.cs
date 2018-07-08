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

namespace DiagramEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color c1, c2;
        Random r = new Random();
        List<DataDiagram> lst = new List<DataDiagram>();
        const int interval = 10;
        public double WidthRect { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            String comb = $"{legend.Text} => {data.Text}";
            dataList.Items.Add(comb);
            lst.Add(new DataDiagram(legend.Text, Int32.Parse(data.Text)));
            legend.Clear();
            data.Clear();
            legend.Focus();
        }

        private void drawRect(double width, double heigth, Brush f, Brush s, double t, double a, double b)
        {
            Rectangle r = new Rectangle();
            r.Width = width;
            r.Height = heigth;
            r.Fill = f;
            r.Stroke = s;
            r.StrokeThickness = t;
            r.RenderTransform = new TranslateTransform(a, b);
            viewBox.Children.Add(r);
        }
        private void drawText(string content, double x, double y, int fontsize)
        {
            Label l = new Label();
            l.FontSize = fontsize;
            l.Content = content;
            viewBox.Children.Add(l);
            l.RenderTransform = new TranslateTransform(x-l.FontSize, y-l.FontSize*2);
        }
        private void drawDiagram()
        {
            WidthRect = (viewBox.ActualWidth - (interval * lst.Count)) / lst.Count - interval;
            double x = interval;
            double y = viewBox.ActualHeight - interval;
            int max = lst.Max(k => k.Procent);
            double height;
            foreach (var item in lst)
            {
                c1 = Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
                c2 = Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
                height = ((100 * item.Procent) / max) * 5;
                drawRect(WidthRect, height, new LinearGradientBrush(c1, c2, 1), Brushes.Blue, 2, x, y - height);
                drawText(item.Name, x+ WidthRect/2, y - height, (int)WidthRect/8);
                x += WidthRect + interval;
            }  
        }



        private void drawCircleDiagram()
        {
            Ellipse e = new Ellipse();
            e.Height = e.Width = 250;
            e.Fill = Brushes.Bisque;
            viewBox1.Children.Add(e);
            e.RenderTransform = new TranslateTransform(400, 400);
        }

        private void draw_Click(object sender, RoutedEventArgs e)
        {
            viewBox.Children.Clear();
            drawDiagram();
            drawCircleDiagram();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            int k = dataList.SelectedIndex;
            if (k < 0)
                MessageBox.Show("Вы не выбрали элемент для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                dataList.Items.RemoveAt(k);
                lst.RemoveAt(k);
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            viewBox.Children.Clear();
        }
    }
}
