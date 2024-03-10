namespace MusicOrganisationApp.Lib.Databases
{
    public interface ISqlStatement
    {
        public const string WHERE = "WHERE";
        public const string AND = "AND";
        public const string OR = "OR";
        public const string IS = "IS";
        public const string IS_NOT = "IS NOT";
        public const string EQUALS = "=";
        public const string LIKE = "LIKE";
        public const string GREATER = ">";
        public const string LESS = "<";
        public const string GREATER_OR_EQUAL = ">=";
        public const string LESS_OR_EQUAL = "<=";
        public const string NOT_EQUAL = "!=";
        public const string ASC = "ASC";
        public const string DESC = "DESC";

        public string GetSql();
    }


    public interface ISqlStatement<T> : ISqlStatement where T : class, ITable, new() { }
}