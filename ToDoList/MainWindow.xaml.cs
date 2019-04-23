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
        public delegate void CheckedChanged();

        public CheckedChanged OnCheckedChanged;

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
                        string queryingstring = $"Insert into Items(Title) Values('{text}')";
                        SqlCommand add = new SqlCommand(queryingstring, connection);
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
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CheckBox item = new CheckBox();
                        item.Content = reader[0];
                        listBox.Items.Add(item);
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
                        CheckBox item = new CheckBox();
                        item.Content = reader[0];
                        listBox.Items.Add(item);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        
    }

        private void CheckBox_CheckedChanged()
        {
            List<CheckBox> selectedItems = new List<CheckBox>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                foreach (var item in listBox.Items)
                {
                    CheckBox ch = (CheckBox)item;
                    if (ch.IsChecked == true)
                    {
                        SqlCommand command = new SqlCommand($"Update Items set IsCompleted = 1 where Title = '{ch.Content}'");
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand($"Update Items set IsCompleted = 0 where Title = '{ch.Content}'");
                    }
                }
            }          
        }

        private void ClearCompletedButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {              

                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("Delete From Items where IsCompleted = 1", connection);
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CheckBox item = new CheckBox();
                        item.Content = reader[0];
                        listBox.Items.Add(item);
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
