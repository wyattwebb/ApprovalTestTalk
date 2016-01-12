namespace ApprovalTests.Web.Models
{
    public class TeamsViewModel
    {
        public string ErrorMessage { get; set; }

        public TeamViewModel[] Teams { get; set; }

        public class TeamViewModel
        {
            public string Center { get; set; }

            public string OffensiveGuard { get; set; }

            public string OffensiveTackle { get; set; }

            public string QuarterBack { get; set; }

            public string RunningBack { get; set; }

            public string WideReciever { get; set; }

            public string TightEnd { get; set; }

            public string DefensiveTackle { get; set; }

            public string DefensiveEnd { get; set; }

            public string LineBacker { get; set; }

            public string Safety { get; set; }

            public string CornerBack { get; set; }
        }
    }
}