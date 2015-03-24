using System;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using java.io;
using java.util;

namespace Anlis.Core
{
    public class NlpService
    {
        private LexicalizedParser _lp;
        private TokenizerFactory _tokenizerFactory;
        private GrammaticalStructureFactory _structureFactory;

        public NlpService()
        {
            string parserFileOrUrl = "englishPCFG.ser.gz";
            _lp = LexicalizedParser.loadModel(parserFileOrUrl);
            if (_lp == null)
                throw new InvalidOperationException("couldn't load " + parserFileOrUrl);
            
            _tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");

            var tlp = new PennTreebankLanguagePack();
            _structureFactory = tlp.grammaticalStructureFactory();
        }

        public void Start()
        {
            //throw new NotImplementedException();

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

        public ParsedStatement ParseSentence(string input)
        {
            var sent2Reader = new StringReader(input);
            var rawWords2 = _tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            var parse = _lp.apply(rawWords2);

                        var gs = _structureFactory.newGrammaticalStructure(parse);
            var tdl = gs.typedDependenciesCCprocessed();
            System.Console.WriteLine("\n{0}\n", tdl);

            var tp = new TreePrint("penn,typedDependenciesCollapsed");
            tp.printTree(parse);
            return new ParsedStatement(parse);
            //System.Console.WriteLine("TreePrint: \n{0}\n", parse);
        }
    }



}
