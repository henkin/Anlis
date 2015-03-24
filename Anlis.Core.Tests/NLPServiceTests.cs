using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using edu.stanford.nlp.trees;
using NUnit.Framework;
using List = java.util.List;
using Tree = com.sun.source.tree.Tree;

namespace Anlis.Core.Tests
{
    [TestFixture]
    public class NLPServiceTests
    {
        private NlpService _nlpService;


        [TestFixtureSetUp]
        public void Init()
        {
            _nlpService = new NlpService();
        }

        [Test]
        public void ParseStatement()
        {
            var statement = "Bob likes to eat apples after work";
            var nlpService = new NlpService();

            var parse = nlpService.ParseStatement(statement);
        }

        [Test]
        public void ParseRandomStatements()
        {
            var nlpService = new NlpService();

            int count = 100;

            var statements = Enumerable.Range(0, count).Select((x) =>
                RandomPhraseService.GetRandomPhraseFromResource(Assembly.GetExecutingAssembly(), "Data.Kids.txt")
                ).ToList();

            Run(count, nlpService, statements);
        }

        [Test]
        public void ParseSameStatements()
        {
            var statements = RandomPhraseService.GetAllPhrasesFromResource(Assembly.GetExecutingAssembly(), "Data.Kids.txt");

            Run(statements.Count(), _nlpService, statements.ToList());
            Run(statements.Count(), _nlpService, statements.ToList());
            Run(statements.Count(), _nlpService, statements.ToList());
            Run(statements.Count(), _nlpService, statements.ToList());
        }


        [Test]
        public void GetRating()
        {
            
            Console.WriteLine("{0:000}", _nlpService.GetAverageRating());
            Console.WriteLine("{0:000}", _nlpService.GetAverageRating());
            Console.WriteLine("{0:000}", _nlpService.GetAverageRating());
            Console.WriteLine("{0:000}", _nlpService.GetAverageRating());
            Console.WriteLine("{0:000}", _nlpService.GetAverageRating());
            Console.WriteLine("{0:000}", _nlpService.GetAverageRating());
        }


        private static void Run(int count, NlpService nlpService, List<string> statements)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var parsedStatements = nlpService.ParseStatements(statements);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
           
            Console.WriteLine("{0}{1} - {2:00}.{3:00}.{4:00}", count, 'X', ts.Minutes, ts.Seconds, ts.Milliseconds/10);
        }

        [Test]
        public void DeserializeParsedStatement()
        {
            var resourceText = GetResourceText();
            var statement = ParsedStatementFactory.CreateParsedStatement(resourceText);
        }

        [Test]
        public void Deserialize()
        {
            var resourceText = GetResourceText();
            var parseResult = ParsedStatementFactory.CreateParsedStatement(resourceText);

            Assert.IsNotNull(parseResult.Tree);
            Assert.AreEqual("ROOT", parseResult.Tree.node.Value);
            var statementNode = parseResult.Tree.node.Nodes[0];
            Assert.AreEqual("S", statementNode.Value);
            Assert.AreEqual(2, statementNode.Nodes.Count());
            var vbzNode = statementNode.Nodes.Last().Nodes.First();
            Assert.AreEqual("VBZ", vbzNode.Value);
            Assert.AreEqual("likes", vbzNode.Leaf.Value);

            Assert.IsNotNull(parseResult.Dependencies);
            Assert.AreEqual(6, parseResult.Dependencies.Count());
            Assert.AreEqual("after", parseResult.Dependencies.Last().Governor);
            Assert.AreEqual("work", parseResult.Dependencies.Last().Dependent);
        }

        private static string GetResourceText()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetName().Name + "." + "Data.ParsedStatementWithDependencies.txt";

            string fileText = null;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                fileText = reader.ReadToEnd();
            }
            return fileText;
        }
    }

    
}