using Neoris.TAF.Core;

namespace Example.ProjectToTest
{
    public static class Pages
    {
        public static HomePage HomePage
        {
            get { return PageFactoryHelper.InitElements<HomePage>(); }
        }
    }
}
