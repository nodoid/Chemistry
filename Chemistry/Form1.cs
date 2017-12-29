using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Chemistry
{
    public partial class Form1 : Form
    {

        private Label[] elementBoxes;
        string[] names = new string[] {"H","He",
                        "Li","Be","B","C","N","O","F","Ne",
                        "Na","Mg","Al","Si","P","S","Cl","Ar",
                        "K","Ca","Sc","Ti","V","Cr","Mn","Fe","Co","Ni","Cu","Zn","Ga","Ge","As","Se","Br","Kr",
                        "Rb","Sr","Y","Zr","Nb","Mo","Tc","Ru","Rh","Pd","Ag","Cd","In","Sn","Sb","Te","I","Xe",
                        "Cs","Ba","La",
                        "Ce","Pr","Nd","Pm","Sm","Eu","Gd","Tb","Dy","Ho","Er","Tm","Yb","Lu",
                        "Hf","Ta","W","Re","Os","Ir","Pt","Au","Hg","Tl","Pb","Bi","Po","At","Rn",
                        "Fr","Ra","Ac",
                        "Th","Pa","U","Np","Pu","Am","Cm","Bk","Cf","Es","Fm","Md","No","Lr",
                        "Rf","Db","Sg","Bh","Hs","Mt", "Ds", "Rg"};

        public Form1()
        {
            InitializeComponent();
            generateElements();
        }

        private void generateElements()
        {
            Label[] ele = new Label[120];

            int[] x = new int[] {5, 566, 5, 38, 401, 434, 467, 500, 533, 566, 5, 38, 401, 434, 467, 500, 533, 566,
            5, 38, 71, 104, 137, 170, 203, 236, 269, 302, 335, 368, 401, 434, 467, 500, 533, 566,
            5, 38, 71, 104, 137, 170, 203, 236, 269, 302, 335, 368, 401, 434, 467, 500, 533, 566,
            5, 38, 71, 137, 170, 203, 236, 269, 302, 335, 368, 401, 434, 467, 500, 533, 566,
            104, 137, 170, 203, 236, 269, 302, 335, 368, 401, 434, 467, 500, 533, 566,
            5, 38, 71, 137, 170, 203, 236, 269, 302, 335, 368, 401, 434, 467, 500, 533, 566,
            104, 137, 170, 203, 236, 269, 302, 335};

            int[] y = new int[] {66, 66, 99, 99, 99, 99, 99, 99, 99, 99, 132, 132, 132, 132, 132, 132, 132, 132,
            165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165, 165,
            198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198, 198,
            231, 231, 231, 310, 310, 310, 310, 310, 310, 310, 310, 310, 310, 310, 310, 310, 310,
            231, 231, 231, 231, 231, 231, 231, 231, 231, 231, 231, 231, 231, 231, 231,
            264, 264, 264, 343, 343, 343, 343, 343, 343, 343, 343, 343, 343, 343, 343, 343, 343,
            264, 264, 264, 264, 264, 264, 264, 264};

            int e = 0;
            foreach (string s in names)
            {
                ele[e] = new Label();
                ele[e].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ele[e].Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ele[e].Location = new System.Drawing.Point(x[e], y[e]);
                ele[e].Name = "element" + e.ToString();
                ele[e].Size = new System.Drawing.Size(33, 33);
                ele[e].TabIndex = e;
                ele[e].Text = s;
                ele[e].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                ele[e].Click += new System.EventHandler(this.displaytheelement);
                e++;
            }
            elementBoxes = ele;
            groupBox1.Controls.AddRange(ele);

            display(0);
        }
        private int unknown;

        private void calculation(List<double> vals)
        {
            double sta = vals[0], cna = vals[1], vla = vals[2];
            double stb = vals[3], cnb = vals[4], vlb = vals[5];
            double known = 0, notknown = 0;
            if (unknown < 2)
                known = stb == 1 ? cnb * (vlb / 1000) : (cnb / stb) * (vlb / 1000);
            else
                known = sta == 1 ? cna * (vla / 1000) : (cna / sta) * (vla / 1000);
            switch (unknown)
            {
                // unknown [a]
                case 0:
                    notknown = sta == 1 ? known * (1000 / vla) : (known / sta) * (1000 / vla);
                    ca.Value = Convert.ToDecimal(notknown);
                    break;
                // unknown Va
                case 1:
                    notknown = sta == 1 ? (known * 1000) / cna : (known * 1000) / (cna / sta);
                    va.Value = Convert.ToDecimal(notknown);
                    break;
                // unknown [b]
                case 2:
                    notknown = stb == 1 ? known * (1000 / vlb) : (known / stb) * (1000 / vlb);
                    cb.Value = Convert.ToDecimal(notknown);
                    break;
                // unknown Vb
                case 3:
                    notknown = stb == 1 ? (1000 * known) / cnb : (1000 * known) / (cnb / stb);
                    vb.Value = Convert.ToDecimal(notknown);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> info = new List<double>();
            info.Add(Convert.ToDouble(sa.Value));
            info.Add(Convert.ToDouble(ca.Value));
            info.Add(Convert.ToDouble(va.Value));
            info.Add(Convert.ToDouble(sb.Value));
            info.Add(Convert.ToDouble(cb.Value));
            info.Add(Convert.ToDouble(vb.Value));
            calculation(info);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            unknown = 0;
            ca.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ca.Value = 0;
            ca.Enabled = false;
            unset(0, 1, 1, 1);
            unknown = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            va.Value = 0;
            va.Enabled = false;
            unset(1, 0, 1, 1);
            unknown = 1;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            cb.Value = 0;
            cb.Enabled = false;
            unset(1, 1, 0, 1);
            unknown = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            vb.Value = 0;
            vb.Enabled = false;
            unset(1, 1, 1, 0);
            unknown = 3;
        }

        private void unset(int a, int b, int c, int d)
        {
            ca.Enabled = a == 0 ? false : true;
            va.Enabled = b == 0 ? false : true;
            cb.Enabled = c == 0 ? false : true;
            vb.Enabled = d == 0 ? false : true;
        }

        void displaytheelement(object sender, EventArgs e)
        {
            int m = ((Label)sender).TabIndex;
            display(m);
        }

        private void display(int m)
        {
            xmlhandler xml = new xmlhandler();
            xml.dotheread(m);
            lblPeriodicEStruct.Text = xml.element_structure;
            double ew = xml.element_weight;
            lblPeriodicMass.Text = ew.ToString();
            double en = xml.element_number;
            lblPeriodicAtNum.Text = en.ToString();
            lblPeriodicElement.Text = xml.element_name;
            lblPeriodicSymbol.Text = xml.element_symbol;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            try
            {
                if (en == 99 || en >= 101)
                    pictureBox1.Image = new Bitmap(Chemistry.Properties.Resources.Radioactive);
                else
                {
                    Stream resourceStream = executingAssembly.GetManifestResourceStream(xml.picname);
                    pictureBox1.Image = new Bitmap(resourceStream);
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Unable to find element picture", "Picture not found", MessageBoxButtons.OK);
            }
        }

        public class element
        {
            public string el;
            public double num;
            public element(string e, double n)
            {
                this.el = e;
                this.num = n;
            }
        }

        List<element> elem = new List<element>();

        string[] elements = new string[] {"H","He",
                        "Li","Be","B","C","N","O","F","Ne",
                        "Na","Mg","Al","Si","P","S","Cl","Ar",
                        "K","Ca","Sc","Ti","V","Cr","Mn","Fe","Co","Ni","Cu","Zn","Ga","Ge","As","Se","Br","Kr",
                        "Rb","Sr","Y","Zr","Nb","Mo","Tc","Ru","Rh","Pd","Ag","Cd","In","Sn","Sb","Te","I","Xe",
                        "Cs","Ba","La",
                        "Ce","Pr","Nd","Pm","Sm","Eu","Gd","Tb","Dy","Ho","Er","Tm","Yb","Lu",
                        "Hf","Ta","W","Re","Os","Ir","Pt","Au","Hg","Tl","Pb","Bi","Po","At","Rn",
                        "Fr","Ra","Ac",
                        "Th","Pa","U","Np","Pu","Am","Cm","Bk","Cf","Es","Fm","Md","No","Lr",
                        "Rf","Db","Sg","Bh","Hs","Mt", "Ds", "Rg"};

        double[] atmass = new double[111] {1.0079,4.0026,
                        6.941,9.01218,10.8,12.011,14.0067,15.9994,18.9984,20.179,
                        22.9898,24.305,26.9815,28.0855,30.9738,32.06,35.453,39.948,
                        39.0983,40.08,44.9559,47.88,50.9415,51.996,54.9380,55.847,58.9332,58.69,63.546,65.38,69.72,72.59,74.9216,78.96,79.904,83.8,
                        85.4679,87.62,88.9059,91.22,92.9064,95.94,98,101.07,102.9055,106.42,107.868,112.41,114.82,118.69,121.75,127.6,126.9045,131.29,
                        132.9054,137.33,138.9055,
                        140.12,140.9077,144.24,145,150.36,151.96,157.25,158.9254,162.5,164.9304,167.26,168.9342,173.04,174.967,
                        178.49,180.9479,183.85,186.207,190.2,192.22,195.08,196.9665,200.59,204.383,207.2,208.9804,209,210,222,
                        223,226.0254,227.0278,
                        232.0381,231.0359,238.0289,237.0482,244,243,247,247,251,252,257,258,259,260,
                        261,262,263,264,265,266,267,268 };

        string[] numbers = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        double atommass(string e)
        {
            double c = -1;
            for (int a = 0; a < 111; a++)
            {
                if (elements[a].CompareTo(e) == 0)
                {
                    c = atmass[a];
                    break;
                }
            }
            return c;
        }

        void search(string formula, bool dot)
        {
            int mult1 = 1, mult2 = 1, s = 0, p = 0, bs = 0, be = 0, bn = 0;
            bool hasdot = formula.Contains(".") ? true : false;
            bool hasbrace = formula.Contains("(") ? true : false;
            int point = hasdot == true ? formula.IndexOf(".") + 1 : 0;

            if (hasbrace == true)
            {
                int k = 0;
                for (k = 0; k < formula.Length; ++k)
                {
                    if (formula[k] == '(')
                        bs = k;
                    if (formula[k] == ')')
                        be = k;
                }
                k = formula.IndexOf(")") + 1;
                if (formula[k] >= '0' && formula[k] <= '9')
                {
                    int c = 0;
                    while (formula[k + c] >= '0' && formula[k + c] <= '9')
                        c++;
                    bn = Int32.Parse(formula.Substring(k, c));
                }
                else
                    bn = 1;
            }

            if (hasdot == true && dot == true)
            {
                if (formula[point] >= '0' && formula[point] <= '9')
                {
                    int c = 0;
                    while (formula[point + c] >= '0' && formula[point + c] <= '9')
                    {
                        c++;
                    }
                    mult1 = Int32.Parse(formula.Substring(point, c));
                    s = point + 1;
                }
            }
            else
            {
                if (formula[0] >= '0' && formula[0] <= '9')
                {
                    int c = 0;
                    while (formula[c] >= '0' && formula[c] <= '9')
                    {
                        c++;
                    }
                    mult1 = Convert.ToInt32(formula.Substring(0, c));
                    s = 1;
                }
            }

            string twoelem = "   ";
            double newmass = 0;

            if (hasdot == true && dot == false)
                p = formula.IndexOf(".");
            else
                p = formula.Length + 1;

            int loop = s;
            {
                while (loop < p)
                {
                    if (loop + 1 > p || formula[loop] == '#')
                        break;

                    if (loop == bs)
                    {
                        while (loop < be)
                        {
                            if (formula[loop + 1] >= 'a')
                            {
                                twoelem = formula.Substring(loop, 2);
                                loop += 2;
                            }
                            else
                            {
                                twoelem = formula.Substring(loop, 1);
                                loop++;
                            }

                            if (formula[loop] >= '0' && formula[loop] <= '9')
                            {
                                int c = 0;
                                while (formula[loop + c] >= '0' && formula[loop + c] <= '9')
                                {
                                    c++;
                                }
                                if (twoelem == "")
                                {
                                    if (formula[loop + c + 1] < 'a')
                                        twoelem = formula.Substring(loop + c, 1);
                                    else
                                        twoelem = formula.Substring(loop + c, 2);
                                }
                                mult2 = Int32.Parse(formula.Substring(loop, c));
                                loop += c;
                            }
                            newmass = atommass(twoelem) * mult2 * bn;
                            elem.Add(new element(twoelem, newmass));
                            newmass = 0;
                            mult2 = 1;
                        }
                    }
                    if (formula[loop + 1] >= 'a')
                    {
                        twoelem = formula.Substring(loop, 2);
                        loop += 2;
                    }
                    else
                    {
                        twoelem = formula.Substring(loop, 1);
                        loop++;
                    }

                    if (formula[loop] >= '0' && formula[loop] <= '9')
                    {
                        int c = 0;
                        while (formula[loop + c] >= '0' && formula[loop + c] <= '9')
                        {
                            c++;
                        }
                        if (twoelem == "")
                        {
                            if (formula[loop + c + 1] < 'a')
                                twoelem = formula.Substring(loop + c, 1);
                            else
                                twoelem = formula.Substring(loop + c, 2);
                        }
                        mult2 = Int32.Parse(formula.Substring(loop, c));
                        loop += c;
                    }
                    newmass = atommass(twoelem) * mult2 * mult1;
                    elem.Add(new element(twoelem, newmass));
                    newmass = 0;
                    mult2 = 1;
                }
            }
            if (dot == false && hasdot == true)
                search(formula, true);
        }

        void cleardisplay(object sender, EventArgs e)
        {
            lblRMM.Text = "0";
            lblMass.Text = "0g";
            tbMoles.Text = "";
            tbFormula.Text = "";
        }

        void destroylist()
        {
            elem.Clear();
        }

        void results()
        {
            double weight = 0.0;
            elem.ForEach(delegate(element e)
            {
                weight += e.num;
            });

            double pw = Convert.ToDouble(nudPower.Value);
            double nw = 0.0;
            if (pw != 0)
            {
                if (tbMoles.Text == "" || tbMoles.Text == "1")
                {
                    if (pw < 0.0)
                    {
                        //nw = 1 / Math.Pow(weight, Math.Abs(pw));
                        nw = weight * (1 / Math.Pow(10, Math.Abs(pw)));
                    }
                    else
                    {
                        nw = weight * Math.Pow(10, pw - 1);
                    }
                }
                else
                {
                    if (pw < 0.0)
                    {
                        nw = weight * (1 / Math.Pow(10, Math.Abs(pw)));
                    }
                    else
                    {
                        nw = weight * Double.Parse(tbMoles.Text) * Math.Pow(10, pw - 1);
                    }
                }
            }
            else
                nw = 1;
            lblRMMAns.Text = weight.ToString("f4");
            lblMass.Text = nw.ToString("f4") + "g";
            destroylist();
        }

        void calculate(object sender, EventArgs e)
        {
            if (tbFormula.Text.Length == 0)
            {
                MessageBox.Show("Error : You haven't entered a formula", "Formula error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((tbFormula.Text.Contains("(") && !tbFormula.Text.Contains(")")) ||
                (tbFormula.Text.Contains(")") && tbFormula.Text.Contains("(")))
            {
                MessageBox.Show("Error : You have a missing brace within your formula. Please recheck", "Bracket error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (formula.Text.Length == 1)
            {
                bool test = true;
                foreach (string element in elements)
                {
                    if (tbFormula.Text.Contains(element))
                    {
                        test = false;
                        continue;
                    }
                }
                if (test == true)
                {
                    MessageBox.Show("Error : Your formula contains an unknown element", "Unknown element", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tbFormula.Text.Contains("."))
            {
                int pos = tbFormula.Text.IndexOf(".");
                if (pos == tbFormula.Text.Length)
                {
                    MessageBox.Show("Error : You have a period followed by nothing", "Period error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string dupeform = tbFormula.Text;

            if (dupeform.Contains("("))
                dupeform = dupeform.Remove(dupeform.IndexOf("("), 1);
            if (dupeform.Contains(")"))
                dupeform = dupeform.Remove(dupeform.IndexOf(")"), 1);
            if (dupeform.Contains("."))
                dupeform = dupeform.Remove(dupeform.IndexOf("."), 1);
            for (int a = 0; a < dupeform.Length; ++a)
            {
                foreach (string n in numbers)
                {
                    if (dupeform.Contains(n))
                        dupeform = dupeform.Remove(dupeform.IndexOf(n), 1);
                }
            }

            dupeform = tbFormula.Text + "#";
            search(dupeform, false);
            results();
        }

        private void aboutThisSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about abt = new about();
            abt.Show();
        }
    }
}
