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
        string connectionstring = "Data Source=(local);"
                                + "Initial Catalog=ToDoList;"
                                + "Integrated Security=true;";  

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
                        connection.Open();
                        SqlCommand add = new SqlCommand("Insert into ToDoList(Title) Values(textBox.Text)", connection);
                        //connection.Close();
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
                    listBox.Items.Add("sdfsdf");
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

        }

        private void ClearCompletedButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CompletedButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
