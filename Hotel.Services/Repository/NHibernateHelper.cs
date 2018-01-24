using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Hotel.Services.ClassMappings;

namespace Hotel.Services.Repository
{
    public static class NHibernateHelper
    {
        private static Configuration _configuration;
        private static ISession _session;

        public static ISession Session
        {
            get
            {
                if (_session == null)
                {
                    //Open and return the nhibernate session
                    ISessionFactory sessionFactory = Configuration.BuildSessionFactory();
                    _session = sessionFactory.OpenSession();
                }
                return _session;
            }
        }

        public static Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    //Create the nhibernate configuration
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }

        private static Configuration CreateConfiguration()
        {
            var configuration = new Configuration();
            //Loads properties from hibernate.cfg.xml
            configuration.Configure();
            //Loads nhibernate mappings 
            configuration.AddDeserializedMapping(CreateMapping(), null);
            return configuration;
        }

        private static HbmMapping CreateMapping()
        {
            var mapper = new ModelMapper();
            //Add the person mapping to the model mapper
            mapper.AddMappings(new List<System.Type> { typeof(GuestMap), typeof(RoomMap), typeof(BookingMap) });
            //Create and return a HbmMapping of the model mapping in code
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }


        public static void CreateDatabaseIfNeeded()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
        }
    }
}