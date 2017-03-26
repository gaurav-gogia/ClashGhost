namespace ClashGhost
{
    class Constants
    {
        #region Hai bhyi kuch, sbkuch thodi dikha dunga
        private const string baseurl = 
        private const string privateKey = Environment.GetEnvironmentVariable("privateKey");
        private const string publicKey = Environment.GetEnvironmentVariable("publicKey");
        private const string key = Environment.GetEnvironmentVariable("Akey");
        private const string iv = Environment.GetEnvironmentVariable("iv");

        private const string register = baseurl + "reg";
        private const string update = baseurl + "update";
        private const string login = baseurl + "login";
        private const string getting = baseurl + "getuser";
        private const string priv = privateKey;
        private const string publ = publicKey;
        private const string chabi = key;
        private const string vector = iv;

        internal const string REGISTER = register;
        internal const string UPDATE = update;
        internal const string LOGIN = login;
        internal const string PRIV = priv;
        internal const string GETTING = getting;
        internal const string PUBL = publ;
        internal const string KEY = chabi;
        internal const string VEC = vector;
        #endregion
    }
}
