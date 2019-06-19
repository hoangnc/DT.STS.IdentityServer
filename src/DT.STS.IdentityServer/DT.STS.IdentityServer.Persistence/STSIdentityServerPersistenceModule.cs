using Autofac;

namespace DT.STS.IdentityServer.Persistence
{
    public class STSIdentityServerPersistenceModule : Module
    {
        private string _stsConnectionString;

        public STSIdentityServerPersistenceModule(string stsConnectionString)
        {
            _stsConnectionString = stsConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new STSDbContext(_stsConnectionString)).
                             AsSelf();

            base.Load(builder);
        }
    }
}
