namespace Helper
{
    public class CommonConstant
    {
        public const int Read = 0;
        public const int Create = 1;
        public const int Update = 2;
        public const int Delete = 3;
        public const int ReadWithFilter = 4;

        #region UserDocumentTypes
        public const int ProfilePhoto = 1;
        public const int IdProof = 2;
        public const int AddressProof = 3;
        #endregion


    }

    public class ServiceProviderCategoryConstant
    {
        public const string Holding = "Holding";
        public const string Cash = "Cash";
        public const string Total_Holdings = "Total Holdings";
        public const string Total_Value = "Total Value";
    }

    public class CurrencySymbolConstant    // Fixer API - https://apilayer.com/marketplace/fixer-api#documentation-tab
    {
        public const string South_African_Rand = "ZAR";
        public const string Gold_troy_ounce = "XAU";
        public const string United_States_Dollar = "USD";
        public const string Bitcoin = "BTC";
    }
}
