using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using com.sun.corba.se.impl.orbutil.graph;
using edu.stanford.nlp.trees;
using java.util;

namespace Anlis.Core
{
    public class ParsedStatementFactory
    {
        public static ParseResult CreateParsedStatement(string xml)
        {
            var serializer = new XmlSerializer(typeof(ParseResult));
            var statement = (ParseResult)serializer.Deserialize(new StringReader(xml));
            statement.Xml = xml;
            return statement;
        }

        [XmlRoot("s")]
        public class ParseResult
        {
            public string Xml { get; set; }

            [XmlElement("tree")]
            public StatementTree Tree { get; set; }

            [XmlArrayItem("dep")]
            [XmlArray("dependencies")]
            public Dependency[] Dependencies { get; set; }

            public override string ToString()
            {
                return Xml;
            }
        }

        public class StatementTree
        {
            public StatementNode node { get; set; }
        }

        public class Leaf
        {
            [XmlAttribute("value")]
            public string Value { get; set; }
        }

        public class StatementNode
        {
            [XmlAttribute("value")]
            public string Value { get; set; }

            [XmlElement("node")]
            public StatementNode[] Nodes { get; set; }

            [XmlElement("leaf")]
            public Leaf Leaf { get; set; }
        }
        
        public class Dependency
        {
            [XmlElement("governor")]
            public string Governor { get; set; }
            [XmlElement("dependent")]
            public string Dependent { get; set; }
        }

       
    }
}