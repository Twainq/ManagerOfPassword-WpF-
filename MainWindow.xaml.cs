﻿using Newtonsoft.Json;
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
        public MainWindow()
        {
            InitializeComponent();

            if(File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                   data = sr.ReadToEnd();   
                }
                string[] lines = data.Split('\n');
                foreach(string line in lines)
                {
                    if (line != "")
                    {
                        Product p = JsonConvert.DeserializeObject<Product>(line);
                        ServicesGrid.Items.Add(p);
                    }
                    
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServicesGrid.Items.Add(new Product(service.Text, login.Text, password.Text));
            //foreach (Product p in ServicesGrid.Items)
            //{
            //    if (service.Text!= p.Service && login.Text != p.Service)
            //    {
            //        ServicesGrid.Items.Add(new Product(service.Text, login.Text, password.Text));
            //    }
            //    else
            //    {
            //        MessageBox.Show("Такой пароль уже существует!");
            //    }
            //}
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItems.Count != 0) {

                while (ServicesGrid.SelectedItems.Count != 0) {
                    ServicesGrid.Items.Remove(ServicesGrid.SelectedItem);
                }

            }else
            {
                MessageBox.Show("Нет выбранных паролей для удаления!"); 
            }

        }
    }
}
