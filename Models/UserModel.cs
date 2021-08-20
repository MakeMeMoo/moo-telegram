using System;

namespace moo_telegram.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } // Идентификатор пользователя
        public long TgId { get; set; } // Идентификатор в Телеграмм
        public string TgUsername { get; set; } // Имя пользователя в Телеграмм
        public string TgFirstName { get; set; } // Имя в Телеграмм
        public string TgLastName { get; set; } // Фамилия в Телеграмм
        public string TgLanguageCode { get; set; } // Код языка
        public DateTimeOffset? LastMooDate { get; set; }  // Дата последнего Му
        public long MooCount { get; set; } // Количество Му
    }
}
