using System.Configuration;

namespace DT.STS.IdentityServer.Mvc.Configurations
{
    [ConfigurationCollection(typeof(DefaultClientSetting))]
    public class DefaultClientSettings : ConfigurationElementCollection
    {
        internal const string PropertyName = "defaultClientSetting";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DefaultClientSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DefaultClientSetting)(element)).ClientId;
        }

        public DefaultClientSetting this[int idx] => (DefaultClientSetting)BaseGet(idx);
    }
}