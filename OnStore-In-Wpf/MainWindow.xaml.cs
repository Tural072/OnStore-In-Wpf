using OnStore_In_Wpf.Models;
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

namespace OnStore_In_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Product> Products = new List<Product>
        {
            new Product{
             Name="Bread",
              Price=0.50,
               ImagePath="Images/bread1.png",
               Description=""
            },
            new Product{
             Name="Pizza",
              Price=14,
               ImagePath="Images/pizza.png",
            },
            new Product{
             Name="Burger",
              Price=4,
               ImagePath="Images/burger.jpg",
               Description=""
            },
            new Product{
             Name="Cola",
              Price=1.50,
               ImagePath="Images/cola.png",
            },
            new Product{
             Name="Cips",
              Price=2,
               ImagePath="Images/cips.png",
               Description=""
            },
            new Product{
             Name="Juice",
              Price=2.50,
               ImagePath="Images/juice.png",
            }
        };
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            mainListbox.ItemsSource = Products;
        }
    }
}
