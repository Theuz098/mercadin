using System;
using System.Windows.Forms;

namespace MERCADIN
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProdutos formprod = new FormProdutos();
            formprod.ShowDialog();
        }
    }
}
