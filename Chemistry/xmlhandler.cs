// xmlhandler.cs created with MonoDevelop
// User: paul at 2:14 PM 5/2/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace Chemistry
{
    public class xmlhandler : Form
    {
        public string element_name, element_symbol, element_structure, picname;
        public double element_weight;
        public int element_number;

        public xmlhandler()
        {
        }

        public void dotheread(int atno)
        {
            ElementNames elements;
            elements = null;
            try
            {
                string path_env = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
                XmlSerializer s = new XmlSerializer(typeof(ElementNames));
                TextReader r = new StreamReader(path_env + "elements.xml");
                elements = (ElementNames)s.Deserialize(r);
                r.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Unable to find the elements information file", "File not found", MessageBoxButtons.OK);
                return;
            }
            element_name = elements.elements[atno].name;
            element_number = elements.elements[atno].atnumber;
            element_symbol = elements.elements[atno].symbol;
            element_structure = elements.elements[atno].structure;
            element_weight = elements.elements[atno].mass;
            picname = "Chemistry.Resources." + elements.elements[atno].picture + ".jpg";
        }

        public void savequestions()
        {
        }

        public void loadquestions(string name)
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xmlhandler));
            this.SuspendLayout();
            // 
            // xmlhandler
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "xmlhandler";
            this.ResumeLayout(false);

        }

    }

    [XmlRoot("Elements")]
    public class ElementNames
    {
        private ArrayList elementData;

        public ElementNames()
        {
            elementData = new ArrayList();
        }

        [XmlElement("Element")]
        public Elements[] elements
        {
            get
            {
                Elements[] elements = new Elements[elementData.Count];
                elementData.CopyTo(elements);
                return elements;
            }
            set
            {
                if (value == null)
                    return;
                Elements[] elements = (Elements[])value;
                elementData.Clear();
                foreach (Elements element in elements)
                    elementData.Add(element);
            }
        }

        public int AddItem(Elements element)
        {
            return elementData.Add(element);
        }
    }

    public class Elements
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("symbol")]
        public string symbol;
        [XmlAttribute("structure")]
        public string structure;
        [XmlAttribute("picture")]
        public string picture;
        [XmlAttribute("atnumber")]
        public int atnumber;
        [XmlAttribute("atmass")]
        public double mass;

        public Elements()
        {
        }

        public Elements(string Name, string Symbol, string Structure, string Picture, int Number, double Mass)
        {
            name = Name;
            symbol = Symbol;
            structure = Structure;
            picture = Picture;
            atnumber = Number;
            mass = Mass;
        }
    }


}