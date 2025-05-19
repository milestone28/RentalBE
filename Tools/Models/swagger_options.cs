namespace GTETools.Models
{
    public class swagger_options
    {
        private string _JsonRoute;
        public string JsonRoute
        {
            get { return _JsonRoute; }
            set { _JsonRoute = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _UIEndpoint;
        public string UIEndpoint
        {
            get { return _UIEndpoint; }
            set { _UIEndpoint = value; }
        }
    }
}
