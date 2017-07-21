using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using AngleSharp.Parser.Html;

namespace Parser.Core
{
    class ParserWorker<T> where T : class
    {
        private IParser<T> parser;
        private IParserSettings settings;
        private bool isAcrive;


        private HtmlLoader loader;

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted; 


        public ParserWorker(IParser<T> parser)
        {

            this.parser = parser;

        }

        public ParserWorker(IParser<T> parser, IParserSettings settings): this(parser)
        {

            
            this.settings = settings;

        }

        public IParserSettings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                loader = new HtmlLoader(value);
            }

        }
        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public bool IsActive => isAcrive;

        public void Start()
        {

            isAcrive = true;
            Worker();

        }

        public void Abort()
        {

            isAcrive = false;

        }

        private async void Worker()
        {

            for (int i = settings.StartPoint; i <= settings.EndPoint; i++)
            {

                if (!isAcrive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceById(i);
                var domParser = new HtmlParser();
                var document = await domParser.ParseAsync(source);
                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);

            }
            OnCompleted?.Invoke(this);
            isAcrive = false;

        }



    }
}
