//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Employee_Profession.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Date_of_birth { get; set; }
        public int ID_Gender { get; set; }
        public System.DateTime Date_joining_service { get; set; }
        public int ID_Profession { get; set; }
        public int ID_Department { get; set; }
        public Nullable<System.DateTime> Date_end_service { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Profession Profession { get; set; }
    }
}
