using Lab_3_App.Convert;
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
using System.Diagnostics;


namespace Lab_3_App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CurrencyConverter currencyConverter;
        private Style customLabelStyle; // Додайте поле для зберігання стилю
        public MainWindow()
        {
            InitializeComponent();
            SizeChanged += MainWindow_SizeChanged; //зміна орієнатції вмісту елементів при зменшенні ширини вікна
            currencyConverter = new CurrencyConverter();
            customLabelStyle = (Style)FindResource("CustomLabelStyle"); // Отримайте ресурс стилю з ресурсів вікна

        }

        //зміна орієнатції вмісту елементів при зменшенні ширини вікна
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 520)
            {
                MainStackPanel.Orientation = Orientation.Vertical;
                Height = 750;
            }
            else
            {
                MainStackPanel.Orientation = Orientation.Horizontal;
            }
        }


        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                // Отримуємо дані з першого текстового поля
                double amount = double.Parse(textBoxAmount.Text);
                string fromCurrency = comboBoxFromCurrency.Text;
                string toCurrency = comboBoxToCurrency.Text;

                // Викликаємо метод конвертації
                double result = currencyConverter.Convert(amount, fromCurrency, toCurrency);

                // Виводимо результат у друге текстове поле
                textBoxResult.Text = result.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка конвертації: Не було обрано валюту або не було зазначено кількість", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBoxAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Очистити результат, коли відбувається зміна в першому текстовому полі
            textBoxResult.Text = "";
        }


        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            // Перевірити, чи textBoxResult не є пустим
            if (!string.IsNullOrEmpty(textBoxResult.Text))
            {
                // Копіювати значення з textBoxResult до textBoxAmount
                textBoxAmount.Text = textBoxResult.Text;
            }

            // Зберегти обрані варіанти списків
            string selectedFromCurrency = comboBoxFromCurrency.Text;
            string selectedToCurrency = comboBoxToCurrency.Text;

            // Змінити обрані варіанти списків
            comboBoxFromCurrency.Text = selectedToCurrency;
            comboBoxToCurrency.Text = selectedFromCurrency;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Копіювати значення з textBoxResult до textBoxAmount
            textBoxAmount.Text = "";
            textBoxResult.Text = "";


        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            


            double minWidthSmall = 500; // Мінімальна ширина для малих розмірів бордера
            double minHeightSmall = 400; // Мінімальна висота для малих розмірів бордера

            double minWidthMedium = 520; // Мінімальна ширина для середніх розмірів бордера
            double minHeightMedium = 450; // Мінімальна висота для середніх розмірів бордера

            double minWidthLarge = 620; // Мінімальна ширина для великих розмірів бордера
            double minHeightLarge = 520; // Мінімальна висота для великих розмірів бордера

            // Отримуємо розмір вікна
            double windowHeight = this.ActualHeight;
            double windowWidth = this.ActualWidth;

            // Змінюємо розмір бордера в залежності від ширини вікна
            if (windowHeight > 600)
            {
                border.Width = minWidthLarge;
                border.Height = minHeightLarge;
            }
            else if (windowHeight >= 540 && windowHeight <= 600)
            {
                border.Width = minWidthMedium;
                border.Height = minHeightMedium;
            }
            else if (windowHeight < 540 && windowWidth < 540)
            {
                border.Width = minWidthSmall;
                border.Height = minHeightSmall;
            }

            if (e.NewSize.Width < 520)
            {
                // При натисканні на розмір менше 541 встановіть розмір бордера на висоту та ширину вікна
                border.Width = e.NewSize.Width - 50;
                border.Height = 700;
            }
        }

        private void Font_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Отримайте нову ширину вікна
            double newWidth = e.NewSize.Width;

            // Якщо ширина вікна менше 400, змініть розмір шрифту на відповідний
            if (newWidth < 500)
            {
                double scaleFactor = newWidth / 550; // Обчисліть масштабний фактор
                double newFontSize = 32 * scaleFactor; // Помножте розмір шрифту на масштабний фактор

                // Створіть новий стиль і застосуйте його до Label
                Style newStyle = new Style(typeof(Label), customLabelStyle);
                newStyle.Setters.Add(new Setter(Label.FontSizeProperty, newFontSize));
                customLabelStyle = newStyle;
                mainLable.Style = customLabelStyle;
            }
            else
            {
                // Якщо ширина вікна більше або рівна 400, поверніть розмір шрифту до вихідного значення
                mainLable.Style = customLabelStyle;
            }
        }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Перевіряємо чи введений символ є цифрою або крапкою
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",")
            {
                e.Handled = true; // Якщо символ не є цифрою або крапкою, то він не буде оброблений
            }
            else if (e.Text == ",")
            {
                // Перевіряємо, чи крапка є вже присутня в тексті
                if (textBox.Text.Contains(","))
                {
                    e.Handled = true; // Якщо крапка вже присутня, то блокуємо введення
                }
            }
            else if (textBox.Text.Contains(","))
            {
                // Якщо крапка присутня, перевіряємо, чи кількість цифр після неї не перевищує 2
                int dotIndex = textBox.Text.IndexOf(",");
                if (textBox.Text.Length - dotIndex > 2)
                {
                    e.Handled = true; // Блокуємо введення, якщо кількість цифр після крапки більше 2
                }
            }
        }

    }
}
