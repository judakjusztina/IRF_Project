using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMBNK1_Beadando.Entities
{
    class Felulet : DataGridView
    {
        public Felulet()
        {
            Height = 200;
            Width = 560;
            GridColor = Color.White;
            DefaultCellStyle.Font = new Font("Calibri", 12); 
            DefaultCellStyle.BackColor = Color.Beige;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
    }

}
