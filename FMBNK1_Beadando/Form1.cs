using FMBNK1_Beadando.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FMBNK1_Beadando
{
    public partial class Form1 : Form
    {
        BindingList<HallgatoAdat> Adatok = new BindingList<HallgatoAdat>();
       
        public Form1()
        {
            InitializeComponent();

            xmlfeldolgozas();

            dataGridView1.DataSource = Adatok;
        }
        public void xmlfeldolgozas()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("Hallgatok.xml");

            foreach (XmlNode node in xml.DocumentElement)
            {
                var adat = new HallgatoAdat();

                adat.nev = node.FirstChild.InnerText;
                adat.neptun = node.ChildNodes[1].InnerText;
                adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                adat.beadando = node.ChildNodes[4].InnerText;

                Adatok.Add(adat);

            }
        }
    }
}
