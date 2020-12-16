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

        XmlDocument xml = new XmlDocument();

       public Form1()
        {
            InitializeComponent();

            feluletletrehozas();
            xmlfeldolgozas();
           
        }

        public void feluletletrehozas()
        {
            //Összes
            Felulet f = new Felulet();
            f.Left = 40;
            f.Top = 50;
            Controls.Add(f);
            f.DataSource = Adatok;
            f.Columns[0].HeaderText = "Név";
            f.Columns[1].HeaderText = "Neptun";
            f.Columns[2].HeaderText = "Hiányzások";
            f.Columns[3].HeaderText = "Negyedéves pont";
            f.Columns[4].HeaderText = "Beadandó";

            //Elégtelenek
            Felulet l = new Felulet();
            l.Left = 40;
            l.Top = 300;
            Controls.Add(l);
            l.DataSource = Elegtelen;
            l.Columns[0].HeaderText = "Név";
            l.Columns[1].HeaderText = "Neptun";
            l.Columns[2].HeaderText = "Hiányzások";
            l.Columns[3].HeaderText = "Negyedéves pont";
            l.Columns[4].HeaderText = "Beadandó";

            gombok g = new gombok();
        }
        
       
        public void xmlfeldolgozas()
        {
            Adatok.Clear();

            xml.Load("Hallgatok2.xml");

            foreach (XmlNode node in xml.DocumentElement)
            {

                    var adat = new HallgatoAdat();

                    adat.nev = node.FirstChild.InnerText;
                    adat.neptun = node.ChildNodes[1].InnerText;
                    adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                    adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                    adat.beadando = (Beadando)Enum.Parse(typeof(Beadando),(node.ChildNodes[4].InnerText));

                    Adatok.Add(adat);
            }
        }
//LEKÉRDEZÉS
        public void btnGet_Click(object sender, EventArgs e)
        {
            lekerdezes();
        }
        private void lekerdezes()
        {
            Elegtelen.Clear();
            XmlDocument xml = new XmlDocument();
            xml.Load("Hallgatok2.xml");


            foreach (XmlNode node in xml.DocumentElement)
            {
                //Mindhárom feltétel aktív
                if (checkBoxhianyzas.Checked && checkboxvizsga.Checked && checkboxbead.Checked)
                {
                    if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value || int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text) || node.ChildNodes[4].InnerText == "0")
                    {
                        var adat = new HallgatoAdat();

                        adat.nev = node.FirstChild.InnerText;
                        adat.neptun = node.ChildNodes[1].InnerText;
                        adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                        adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                        adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

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
                            adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

                            Elegtelen.Add(adat);
                        }
                    }
                    //Hiányzás és beadandó aktív
                    else if (checkBoxhianyzas.Checked && checkboxbead.Checked)
                    {
                        if (int.Parse(node.ChildNodes[2].InnerText) > numericUpDown1.Value || node.ChildNodes[4].InnerText == "0")
                        {
                            var adat = new HallgatoAdat();

                            adat.nev = node.FirstChild.InnerText;
                            adat.neptun = node.ChildNodes[1].InnerText;
                            adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                            adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                            adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

                            Elegtelen.Add(adat);
                        }
                    }
                    //Vizsga és beadandó aktív
                    else if (checkboxvizsga.Checked && checkboxbead.Checked)
                    {
                        if (int.Parse(node.ChildNodes[3].InnerText) < int.Parse(ExamTB.Text) || node.ChildNodes[4].InnerText == "0")
                        {
                            var adat = new HallgatoAdat();

                            adat.nev = node.FirstChild.InnerText;
                            adat.neptun = node.ChildNodes[1].InnerText;
                            adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                            adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                            adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

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
                            adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

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
                            adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

                            Elegtelen.Add(adat);
                        }
                    }
                    //Csak beadandó aktív
                    else if (checkboxbead.Checked)
                    {
                        if (node.ChildNodes[4].InnerText == "0")
                        {
                            var adat = new HallgatoAdat();

                            adat.nev = node.FirstChild.InnerText;
                            adat.neptun = node.ChildNodes[1].InnerText;
                            adat.reszvetel = int.Parse(node.ChildNodes[2].InnerText);
                            adat.pontszam = int.Parse(node.ChildNodes[3].InnerText);
                            adat.beadando = (Beadando)Enum.Parse(typeof(Beadando), (node.ChildNodes[4].InnerText));

                            Elegtelen.Add(adat);
                        }
                    }
                }
            }

            //Hallgatók számának megjelenítése
            label11.Text = (Elegtelen.Count).ToString();
            feluletletrehozas();
        }
//TÖRLÉS
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (Elegtelen.Count == 0)
            {
                MessageBox.Show("A törléshez először le kell kérdeznie a hallgatókat.");
            }
            else
            {
                const string message = "Biztosan törölni szeretnéd a azokat a hallgatókat, akik nem feleltek meg a követelményeknek? A hallgatók a forrás fájlból is törlésre kerülnek.";
                const string caption = "Törlés";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (HallgatoAdat t in Elegtelen)
                    {
                        foreach (XmlNode xNode in xml.SelectNodes("hallgatok/diak"))
                        {

                            string torlendo = t.neptun;
                            if (xNode.SelectSingleNode("Neptun").InnerText == torlendo) xNode.ParentNode.RemoveChild(xNode);
                        }
                    }
                    xml.Save("Hallgatok2.xml");
                    xmlfeldolgozas();
                    lekerdezes();
                }
            }
        }
//MENTÉS
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            const string message = "A mentett fáljban csak a hallgatók Neptun-kódja szerepeljen?";
            const string caption = "Mentés";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.No)
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
