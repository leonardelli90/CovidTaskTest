using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CovidTaskTest.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CovidTaskTest.Repositories
{
    public class MongoDbPatientRepository : IPatientsRepository
    {
        private const string databaseName = "covidPatient";
        private const string collectionName = "patient";

        private readonly IMongoCollection<Patient> patientCollection;
        private readonly FilterDefinitionBuilder<Patient> filterBuilder = Builders<Patient>.Filter;

        public MongoDbPatientRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            patientCollection = database.GetCollection<Patient>(collectionName);
        }

         public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await patientCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Patient> GetPatientAsync(Guid id)
        {
            var filter = filterBuilder.Eq(patient => patient.Id, id);
            return await patientCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Patient>> GetFilterdPatientsAsync(String timestamp, String status)
        {
            FilterDefinition<Patient> filter;
            
            if (status is null && timestamp is null)
            {
                return await patientCollection.Find(new BsonDocument()).ToListAsync();
            } else if(status is null) {
                filter = filterBuilder.Gt(patient => patient.ModifiedOn, DateTimeOffset.Parse(timestamp));
            } else if (timestamp is null) {
                filter = filterBuilder.Eq(patient => patient.Status, int.Parse(status));
            } else {
                filter = filterBuilder.Gt(patient => patient.ModifiedOn, DateTimeOffset.Parse(timestamp)) & 
                        filterBuilder.Eq(patient => patient.Status, int.Parse(status));
            }

            return await patientCollection.Find(filter).ToListAsync();
        }
       
       public async Task CreatePatientAsync(Patient patient)
       {
           await patientCollection.InsertOneAsync(patient);
       }

        public async Task UpdatePatientAsync(Patient patient)
       {
           var filter = filterBuilder.Eq(existingPatient => existingPatient.Id, patient.Id);
           await patientCollection.ReplaceOneAsync(filter, patient);
       }

       public async Task DeletePatientAsync(Guid id)
       {
           var filter = filterBuilder.Eq(person => person.Id, id);
           await patientCollection.DeleteOneAsync(filter);
       }
     
    }
}