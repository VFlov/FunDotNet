namespace WallpaperChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // ��������� ����� ��� �����
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
            TopMost = true;
            TransparencyKey = Color.Black; // ���� ������������
            //BackColor = Color.Black; // ��� ��� ������������
            //ShowInTaskbar = false; // ������ � ������ �����

            // �������� ���������� ������� ��� ����
            MouseDown += OnMouseDown;
        }

        // ��������� ������� ����
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // ���������� ���������
            MessageBox.Show("�� ������ �� ����!");
        }
    
    }
}
