using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace Zhaopin.IbatisFactory.Common
{
    [Serializable]
    [XmlRoot("folders")]
    public class FolderClass
    {
        [XmlElement(ElementName = "Item")]
        public List<FolderItem> ItemList { get; set; }
    }

    [XmlRoot(ElementName = "Item")]
    public class FolderItem
    {
        [XmlAttribute(AttributeName = "Code")]
        public int Code { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Path")]
        public string Path { get; set; }
        [XmlAttribute(AttributeName = "NewPath")]
        public string NewPath { get; set; }
        [XmlAttribute(AttributeName = "ItemTemplate")]
        public string ItemTemplate { get; set; }

    
        
    }
}
