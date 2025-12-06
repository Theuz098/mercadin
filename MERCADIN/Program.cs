using System;
using System.Windows.Forms;
using SQLitePCL;
// Se o "using SQLitePCL;" der erro, pode remover ele, 
// pois chamaremos o nome completo abaixo.

namespace MERCADIN
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // AGORA ISSO VAI FUNCIONAR:
            SQLitePCL.Batteries_V2.Init();

            DatabaseSetup.InitializeDatabase();
            Application.Run(new FormMenu());
        }
    }
}