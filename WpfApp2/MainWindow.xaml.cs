using System;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        // Объявление трех объектов класса User для представления пользователей
        private User user1;
        private User user2;
        private User user3;

        // Конструктор класса MainWindow
        public MainWindow()
        {
            InitializeComponent();

            // Создание трех пользователей с начальными параметрами
            user1 = new User(0, 1.0);
            user2 = new User(10, 0.8);
            user3 = new User(-5, 1.2);

            // Подписка на события перемещения и сжатия для каждого пользователя
            user1.Move += User_Move;
            user2.Move += User_Move;
            user3.Move += User_Move;

            user2.Compress += User_Compress;
            user3.Compress += User_Compress;
        }

        // Обработчик события нажатия кнопки "MoveButton"
        private void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Вызов методов перемещения для каждого пользователя
            user1.MoveUser(5);
            user2.MoveUser(-3);
            user3.MoveUser(8);
        }

        // Обработчик события нажатия кнопки "CompressButton"
        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            // Вызов методов сжатия для каждого пользователя
            user2.CompressUser(0.9);
            user3.CompressUser(1.1);
        }

        // Обработчик события перемещения пользователя
        private void User_Move(object sender, int newPosition)
        {
            // Добавление текста о перемещении пользователя в текстовое поле
            User user = sender as User;
            outputTextBox.AppendText($"User moved to position: {newPosition}\n");
        }

        // Обработчик события сжатия пользователя
        private void User_Compress(object sender, double newCompressionFactor)
        {
            // Добавление текста о сжатии пользователя в текстовое поле
            User user = sender as User;
            outputTextBox.AppendText($"User compressed with factor: {newCompressionFactor}\n");
        }
    }

    // Класс, представляющий пользователя
    public class User
    {
        // Событие перемещения пользователя
        public event EventHandler<int> Move;

        // Событие сжатия пользователя
        public event EventHandler<double> Compress;

        // Положение пользователя
        private int position;

        // Коэффициент сжатия пользователя
        private double compressionFactor;

        // Конструктор класса User
        public User(int initialPosition, double initialCompressionFactor)
        {
            position = initialPosition;
            compressionFactor = initialCompressionFactor;
        }

        // Метод для перемещения пользователя
        public void MoveUser(int offset)
        {
            position += offset;
            OnMove(position);
        }

        // Метод для сжатия пользователя
        public void CompressUser(double factor)
        {
            compressionFactor *= factor;
            OnCompress(compressionFactor);
        }

        // Вызов события перемещения пользователя
        protected virtual void OnMove(int newPosition)
        {
            Move?.Invoke(this, newPosition);
        }

        // Вызов события сжатия пользователя
        protected virtual void OnCompress(double newCompressionFactor)
        {
            Compress?.Invoke(this, newCompressionFactor);
        }
    }
}
