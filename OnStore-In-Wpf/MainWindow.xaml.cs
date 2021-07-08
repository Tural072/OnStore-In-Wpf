using Microsoft.Win32;
using OnStore_In_Wpf.Extensions;
using OnStore_In_Wpf.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// 
    public partial class MainWindow : Window
    {
        Product product;
        public List<Product> Products { get; set; }
        public List<Product> ProductsCopy { get; set; }

        public MainWindow()
        {
            Products = new List<Product>() {
                new Product
                {
                    Name = "Bread",
                    Price = 0.50,
                    ImagePath = "Images/bread1.png",
                    Description = "Bine zavod coreyi"
                },
            new Product
            {
                Name = "Pizza",
                Price = 14,
                ImagePath = "Images/pizza.png",
                Description = "Pizza salami"
            },
            new Product
            {
                Name = "Burger",
                Price = 4,
                ImagePath = "Images/burger.png",
                Description = "Beef burger"
            },
            new Product
            {
                Name = "Cola",
                Price = 1.50,
                ImagePath = "Images/cola.png",
            },
            new Product
            {
                Name = "Cips",
                Price = 2,
                ImagePath = "Images/cips.png",
                Description = "0,5 L"
            },
            new Product
            {
                Name = "Juice",
                Price = 2.50,
                ImagePath = "Images/juice.png",
                Description = "Portagal shiresi"
            }
            };
            DataContext = this;
            ProductsCopy = Products;
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (mainListbox.SelectedItem is Product item)
            {
                selectedProductImg.Source = new BitmapImage(new Uri(item.ImagePath, UriKind.RelativeOrAbsolute));
                product = item;
                name.Text = item.Name;
                price.Text = item.Price.ToString();
                description.Text = item.Description;

            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainListbox.ItemsSource = null;
            ProductsCopy = ProductsCopy.Where(p => p.Name.Contains(searchTextBox.Text)).ToList();
            mainListbox.ItemsSource = ProductsCopy;
            ProductsCopy = Products;
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                mainListbox.ItemsSource = Products;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (product == null)
            {
                return;
            }
            if (selectedProductImg.Source != null)
            {
                product.ImagePath = selectedProductImg.Source.ToString();
            }
            product.Name = name.Text;
            product.Price = double.Parse(price.Text);
            product.Description = description.Text;
            mainListbox.ItemsSource = null;
            mainListbox.ItemsSource = Products;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image file (*.png)|*.png";
            if (open.ShowDialog() != true)
            {
                return;
            }
            var image = new BitmapImage(new Uri(open.FileName));
            var fileName = $@"Images/{Guid.NewGuid()}.png";
            if (!Directory.Exists("Images"))
            {
                Directory.CreateDirectory("Images");
            }
            image.Save(fileName);
            var fullFileName = Directory.GetCurrentDirectory() + "\\" + fileName;
            selectedProductImg.Source = new BitmapImage(new Uri(fullFileName));
        }

        private void StackPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effects = DragDropEffects.All;
            }
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            string word = "";
            if (!Directory.Exists("Add ProductImage"))
            {
                Directory.CreateDirectory("Add ProductImage");
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (var file in files)
            {
                word = file;
            }
            try
            {
                string dircopyfrom = word;
                string[] fileEnter = Directory.GetFiles(dircopyfrom);
                string dircopyto = $@"Add ProductImage";
                foreach (var f in fileEnter)
                {
                    string filename = System.IO.Path.GetFileName(f);
                    File.Copy(f, dircopyto + "\\" + filename, true);
                    File.Delete(f);
                }
            }
            catch (Exception) { }
            File.Copy(word, $@"Add ProductImage/image.png", true);
            selectedProductImg.Source = new BitmapImage(new Uri(word));
        }
    }
}
