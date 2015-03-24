using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using java.io;
using java.util;

namespace Anlis.Core
{
    public interface INlpService
    {
        List<ParsedStatementFactory.ParseResult> ParseStatements(IEnumerable<string> input);
        ParsedStatementFactory.ParseResult ParseStatement(string input);
        int GetAverageRating();
    }

    public class NlpService : INlpService
    {
        private LexicalizedParser _lp;
        private TokenizerFactory _tokenizerFactory;
        private GrammaticalStructureFactory _structureFactory;
        private PennTreebankLanguagePack _tlp;

        public NlpService()
        {
            string parserFileOrUrl = "englishPCFG.ser.gz";
            _lp = LexicalizedParser.loadModel(parserFileOrUrl);
            if (_lp == null)
                throw new InvalidOperationException("couldn't load " + parserFileOrUrl);
            _tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");

            _tlp = new PennTreebankLanguagePack();
            _structureFactory = _tlp.grammaticalStructureFactory();
        }

        public void RunDemo()
        {
            //DemoDP(lp, parserFileOrUrl);

            DemoAPI(_lp);
        }

        public static void DemoDP(LexicalizedParser lp, string fileName)
        {
            // This option shows loading and sentence-segment and tokenizing
            // a file using DocumentPreprocessor
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            // You could also create a tokenizer here (as below) and pass it
            // to DocumentPreprocessor
            foreach (List sentence in new DocumentPreprocessor(fileName))
            {
                var parse = lp.apply(sentence);
                parse.pennPrint();

                var gs = gsf.newGrammaticalStructure(parse);
                var tdl = gs.typedDependenciesCCprocessed(true);
                System.Console.WriteLine("\n{0}\n", tdl);
            }
        }

        public static void DemoAPI(LexicalizedParser lp)
        {
            // This option shows parsing a list of correctly tokenized words
            var sent = new[] { "This", "is", "an", "easy", "sentence", "." };
            var rawWords = Sentence.toCoreLabelList(sent);
            var parse = lp.apply(rawWords);
            //parse.pennPrint();
            parse.indentedXMLPrint();

            // This option shows loading and using an explicit tokenizer
            const string Sent2 = "This is another sentence.";
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new StringReader(Sent2);
            var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            parse = lp.apply(rawWords2);

            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(parse);
            var tdl = gs.typedDependenciesCCprocessed();
            System.Console.WriteLine("\n{0}\n", tdl);

            var tp = new TreePrint("penn,typedDependenciesCollapsed");
            tp.printTree(parse);
            System.Console.WriteLine("TreePrint: \n{0}\n", parse);
        }

        public List<ParsedStatementFactory.ParseResult> ParseStatements(IEnumerable<string> input)
        {
                return input
                    .AsParallel()
                    .WithDegreeOfParallelism(200)
                    .Select(ParseStatement)
                    .ToList();
        }

        public ParsedStatementFactory.ParseResult ParseStatement(string input)
        {
            var sent2Reader = new StringReader(input);
            var rawWords2 = _tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            var parse = _lp.apply(rawWords2);

            var gs = _structureFactory.newGrammaticalStructure(parse);
            var tdl = gs.typedDependenciesCCprocessed();
            //System.Console.WriteLine("newGrammaticalStructure:\n{0}\n", gs);
            //System.Console.WriteLine("typedDependenciesCCprocessed:\n{0}\n", tdl);
            //var tp = new TreePrint("penn,typedDependenciesCollapsed");
            //tp.printTree(parse);
            //return new ParsedStatement(parse);

            var xmlTreePrint = new TreePrint("xmlTree, dependencies", "xml, collapsedDependencies", _tlp);
            var stream = new ByteArrayOutputStream();
            xmlTreePrint.printTree(parse, new PrintWriter(stream));

            string xmlOutput = stream.toString() + "</s>";
            //System.Console.WriteLine("xml:\n{0}\n", xmlOutput); 
            
            return ParsedStatementFactory.CreateParsedStatement(xmlOutput);
            //System.Console.WriteLine("TreePrint: \n{0}\n", parse);
        }

        public int GetAverageRating()
        {


            var strings = new string[]
                               {
                                   "Jack was lying in his bed wondering about what he could do tomorrow.",
                                   "He thought he could go to the zoo, he thought he could go to the Park to play football with his friends.",
                                   "His parents said that they will think about it.",
                                   "He asked if he could go to the park.",
                                   "They said for the second time that they will think about it.",
                                   "Jack was lying in his bed wondering about what he could do tomorrow.",
                                   "In the morning, he asked his Mum and Dad if he could go to the zoo.",
                                   "His parents said that they will think about it.",
                                   "They said for the second time that they will think about it.",
                                   "And then he asked them if they could watch the weather for him to see if it was going to be a sunny day."

                               };
            

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.ParseStatements(strings);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            var rating = ts.Seconds;

            return rating;
        }
    }
}
