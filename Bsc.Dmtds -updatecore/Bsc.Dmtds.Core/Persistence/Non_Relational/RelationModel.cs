namespace Bsc.Dmtds.Core.Persistence.Non_Relational
{
    public class RelationModel
    {
        public string DisplayName { get; set; }
        public string ObjectUUID { get; set; }
        public string RelationType { get; set; }
        public object RelationObject { get; set; } 
    }
}