using System;
using System.Collections.Generic;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Dtos
{
    /// <summary>
    /// Наряд заказ
    /// </summary>
    public class CallOffOrderDto
    {
        /// <summary>
        /// Уникальный внутренний идентификатор
        /// </summary>
        public String _id;

        /// <summary>
        ///
        /// </summary>
        public String _rev;

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public String ContractId;

        /// <summary>
        /// Номер наряд заказа
        /// </summary>
        public String Number;

        /// <summary>
        /// ФИО
        /// </summary>
        public string Assignee;

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedAt;

        /// <summary>
        /// Дата и время обновления
        /// </summary>
        public DateTime UpdatedAt;

        /// <summary>
        /// Дата начала действия наряд-заказа
        /// </summary>
        public DateTime? StartDate;

        /// <summary>
        /// Дата окончания действия наряд-заказа
        /// </summary>
        public DateTime? FinishDate;

        /// <summary>
        /// Наименование заказа (по сути - работы)
        /// </summary>
        public String Name;

        /// <summary>
        /// Должность
        /// </summary>
        public string Position;

        /// <summary>
        /// Место работы
        /// </summary>
        public string Location;

        /// <summary>
        /// Системное имя шаблона НЗ
        /// </summary>
        public string TemplateSysName;  // 'Default', 'Annotech'

        /// <summary>
        /// Табельный номер
        /// </summary>
        public string EmployeeNumber;

        /// <summary>
        /// Номер позиции
        /// </summary>
        public string PositionNumber;

        /// <summary>
        /// Происхождение персонала
        /// </summary>
        public string PersonnelSource;

        /// <summary>
        /// Номер PAAF
        /// </summary>
        public string Paaf;

        /// <summary>
        /// Ссылка плана мобилизации
        /// </summary>
        public string MobPlanReference;

        /// <summary>
        /// Дата мобилизации
        /// </summary>
        public DateTime? MobDate;

        /// <summary>
        /// Шаблонные данные <имя параметра, значение>
        /// </summary>
        public Dictionary<string, object> TemplateData;

        /// <summary>
        /// Ставки
        /// </summary>
        public ICollection<RateDto> Rates;

        /// <summary>
        /// Валюта 
        /// </summary>
        public string CurrencySysName;

        public CallOffOrderDto()
        {
            Rates = new List<RateDto>();
            TemplateSysName = "default";
            TemplateData = new Dictionary<string, object>();
        }

        public String Status;

    }
}
