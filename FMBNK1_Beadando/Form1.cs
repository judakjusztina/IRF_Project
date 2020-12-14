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
        BindingList<HallgatoAdat> Elegtelen = new BindingList<HallgatoAdat>();

       
        public Form1()
        {
            InitializeComponent();

            xmlfeldolgozas();

            dataGridView1.DataSource = Adatok;
            dataGridView2.DataSource = Elegtelen;
        }
        public void xmlfeldolgozas()
        {
            Adatok.Clear();

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

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Elegtelen.Clear();

            XmlDocument xml = new XmlDocument();
            xml.Load("Hallgatok.xml");

            foreach (XmlNode node in xml.DocumentElement)
            {
                
                if (checkBoxhianyzas.Checked && checkboxvizsga.Checked)
                {
                    if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value  || int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text)) 
                    {
                        var adat = new HallgatoAdat();

                        adat.nev = node.FirstChild.InnerText;
                        adat.neptun = node.ChildNodes[1].InnerText;
                        adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                        adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                        adat.beadando = node.ChildNodes[4].InnerText;

                        Elegtelen.Add(adat);
                    }
                }
                else if (checkBoxhianyzas.Checked)
                {
                    if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value)
                    {
                        var adat = new HallgatoAdat();

                        adat.nev = node.FirstChild.InnerText;
                        adat.neptun = node.ChildNodes[1].InnerText;
                        adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                        adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                        adat.beadando = node.ChildNodes[4].InnerText;

                        Elegtelen.Add(adat);
                    }
                }
                else if (checkboxvizsga.Checked)
                {
                    if (int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text))
                    {
                        var adat = new HallgatoAdat();

                        adat.nev = node.FirstChild.InnerText;
                        adat.neptun = node.ChildNodes[1].InnerText;
                        adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                        adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                        adat.beadando = node.ChildNodes[4].InnerText;

                        Elegtelen.Add(adat);
                    }
                }

            }

        }


    }
}
