using Maze.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Maze
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, IInput, IOutput
    {
        /// <summary>
        /// GamePage's default constructor.
        /// </summary>
        /// <param name="parent">MainWindow that contains this page.</param>
        /// <param name="index">Index of the first file to load.</param>
        public GamePage(MainWindow parent, int index = 0)
        {
            InitializeComponent();

            this.parent = parent;
            
            // Check files
            string[] fileEntries = Directory.GetFiles(MainWindow.FOLDER_NAME, "*.txt");
            foreach (string file in fileEntries)
                fileList.Add(new FileInfo(file).Name);

            if (0 <= index && index < fileList.Count)
                this.index = index;
            else
                this.index = 0;

            // Create the game
            if (fileList.Count > 0)
                game = new Game(fileList[0], this, this);
            else
                parent.NotifyNoLevel();
        }

        public Direction GetDirection()
        {
            int len = inputs.Count;
            if (len > 0 && !menu && Enum.IsDefined(typeof(Direction), (int)inputs[len - 1]))
                return (Direction) inputs[len - 1];

            return Direction.None;
        }

        public string NextFile()
        {
            index++;
            if (index < fileList.Count)
                return fileList[index];
            return null;
        }

        public void Add(UIElement element)
        {
            myCanvas.Children.Add(element);
        }

        public void SetDimension(double width, double height)
        {
            myCanvas.Width = width;
            myCanvas.Height = height;
        }

        public void SetLevelName(string name)
        {
            lblLevelName.Content = name;
        }

        public void Clear()
        {
            myCanvas.Children.Clear();
        }

        public void Close()
        {
            parent.DisplayMenu();
        }

        /// <summary>
        /// Toggle for showing menu.
        /// </summary>
        public void TogglePause()
        {
            if (menu = !menu)
                gridPause.Visibility = Visibility.Visible;
            else
                gridPause.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Give the keyboard's focus.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += OnKeyDown;
            window.KeyUp += OnKeyUp;
        }

        /// <summary>
        /// Event trigger when a key is pressed.
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                TogglePause();

            if (!inputs.Contains(e.Key))
                inputs.Add(e.Key);

            game.MoveEvent();
        }

        /// <summary>
        /// Event trigger when a key is released.
        /// </summary>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            inputs.Remove(e.Key);

            game.MoveEvent();
        }

        /// <summary>
        /// Event trigger when the Resume button is clicked.
        /// </summary>
        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            TogglePause();
        }

        /// <summary>
        /// Event trigger when the Restart button is clicked.
        /// </summary>
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            game.Restart();
            TogglePause();
        }

        /// <summary>
        /// Event trigger when the Menu button is clicked.
        /// </summary>
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event trigger when the Quit button is clicked.
        /// </summary>
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // inputs
        private MainWindow parent;
        private int index;

        // Output
        public UIElement Transition => gridLevelName;

        // Tools
        private Game game = null;
        private ArrayList inputs = new ArrayList();
        private bool menu = false;
        private List<string> fileList = new List<string>();
    }
}
