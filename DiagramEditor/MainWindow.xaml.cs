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
        List<DataDiagram> lst = new List<DataDiagram>();
        int start = 600;
        public double WidthRect{ get; set; }
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

        private void draw_Click(object sender, RoutedEventArgs e)
        {
            WidthRect = (viewBox.ActualWidth / lst.Count) - (10* lst.Count);     
            double x = 830;
            double y = 660;
            //lst.Sort();
            int max = lst.Max(k => k.Procent);
            double height;
            foreach (var item in lst)
            {
                height = (100 * item.Procent)/max;
                drawRect(WidthRect, height, Brushes.Orange, Brushes.Blue, 1, x, y- viewBox.ActualHeight);
                x -= WidthRect+10;
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            int k = dataList.SelectedIndex;
            if (k < 0)
                MessageBox.Show("Вы не выбрали элемент для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                dataList.Items.RemoveAt(k);
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
