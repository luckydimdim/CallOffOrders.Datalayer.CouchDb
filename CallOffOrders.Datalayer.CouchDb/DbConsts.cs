namespace Cmas.DataLayers.CouchDb.CallOffOrders
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя сущности
        /// </summary>
        public const string ServiceName = "call-off-orders";

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
