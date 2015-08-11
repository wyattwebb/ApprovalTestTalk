using System.Collections.Generic;
using System.Text;

namespace ApprovalTests.Web.Tests.Setup
{
    public class BuildHtml
    {
        public string Title { get; set; }

        public List<string> Lines { get; set; }
        
        public BuildHtml(string title)
        {
            Title = title;
            Lines = new List<string>();
        }

        public void AddLine(string line)
        {
            Lines.Add(line);
        }

        public string GetHtml()
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format("<H1>{0}</H1>", Title));
            foreach (var line in Lines)
            {
                sb.AppendLine(string.Format("<p>{0}</p>", line));
            }
            return sb.ToString();
        }

    }
}
