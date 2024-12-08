using System;
using System.Windows.Forms;

namespace SimplexProxyForm
{
    public partial class Form1 : Form
    {
        private readonly Simplex _client;

        public Form1()
        {
            _client = new Simplex();
            InitializeComponent();
        }

        private void Sum_Click(object sender, EventArgs e)
        {
            try
            {
                A a1 = new A
                {
                    s = A1s.Text,
                    k = int.TryParse(A1k.Text, out int a1k)
                        ? a1k
                        : throw new ArgumentException("A1.k is invalid"),
                    f = float.TryParse(A1f.Text, out float a1f)
                        ? a1f
                        : throw new ArgumentException("A1.f is invalid"),
                };

                A a2 = new A
                {
                    s = A2s.Text,
                    k = int.TryParse(A2k.Text, out int a2k)
                        ? a2k
                        : throw new ArgumentException("A2.k is invalid"),
                    f = float.TryParse(A2f.Text, out float a2f)
                        ? a2f
                        : throw new ArgumentException("A2.f is invalid"),
                };

                A a3 = _client.Sum(a1, a2);

                A3s.Text = a3.s;
                A3k.Text = a3.k.ToString();
                A3f.Text = a3.f.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}
