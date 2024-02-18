using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName = "data.db";
        string data;
        List<Product> list = new List<Product>();
        public MainWindow()
        {
            InitializeComponent();

            if(File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                   data = sr.ReadToEnd();   
                }
                SplitPassword();

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0)
                AddPassword();
            else
            {
                if (Uniq())
                    AddPassword();
                else
                    MessageBox.Show("Пароль с таким сервисом и логином уже существует!");
            }
            
        }
        private void AddPassword()
        {
            list.Add(new Product(service.Text, login.Text, password.Text));
            ServicesGrid.Items.Add(new Product(service.Text, login.Text, password.Text));
        }
        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {

            string gridLins ="";
            foreach (Product p in ServicesGrid.Items)
            {
                gridLins += JsonConvert.SerializeObject(p)+"\n";
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(gridLins);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItems.Count != 0) 
            {
                while (ServicesGrid.SelectedItems.Count != 0)
                {
                    Product selectedProduct = (Product)ServicesGrid.SelectedItem;
                    ServicesGrid.Items.Remove(selectedProduct);
                    list.Remove(selectedProduct);
                }
                
            }else
                MessageBox.Show("Нет выбранных паролей для удаления!"); 
        }

        private bool Uniq()
        {
            foreach (Product product in list)
            {
                if (product.Service == service.Text && product.Login == login.Text)
                    return false;
            }
            return true;
        }

        private void SplitPassword()
        {
            string[] lines = data.Split('\n');
            foreach (string line in lines)
            {
                if (line != "")
                {
                    Product p = JsonConvert.DeserializeObject<Product>(line);
                    ServicesGrid.Items.Add(p);
                    list.Add(p);
                }

            }
        }
    }
}
