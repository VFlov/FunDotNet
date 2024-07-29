namespace WallpaperChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Установка формы без рамки
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
            TopMost = true;
            TransparencyKey = Color.Black; // Ключ прозрачности
            //BackColor = Color.Black; // Фон для прозрачности
            //ShowInTaskbar = false; // Скрыть в панели задач

            // Добавить обработчик события для мыши
            MouseDown += OnMouseDown;
        }

        // Обработка нажатия мыши
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Отобразить сообщение
            MessageBox.Show("Вы нажали на обои!");
        }
    
    }
}
