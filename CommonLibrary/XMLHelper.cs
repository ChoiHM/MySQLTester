namespace CommonLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Linq;

    public class XMLHelper
    {
        protected XDocument XMLDoc;
        public string FilePath { get; set; }
        public XMLHelper(string fileName)
        {
            FilePath = Application.StartupPath + @"\" + fileName;
            Load();
        }

        public XElement Root
        {
            get
            {
                if (XMLDoc == null)
                {
                    XDocument tmp = new XDocument();
                    tmp = XDocument.Parse("<ROOT></ROOT>");
                    XMLDoc = tmp;
                }
                return XMLDoc.Root;
            }
        }

        public void Load()
        {
            try
            {
                XMLDoc = XDocument.Load(FilePath);
            }
            catch
            {
                XDocument tmp = new XDocument();
                tmp = XDocument.Parse("<ROOT></ROOT>");
                XMLDoc = tmp;
            }
        }

        public void Save()
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath) == true)
                {
                    MessageBox.Show("Save실패. 경로가 지정되지 않았습니다.");
                }
                XMLDoc.Save(FilePath, SaveOptions.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static partial class Util
    {
        public static XElement SafeElement(this XElement me, XName name)
        {
            XElement tmp = new XElement(name);
            if (me == null)
            {
                me.Add(tmp);
                return tmp;
            }
            if (me.Element(name) == null)
            {
                me.Add(tmp);
                return tmp;
            }
            else
            {
                return me.Element(name);
            }
        }

        public static IEnumerable<XElement> SafeElements(this XElement me, XName name)
        {
            if (me == null) return Enumerable.Empty<XElement>();
            return me.Elements(name) ?? Enumerable.Empty<XElement>();
        }

        public static XAttribute SafeAttribute(this XElement me, XName name)
        {
            XAttribute tmp = new XAttribute(name, string.Empty);
            if (me == null)
            {
                return tmp;
            }
            if (me.Attribute(name) == null)
            {
                me.Add(tmp);
                return tmp;
            }
            else
            {
                return me.Attribute(name);
            }
        }
    }
}