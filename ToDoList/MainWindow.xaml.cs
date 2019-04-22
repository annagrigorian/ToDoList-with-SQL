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
using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionstring = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = ToDoList; Integrated Security = True";

        public MainWindow()
        {
            InitializeComponent();
        }     

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    try
                    {
                        string text = textBox.Text;
                        connection.Open();
                        string queryingstring = $"Insert into Items(Title) Values({text})";
                        SqlCommand add = new SqlCommand(queryingstring, connection);
                        //connection.Open();

                        add.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                textBox.Text = "";                        
            }
        }

        private void AllButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                listBox.Visibility = Visibility.Visible;                
                SqlCommand command = new SqlCommand("SELECT Title FROM Items", connection);

                try
                {
                    connection.Open();
                    //listBox.Items.Add("sdfsdf");
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox.Items.Add(reader[0]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }               
            }            
        }

        private void ActiveButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                listBox.Visibility = Visibility.Visible;
                SqlCommand command = new SqlCommand("SELECT Title FROM Items where IsCompleted=0", connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox.Items.Add(reader[0]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        
    }

        private void ClearCompletedButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CompletedButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                listBox.Visibility = Visibility.Visible;
                SqlCommand command = new SqlCommand("SELECT Title FROM Items where IsCompleted=1 ", connection);

                try
                {
                    connection.Open();
                    //listBox.Items.Add("sdfsdf");
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox.Items.Add(reader[0]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
