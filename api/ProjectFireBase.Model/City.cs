using Google.Cloud.Firestore;

namespace ProjectFireBase.Model
{
    [FirestoreData]
    public record City
    {
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;

        [FirestoreProperty]
        public string State { get; set; } = string.Empty;

        public City(string name, string state)
        {
            Name = name;
            State = state;
        }

        public City()
        {

        }
    }
}