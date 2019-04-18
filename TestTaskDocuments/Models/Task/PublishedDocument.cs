using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskDocuments.Models.Task
{
    public class PublishedDocument
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public string Comment { get; set; }
        public string PathDocument { get; set; }

        public PublishedDocument() { }

        public PublishedDocument(string Header, string Description, string Language,
            string Publisher, string Comment, string PathDocument)
        {
            this.Header = Header;
            this.Description = Description;
            this.Language = Language;
            this.Publisher = Publisher;
            this.Comment = Comment;
            this.PathDocument = PathDocument;
        }
    }
}
