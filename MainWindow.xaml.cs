using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Maze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// MainWindow's default constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            noLevel = false;

            string sprite = "title.png";
            img.Source = new BitmapImage(new Uri(@"pack://application:,,,/res/" + sprite));

            Directory.CreateDirectory(FOLDER_NAME);

            initialContent = Content;
        }

        /// <summary>
        /// Notify that there is no level.
        /// </summary>
        public void NotifyNoLevel()
        {
            noLevel = true;
        }

        /// <summary>
        /// Set the view to the main menu.
        /// </summary>
        public void DisplayMenu()
        {
            Content = initialContent;
        }

        /// <summary>
        /// Event to toggle fullscreen.
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11)
            {
                if (isFullScreen = !isFullScreen)
                {
                    tmp = WindowState;
                    WindowState = WindowState.Maximized;
                    WindowStyle = WindowStyle.None;
                }
                else
                {
                    WindowState = tmp;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                }
            }
        }

        /// <summary>
        /// Event trigger when the Play button is clicked.
        /// </summary>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            lblImport.Content = "";

            GamePage game = new GamePage(this);
            if (!noLevel)
                Content = game;
            else
                lblImport.Content = "Pas de niveau valide disponible.";

            noLevel = false;
        }

        /// <summary>
        /// Event trigger when the Import button is clicked.
        /// </summary>
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Text documents (.txt)|*.txt"
            };
            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.SafeFileName;
                if (!File.Exists(FOLDER_NAME + filename))
                {
                    File.Copy(dialog.FileName, FOLDER_NAME + filename);
                    lblImport.Content = "Copie réussie.";
                }
                else
                    lblImport.Content = "Il existe déjà un fichier avec ce nom.";
            }
        }

        /// <summary>
        /// Event trigger when the Quit button is clicked.
        /// </summary>
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Output
        public const string FOLDER_NAME = "./mazes/";

        // tool
        private object initialContent;
        private bool isFullScreen;
        private WindowState tmp;
        private bool noLevel;
    }
}
