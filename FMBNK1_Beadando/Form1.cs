using FMBNK1_Beadando.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            dataGridView2.Visible = false;
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


        private void btnDelete_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            Elegtelen.Clear();

            XmlDocument xml = new XmlDocument();
            xml.Load("Hallgatok.xml");

            dataGridView2.Visible = true;

            foreach (XmlNode node in xml.DocumentElement)
            {
                //Mindhárom feltétel aktív
                if (checkBoxhianyzas.Checked && checkboxvizsga.Checked && checkboxbead.Checked)
                {
                    if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value  || int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text) || node.ChildNodes[4].InnerText == "Nem") 
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
                else
                {
                  //Hiányzás és vizsga aktív
                  if (checkBoxhianyzas.Checked && checkboxvizsga.Checked)
                    {
                        if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value || int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text))
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
                    //Hiányzás és beadandó aktív
                    else if (checkBoxhianyzas.Checked && checkboxbead.Checked)
                    {
                        if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value || node.ChildNodes[4].InnerText == "Nem")
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
                    //Vizsga és beadandó aktív
                    else if (checkboxvizsga.Checked && checkboxbead.Checked)
                    {
                        if (int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text) || node.ChildNodes[4].InnerText == "Nem")
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
                    //Csak hiányzás aktív
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
                    //Csak vizsga aktív
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
                    //Csak beadandó aktív
                    else if (checkboxbead.Checked)
                    {
                        if (node.ChildNodes[4].InnerText == "Nem")
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
            //Hallgatók számának megjelenítése
            label7.Text = (dataGridView2.Rows.Count).ToString();
        }
        
        //Mentés funkció 
        private void btnSave_Click(object sender, EventArgs e)
        {
            const string message = "A mentett fáljban csak a hallgatók Neptun-kódja szerepeljen?";
            const string caption = "Mentés";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result== DialogResult.Cancel)
            {
                
            }
            else if (result == DialogResult.No)
            {
                mentes();
            }
            else if (result == DialogResult.Yes)
            {
                mentesnevnelkul();
            }

        }
        private void mentes()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var a in Adatok)
                {
                    sw.Write(a.nev);
                    sw.Write(";");
                    sw.Write(a.neptun);
                    sw.Write(";");
                    sw.Write(a.reszvetel);
                    sw.Write(";");
                    sw.Write(a.pontszam);
                    sw.Write(";");
                    sw.Write(a.beadando);
                    sw.WriteLine();

                }
            }
        }
        private void mentesnevnelkul()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var a in Adatok)
                {
                    sw.Write(a.neptun);
                    sw.Write(";");
                    sw.Write(a.reszvetel);
                    sw.Write(";");
                    sw.Write(a.pontszam);
                    sw.Write(";");
                    sw.Write(a.beadando);
                    sw.WriteLine();

                }
            }
        }
    }
}
