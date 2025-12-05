using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MERCADIN
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProdutos frm = new FormProdutos();
            frm.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClientes frm = new FormClientes();
            frm.ShowDialog();
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCaixa frm = new FormCaixa();
            frm.ShowDialog();
        }

        private void relatorioDeVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRelatórios frm = new FormRelatórios();
            frm.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
