using DataLayer;
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
using System.Windows.Shapes;
using LogicLayer;
using ToDoList;
using static System.Net.Mime.MediaTypeNames;

namespace PresentationLayer
{
    public partial class LoginWindow : Window
    {
        private bool isRegisterState = false;
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //txtUsername.Text = "vojta";
            //txtPassword.Password = "123456";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isRegisterState)
            {
                //register
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                string password2 = txtPassowrdAgain.Password;

                if(password != password2)
                {
                    MessageBox.Show("Password1 and password2 must be same.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(UserManager.Register(username, password))
                {
                    // Register successful
                    ShowMainwindow();
                    Close();
                }
                else
                {
                    // Register failed
                    MessageBox.Show("Registration failed.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                //login
                string username = txtUsername.Text;
                string password = txtPassword.Password;

                if (UserManager.Login(username, password))
                {
                    // Login successful
                    ShowMainwindow();
                    Close();
                }
                else
                {
                    // Login failed
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ShowMainwindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            double desiredWidth = screenWidth * 0.6; // 60% of screen width
            double desiredHeight = screenHeight * 0.6; // 60% of screen height

            MainWindow mainWindow = new MainWindow();
            mainWindow.Width = desiredWidth;
            mainWindow.Height = desiredHeight;
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Center the window
            mainWindow.Show();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (isRegisterState)
            {
                // Switch to the login state
                PassowrdAgainTitle.Visibility = Visibility.Collapsed;
                txtPassowrdAgain.Visibility = Visibility.Collapsed;
                isRegisterState = false;
                hyperlink.Inlines.Clear(); // Clear the existing content
                hyperlink.Inlines.Add(new Run("Register"));
                button.Content = "Login";
            }
            else
            {
                // Switch to the register state
                PassowrdAgainTitle.Visibility = Visibility.Visible;
                txtPassowrdAgain.Visibility = Visibility.Visible;
                isRegisterState = true;
                hyperlink.Inlines.Clear(); // Clear the existing content
                hyperlink.Inlines.Add(new Run("Login"));
                button.Content = "Register";
            }
        }
    }
}
