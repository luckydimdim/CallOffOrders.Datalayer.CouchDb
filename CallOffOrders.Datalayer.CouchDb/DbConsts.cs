namespace Cmas.DataLayers.CouchDb.CallOffOrders
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя БД
        /// </summary>
        public const string DbName = "call-off-orders";    //FIXME: перенести в конфиг

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public const string DbConnectionString = "http://cmas-backend:backend967@cm-ylng-msk-03:5984";    //FIXME: перенести в конфиг

        /// <summary>
        /// Имя дизайн документа
        /// </summary>
        public const string DesignDocumentName = "call-off-orders";

        /// <summary>
        /// Имя представления всех документов
        /// </summary>
        public const string AllDocsViewName = "all";

        /// <summary>
        /// Имя представления документов, сгрупированных по ID договора
        /// </summary>
        public const string ByContractDocsViewName = "byContract";
    }
}
