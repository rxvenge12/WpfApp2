using System;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private User user1;
        private User user2;
        private User user3;

        public MainWindow()
        {
            InitializeComponent();

            // Создание объектов пользователей
            user1 = new User(0, 1.0);
            user2 = new User(10, 0.8);
            user3 = new User(-5, 1.2);

            // Подписка на события
            user1.Move += User_Move;
            user2.Move += User_Move;
            user3.Move += User_Move;

            user2.Compress += User_Compress;
            user3.Compress += User_Compress;
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            user1.MoveUser(5);
            user2.MoveUser(-3);
            user3.MoveUser(8);
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            user2.CompressUser(0.9);
            user3.CompressUser(1.1);
        }

        private void User_Move(object sender, int newPosition)
        {
            User user = sender as User;
            outputTextBox.AppendText($"User moved to position: {newPosition}\n");
        }

        private void User_Compress(object sender, double newCompressionFactor)
        {
            User user = sender as User;
            outputTextBox.AppendText($"User compressed with factor: {newCompressionFactor}\n");
        }
    }

    public class User
    {
        public event EventHandler<int> Move;
        public event EventHandler<double> Compress;

        private int position;
        private double compressionFactor;

        public User(int initialPosition, double initialCompressionFactor)
        {
            position = initialPosition;
            compressionFactor = initialCompressionFactor;
        }

        public void MoveUser(int offset)
        {
            position += offset;
            OnMove(position);
        }

        public void CompressUser(double factor)
        {
            compressionFactor *= factor;
            OnCompress(compressionFactor);
        }

        protected virtual void OnMove(int newPosition)
        {
            Move?.Invoke(this, newPosition);
        }

        protected virtual void OnCompress(double newCompressionFactor)
        {
            Compress?.Invoke(this, newCompressionFactor);
        }
    }
}
