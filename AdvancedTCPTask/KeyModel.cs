namespace AdvancedTCPTask
{
    public static class KeyModel
    {
        static object locker = new object();

        public static string Key { get; set; } = string.Empty;
        public static bool Changed { get; set; } = false;

        public static string GetKey()
        {
            var response = Tcp.Request("Register\n");
            Key = response.RemoveLineFeeds();
            return Key;
        }

        public static void RenewKey()
        {
            Changed = false;
            lock (locker)
            {
                if (Changed == false)
                {
                    int waitPeriod = 1;
                    Key = GetKey();
                    while (Key.Contains("Rate limit"))
                    {
                        Thread.Sleep(100 * waitPeriod);

                        Key = GetKey();
                        waitPeriod++;
                        if (waitPeriod > 50)
                            waitPeriod = 1;
                    }
                    Changed = true;
                }
            }
        }
    }
}
